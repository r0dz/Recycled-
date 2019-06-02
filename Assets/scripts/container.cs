using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class container : MonoBehaviour
{
    GameObject objetoGame;
    Sprite[] sprites;

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("iconsreciclados");
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "amarelo")
        {
            if (GameObject.Find("GameController").GetComponent<GameController>().remAmarelo())
            {
                objetoGame = (UnityEngine.GameObject)Resources.Load("gameobjects/objetoreciclagem");
                objetoGame = Instantiate(objetoGame, new Vector3(-7.43f, -5.53f), Quaternion.identity);
                objetoGame.GetComponent<SpriteRenderer>().sprite = sprites[0];
                objetoGame.tag = "runtime";
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                objetoGame.transform.position = mousePos;
            }
        }

        if (gameObject.tag == "azul")
        {
            if (GameObject.Find("GameController").GetComponent<GameController>().remAzul())
            {
                objetoGame = (UnityEngine.GameObject)Resources.Load("gameobjects/objetoreciclagem");
                objetoGame = Instantiate(objetoGame, new Vector3(-7.43f, -5.53f), Quaternion.identity);
                objetoGame.GetComponent<SpriteRenderer>().sprite = sprites[1];
                objetoGame.tag = "runtime";
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                objetoGame.transform.position = mousePos;
            }
        }

        if (gameObject.tag == "vermelho")
        {
            if (GameObject.Find("GameController").GetComponent<GameController>().remVermelho())
            {
                objetoGame = (UnityEngine.GameObject)Resources.Load("gameobjects/objetoreciclagem");
                objetoGame = Instantiate(objetoGame, new Vector3(-7.43f, -5.53f), Quaternion.identity);
                objetoGame.GetComponent<SpriteRenderer>().sprite = sprites[2];
                objetoGame.tag = "runtime";
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                objetoGame.transform.position = mousePos;
            }
        }

        if (gameObject.tag == "verde")
        {
            if (GameObject.Find("GameController").GetComponent<GameController>().remVerde())
            {
                objetoGame = (UnityEngine.GameObject)Resources.Load("gameobjects/objetoreciclagem");
                objetoGame = Instantiate(objetoGame, new Vector3(-7.43f, -5.53f), Quaternion.identity);
                objetoGame.GetComponent<SpriteRenderer>().sprite = sprites[3];
                objetoGame.tag = "runtime";
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                objetoGame.transform.position = mousePos;
            }
        }
    }
}
