using System.Collections.Generic;
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
    Dictionary<float, GameObject> esteiraEsquerda = new Dictionary<float, GameObject>()
    {
        {0.13f, null},
        {-2.8f, null},
        {-4.30f, null}
    };
    Dictionary<float, GameObject> esteiraDireita = new Dictionary<float, GameObject>()
    {
        {0.13f, null},
        {-2.8f, null},
        {-4.30f, null}
    };
    string[] tipos = { "vidroquebrado_incine", "vidroquebrado2_incine", "vidro_verde", "vidro2_verde", "plastico", "plastico2", "metal", "metal2", "paper", "paper2", "cascaBanana_org", "cenoura_org", "maca_org"};
    Sprite[] sprites;
    Random random = new Random();
    private float velocidade;
    private int tempoIntervalo;
    int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("icons");
        velocidade = PlayerPrefs.GetFloat("velocidade");
        tempoIntervalo = PlayerPrefs.GetInt("tempointervalo");
    }

    // Update is called once per frame
    void Update()
    {
        time++;

        if (time > tempoIntervalo)
        {
            instanciaObjetos();
            time = 0;
        }
    }

    private void instanciaObjetos()
    {
        if(GameObject.Find("GameController").GetComponent<GameController>().getExecutando())
        {
            List<float> posicoesVaziasEsquerda = verificaEsteira(esteiraEsquerda);
            List<float> posicoesVaziasDireita = verificaEsteira(esteiraDireita);

            objetoGame = (UnityEngine.GameObject)Resources.Load("gameobjects/objetoreciclagem");

            if (objetoGame != null)
            { 

                if(posicoesVaziasEsquerda.Count != 0)
                { 
                    foreach (float posicao in posicoesVaziasEsquerda)
                    {
                        objetoGame = Instantiate(objetoGame, new Vector3(-7.43f, -5.53f), Quaternion.identity);
                        objetoGame.tag = "esquerda";

                        esteiraEsquerda[posicao] = objetoGame;
                        esteiraEsquerda[posicao].GetComponent<SpriteRenderer>().sprite = sprites[random.Next(13)];
                        esteiraEsquerda[posicao].GetComponent<ObjetoReciclagem>().setDestino(0.13f);

                        esteiraEsquerda[posicao].GetComponent<ObjetoReciclagem>().setVelocidade(velocidade);

                        break;
                    }                     
                }
                if(posicoesVaziasDireita.Count != 0)
                {
                    foreach (float posicao in posicoesVaziasDireita)
                    {
                        objetoGame = Instantiate(objetoGame, new Vector3(7.68f, -5.57f), Quaternion.identity);
                        objetoGame.tag = "direita";

                        esteiraDireita[posicao] = objetoGame;
                        esteiraDireita[posicao].GetComponent<SpriteRenderer>().sprite = sprites[random.Next(13)];
                        esteiraDireita[posicao].GetComponent<ObjetoReciclagem>().setDestino(0.13f);

                        esteiraDireita[posicao].GetComponent<ObjetoReciclagem>().setVelocidade(velocidade);

                        break;
                    }
                }
                //Debug.Log("objeto criado");
            }
        }
    }

    private List<float> verificaEsteira(Dictionary<float, GameObject> esteira)
    {
        List<float> posicoesVazias = new List<float>();

        foreach (KeyValuePair<float, GameObject> objeto in esteira)
        {
            if(objeto.Value == null)
            {
                posicoesVazias.Add(objeto.Key);
            }
        }

        return posicoesVazias;
    }

    //private void reordenaEsteira(Dictionary<float, GameObject> esteira)
    //{
    //    esteira[-2.8f].GetComponent<ObjetoReciclagem>().setDestino(0.13f);
    //    esteira[0.13f] = esteira[-2.8f];
    //    esteira[0.13f].GetComponent<ObjetoReciclagem>().setVelocidade(1.0f);

    //    esteira[-4.30f].GetComponent<ObjetoReciclagem>().setDestino(0.13f);
    //    esteira[-2.8f] = esteira[-4.30f];
    //    esteira[-2.8f].GetComponent<ObjetoReciclagem>().setVelocidade(1.0f);

    //    esteira[-4.30f] = null;
    //}

    public void deletaObjeto(float posicaoEsteira, string cidade)
    {
        if(cidade == "esquerda")
        {
            esteiraEsquerda[posicaoEsteira] = null;
            //reordenaEsteira(esteiraEsquerda);
        } else
        {
            esteiraDireita[posicaoEsteira] = null;
            //reordenaEsteira(esteiraDireita);
        }

        Debug.Log("removido");
        Debug.Log(objetos.Count.ToString());
    }
}
