using UnityEngine;

public class ObjetoReciclagem : MonoBehaviour
{
    private Vector3 posicao;
    private Vector3 destino;
    private float velocidade = 8.0f;
    private bool movimentar = true;
    private bool colidiu = false;
    private bool deletar = false;
    private int tempoExecucao = 100;
    private int cidade;
    private ParticleSystem ps;
    private SpriteRenderer sprite;
    private Material[] material;


    //// Start is called before the first frame update
    void Start()
    {
        material = Resources.LoadAll<Material>("materials");
        posicao = gameObject.transform.position;
        sprite = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        ps.Stop();

        if (gameObject.tag == "runtime")
        {
            movimentar = false;
        } 

        if(posicao.x < 0)
        {
            destino = new Vector3(-7.43f, 0.87f);
        }
        else
        {
            destino = new Vector3(7.68f, 1.06f);
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
    }

    void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    void OnMouseUp()
    {
        if (colidiu)
        {
            movimentar = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!colidiu)
        {
            if (gameObject.tag != "runtime" && other.tag != "runtime" && other.tag != "cidadeesquerda" && other.tag != "cidadedireita" && other.tag != "incineradora")
            {
                if (other.tag == "verde" && (GetComponent<SpriteRenderer>().sprite.name == "vidro" || GetComponent<SpriteRenderer>().sprite.name == "vidro2"))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().addVerde();
                }
                else if (other.tag == "amarelo" && (GetComponent<SpriteRenderer>().sprite.name == "metal" || GetComponent<SpriteRenderer>().sprite.name == "metal2"))
                {
                    GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                    GameObject.Find("GameController").GetComponent<GameController>().addAmarelo();
                }
                else if (other.tag == "azul" && (GetComponent<SpriteRenderer>().sprite.name == "papel" || GetComponent<SpriteRenderer>().sprite.name == "papel2"))
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

                if (gameObject.tag == "esquerda")
                {
                    cidade = 0;
                    Debug.Log("esquerda");
                }
                else
                {
                    cidade = 1;
                    Debug.Log("direita");
                }

                GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>().deletaObjeto(cidade);

                gameObject.GetComponent<ParticleSystemRenderer>().material = material[1];
                ps.Play();
                sprite.enabled = false;
                deletar = true;
                tempoExecucao = 0;
            }

            if (gameObject.tag == "runtime" && (other.tag == "cidadeesquerda" || other.tag == "cidadedireita"))
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

            if ((GetComponent<SpriteRenderer>().sprite.name == "vidroquebrado" || GetComponent<SpriteRenderer>().sprite.name == "vidroquebrado2")
                && other.tag == "incineradora")
            {
                GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>().deletaObjeto(cidade);
                ps.Play();
                sprite.enabled = false;
                deletar = true;
                tempoExecucao = 0;
            }
        }

    }

}
