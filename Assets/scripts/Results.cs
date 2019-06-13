using System;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{

    private int pontos = 0;
    private Sprite trofeuBronze;
    private Sprite trofeuPrata;
    private Sprite trofeuOuro;
    private string texto = "";

    // Start is called before the first frame update
    void Start()
    {

        trofeuBronze = Resources.Load<Sprite>("images/trofeu_bronze");
        trofeuPrata = Resources.Load<Sprite>("images/trofeu_prata");
        trofeuOuro = Resources.Load<Sprite>("images/trofeu_ouro");

        if (PlayerPrefs.GetInt("lixoamarelo") >= 5)
        {
            pontos++;
            texto = String.Concat(texto, "Você reciclou metal corretamente! \n \n");
        }

        if (PlayerPrefs.GetInt("lixoverde") >= 5)
        {
            pontos++;
            texto = String.Concat(texto, "Você reciclou vidro corretamente! \n \n");
        }

        if (PlayerPrefs.GetInt("lixoazul") >= 5)
        {
            pontos++;
            texto = String.Concat(texto, "Você reciclou papel corretamente! \n \n");
        }

        if (PlayerPrefs.GetInt("lixovermelho") >= 5)
        {
            texto = String.Concat(texto, "Você reciclou plástico corretamente!");
            pontos++;
        }

        Debug.Log(PlayerPrefs.GetInt("lixoamarelo").ToString());

        GameObject.Find("resultado").GetComponent<Text>().text = texto;

        if (pontos == 4)
        {
            GameObject.Find("premio").GetComponent<Image>().sprite = trofeuOuro;
        } else if(pontos == 3)
        {
            GameObject.Find("premio").GetComponent<Image>().sprite = trofeuPrata;
        } else
        {
            GameObject.Find("premio").GetComponent<Image>().sprite = trofeuBronze;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
