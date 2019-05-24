using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ObjectsManager : MonoBehaviour
{

    public class Objeto
    {
        public int cidade;
        public string tipo;
        public int tempo;

        public Objeto(int cida, string ti)
        {
            cidade = cida;
            tipo = ti;
            tempo = 0;
        }
    }

    GameObject objetoGame;
    List<Objeto> objetos = new List<Objeto>();
    string[] tipos = { "vidroquebrado", "plastico", "metal", "papel" };
    Sprite[] sprites;
    Random random = new Random();

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("icons");
    }

    // Update is called once per frame
    void Update()
    {
        instanciaObjetos();
    }

    private void instanciaObjetos()
    {
        if(objetos.Count < 2 && GameObject.Find("GameController").GetComponent<GameController>().getExecutando())
        {
            Objeto objeto = criaObjetoAleatorio();
            objetos.Add(objeto);
            objetoGame = (UnityEngine.GameObject)Resources.Load("gameobjects/objetoreciclagem");

            if (objetoGame != null)
            {
                if (objeto.cidade == 0)
                {
                    objetoGame = Instantiate(objetoGame, new Vector3(-7.43f, -5.53f), Quaternion.identity);
                    objetoGame.tag = "esquerda";
                }
                else
                {
                    objetoGame = Instantiate(objetoGame, new Vector3(7.68f, -5.57f), Quaternion.identity);
                    objetoGame.tag = "direita";
                }
                Debug.Log("objeto criado");

                objetoGame.GetComponent<SpriteRenderer>().sprite = sprites[random.Next(4)];
            }
        }
    }

    private Objeto criaObjetoAleatorio()
    {
        int cidade;
        string tipo = tipos[random.Next(4)];

        if(objetos.Count == 0)
        {
            cidade = random.Next(2);
        } 
        else
        {
            if (objetos.Any((obj) => obj.cidade == 0))
            {
                cidade = 1;
            }
            else
            {
                cidade = 0;
            }
        }
        Debug.Log(cidade.ToString());

        return new Objeto(cidade, tipo);
    }

    public void deletaObjeto(int cidade)
    {
        objetos.Remove(objetos.Find((obj) => obj.cidade == cidade));
        Debug.Log("removido");
        Debug.Log(objetos.Count.ToString());
    }
}
