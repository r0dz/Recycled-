﻿using UnityEngine;

public class ObjetoReciclagem : MonoBehaviour
{
    private Vector3 posicao;
    private Vector3 destino;
    private float velocidade = 8.0f;
    private GameObject gameController;
    private bool chegou = false;
    private int cidade;


    //// Start is called before the first frame update
    void Start()
    {
        posicao = gameObject.transform.position;

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

        if( gameObject.transform.position.x == -7.43f && !chegou)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, passo);
            Debug.Log("alterando");
        }
        if (gameObject.transform.position.x == 7.68f && !chegou)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, passo);
            Debug.Log("alterando");
        }

        if(gameObject.transform.position.y == 0.87f || gameObject.transform.position.y == 1.06f)
        {
            chegou = true;
        }
    }

    void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag != "runtime" && other.tag != "runtime" && other.tag != "cidadeesquerda" && other.tag != "cidadedireita")
        {
            if (other.tag == "verde" && GetComponent<SpriteRenderer>().sprite.name == "vidro")
            {
                GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                GameObject.Find("GameController").GetComponent<GameController>().addVerde();
            }
            else if (other.tag == "amarelo" && GetComponent<SpriteRenderer>().sprite.name == "metal")
            {
                GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                GameObject.Find("GameController").GetComponent<GameController>().addAmarelo();
            }
            else if (other.tag == "azul" && GetComponent<SpriteRenderer>().sprite.name == "papel")
            {
                GameObject.Find("GameController").GetComponent<GameController>().remDinheiro();
                GameObject.Find("GameController").GetComponent<GameController>().addAzul();
            }
            else if (other.tag == "vermelho" && GetComponent<SpriteRenderer>().sprite.name == "plastico")
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
            Destroy(gameObject);
        }

    }

}