using UnityEngine;

public class ObjetoReciclagem : MonoBehaviour
{
    private Vector3 posicao;
    private Vector3 destino;
    private float posicaoEsteira;
    private float velocidade = 8.0f;
    private bool movimentavel = false;
    private bool movimentar = false;
    private bool colidiu = false;
    private bool deletar = false;
    private int tempoExecucao = 100;
    private ParticleSystem ps;
    private SpriteRenderer sprite;
    private Material[] material;
    private AudioClip fogo;
    private AudioClip objetoNolixo;

    //// Start is called before the first frame update
    void Start()
    {
        material = Resources.LoadAll<Material>("materials");
        sprite = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        ps.Stop();

        fogo = Resources.Load<AudioClip>("sons/fogo");
        objetoNolixo = Resources.Load<AudioClip>("sons/objetonolixo");

        if (gameObject.tag == "runtime")
        {
            movimentar = false;
            movimentavel = true;
        } 
    }

    ////// Update is called once per frame
    void Update()
    {
        tempoExecucao++;

        float passo = velocidade * Time.deltaTime;

        if(movimentar && !deletar)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, passo);
        }

        if(deletar == true && tempoExecucao > 50)
        {
            Destroy(gameObject);
        }

        if(gameObject.transform.position.y == destino.y)
        { 
            movimentar = false;

            if(gameObject.tag == "runtime")
            {
                Destroy(gameObject);
            }
        }

        if(gameObject.transform.position.y == 0.13f)
        {
            movimentavel = true;
            velocidade = 8.0f;
        }
    }

    void OnMouseDrag()
    {
        if (movimentavel)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }

    void OnMouseUp()
    {
        if (!colidiu && gameObject.tag != "runtime")
        {
            movimentar = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!colidiu && other.tag != "esquerda" && other.tag != "direita")
        {
            if (gameObject.tag != "runtime" && other.tag != "runtime" && other.tag != "cidadeesquerda" && other.tag != "cidadedireita" && other.tag != "incineradora")
            {

                GetComponent<AudioSource>().clip = objetoNolixo;
                GetComponent<AudioSource>().Play();

                if (other.tag == "verde" && (GetComponent<SpriteRenderer>().sprite.name == "vidro_verde" || GetComponent<SpriteRenderer>().sprite.name == "vidro2_verde"))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().addVerde();
                }
                else if (other.tag == "amarelo" && (GetComponent<SpriteRenderer>().sprite.name == "metal" || GetComponent<SpriteRenderer>().sprite.name == "metal2"))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().addAmarelo();
                }
                else if (other.tag == "azul" && (GetComponent<SpriteRenderer>().sprite.name == "paper" || GetComponent<SpriteRenderer>().sprite.name == "paper2"))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().addAzul();
                }
                else if (other.tag == "vermelho" && (GetComponent<SpriteRenderer>().sprite.name == "plastico" || GetComponent<SpriteRenderer>().sprite.name == "plastico2"))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().addVermelho();
                }
                else
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().mostrarLixoDiferente();
                }

                GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>().deletaObjeto(posicaoEsteira, gameObject.tag);

                gameObject.GetComponent<ParticleSystemRenderer>().material = material[1];
                ps.Play();
                sprite.enabled = false;
                deletar = true;
                tempoExecucao = 0;
            }

            if (gameObject.tag == "runtime" && ((other.tag == "cidadeesquerda" && !GameObject.Find("GameController").GetComponent<GameController>().getSaidaEsquerda()) ||
                (other.tag == "cidadedireita" && !GameObject.Find("GameController").GetComponent<GameController>().getSaidaDireita())))
            {
                GameObject.Find("GameController").GetComponent<GameController>().addDinheiro();
                movimentar = true;

                if (other.tag == "cidadeesquerda")
                {
                    transform.position = new Vector3(-5.704f, 0.98f);
                    destino = new Vector3(-5.69f, -5.53f);
                }
                else
                {
                    transform.position = new Vector3(5.68f, 1.161f);
                    destino = new Vector3(5.7f, -5.53f);
                }

                colidiu = true;
            }

            if ((GetComponent<SpriteRenderer>().sprite.name == "vidroquebrado_incine" || GetComponent<SpriteRenderer>().sprite.name == "vidroquebrado2_incine"
                        || GetComponent<SpriteRenderer>().sprite.name == "cascaBanana_org"
                                || GetComponent<SpriteRenderer>().sprite.name == "cenoura_org"
                                    || GetComponent<SpriteRenderer>().sprite.name == "maca_org")
                && other.tag == "incineradora")
            {
                GetComponent<AudioSource>().clip = fogo;
                GetComponent<AudioSource>().volume = 1.0f;
                GetComponent<AudioSource>().Play();

                GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>().deletaObjeto(posicaoEsteira, gameObject.tag);
                ps.Play();
                sprite.enabled = false;
                deletar = true;
                tempoExecucao = 0;
            }
        } else if(!colidiu && gameObject.tag != "runtime" && (other.tag == "esquerda" || other.tag == "direita"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
            GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>().deletaObjeto(posicaoEsteira, gameObject.tag);

            gameObject.GetComponent<ParticleSystemRenderer>().material = material[1];
            ps.Play();
            sprite.enabled = false;
            deletar = true;
            tempoExecucao = 0;
        }

    }

    public void setDestino(float destinoNovo)
    {
        posicao = gameObject.transform.position;
        posicaoEsteira = destinoNovo;
        Debug.Log("destino novo");

        if (posicao.x < 0)
        {
            destino = new Vector3(-7.43f, destinoNovo);
            Debug.Log(destino.x);
        }
        else
        {
            destino = new Vector3(7.68f, destinoNovo);
            Debug.Log(destino.x);
        }

        movimentar = true;
    }

    public void setVelocidade(float velocidade)
    {
        this.velocidade = velocidade;
        movimentar = true;
    }

}
