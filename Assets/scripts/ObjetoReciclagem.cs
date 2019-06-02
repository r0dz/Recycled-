using UnityEngine;

public class ObjetoReciclagem : MonoBehaviour
{
    private Vector3 posicao;
    private Vector3 destino;
    private float velocidade = 8.0f;
    private bool movimentar = true;
    private int cidade;


    //// Start is called before the first frame update
    void Start()
    {
        posicao = gameObject.transform.position;

        if(gameObject.tag == "runtime")
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
        float passo = velocidade * Time.deltaTime;

        if(movimentar)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, passo);
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

    void OnTriggerEnter2D(Collider2D other)
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

            Destroy(gameObject);
        }

        if(gameObject.tag == "runtime" && (other.tag == "cidadeesquerda" || other.tag == "cidadedireita")) 
        {
            GameObject.Find("GameController").GetComponent<GameController>().addDinheiro();
            movimentar = true;

            if(other.tag == "cidadeesquerda")
            {
                transform.position = new Vector3(-5.704f, 0.98f); 
                destino = new Vector3(-5.69f, -5.53f);
            } else
            {
                transform.position = new Vector3(5.68f, 1.161f);
                destino = new Vector3(5.7f, -5.53f);
            }

        }

        if((GetComponent<SpriteRenderer>().sprite.name == "vidroquebrado" || GetComponent<SpriteRenderer>().sprite.name == "vidroquebrado2") 
            && other.tag == "incineradora")
        {
            GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>().deletaObjeto(cidade);
            Destroy(gameObject);
        }

    }

}
