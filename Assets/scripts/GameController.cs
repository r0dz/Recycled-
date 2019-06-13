using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    private int dinheiro = 0;
    private float tempo = 99f;
    private int containerAmarelo = 0;
    private int containerVermelho = 0;
    private int containerVerde = 0;
    private int containerAzul = 0;
    private GameObject textLabel;
    private GameObject mensagemLabel;
    private GameObject mensagemLixo;
    private GameObject cheioAmarelo;
    private GameObject cheioVerde;
    private GameObject cheioAzul;
    private GameObject cheioVermelho;
    private Text text;
    private int limiteConteiner;
    private string dinheiroAlteradoText;
    private string nomeCena = "Fim";
    private bool executando = true;
    private int tempoTexto = 200;
    private int lixoAmarelo = 0;
    private int lixoVermelho = 0;
    private int lixoVerde = 0;
    private int lixoAzul = 0;
    private int contagem = 0;
    private bool saidaEsquerda = false;
    private bool saidaDireita = false;
    private int aleatorio;
    Random random;

    // Start is called before the first frame update
    void Start()
    {
        random = new Random();
        mensagemLabel = GameObject.Find("dinheiro-alterado");
        mensagemLixo = GameObject.Find("mensagens");
        cheioAmarelo = GameObject.Find("cheio-amarelo");
        cheioVerde = GameObject.Find("cheio-verde");
        cheioVermelho = GameObject.Find("cheio-vermelho");
        cheioAzul = GameObject.Find("cheio-azul");
        limiteConteiner = PlayerPrefs.GetInt("limiteconteiner");
        atualizaLabels();
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;
        contagem++;

        if(tempo <= 0)
        {
            executando = false;
            PlayerPrefs.SetInt("lixoamarelo", lixoAmarelo);
            PlayerPrefs.SetInt("lixovermelho", lixoVermelho);
            PlayerPrefs.SetInt("lixoverde", lixoVerde);
            PlayerPrefs.SetInt("lixoazul", lixoAzul);
            SceneManager.LoadScene(nomeCena);
        }

        if(executando)
        {
            atualizaFechamento();
            atualizaLabels();
        }
    }

    private void atualizaFechamento()
    {
        if(contagem > 300)
        {
            GameObject.Find("cidadeesquerda").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("cidadedireita").GetComponent<SpriteRenderer>().enabled = false;
            saidaEsquerda = false;
            saidaDireita = false;
        }

        if(random.Next(100) == 4 && !saidaDireita && !saidaEsquerda)
        {
            aleatorio = random.Next(3);
            contagem = 0;

            if(aleatorio == 1)
            {
                GameObject.Find("cidadeesquerda").GetComponent<SpriteRenderer>().enabled = true;
                saidaEsquerda = true;
            } else
            {
                GameObject.Find("cidadedireita").GetComponent<SpriteRenderer>().enabled = true;
                saidaDireita = true;
            }
        }
        
    }

    private void atualizaLabels()
    {
        textLabel = GameObject.Find("tempo");
        text = textLabel.GetComponent<Text>();
        text.text = Mathf.Round(tempo).ToString();

        textLabel = GameObject.Find("dinheiro");
        text = textLabel.GetComponent<Text>();
        text.text = dinheiro.ToString();

        textLabel = GameObject.Find("amarelo-text");
        text = textLabel.GetComponent<Text>();
        text.text = containerAmarelo.ToString();

        textLabel = GameObject.Find("vermelho-text");
        text = textLabel.GetComponent<Text>();
        text.text = containerVermelho.ToString();

        textLabel = GameObject.Find("verde-text");
        text = textLabel.GetComponent<Text>();
        text.text = containerVerde.ToString();

        textLabel = GameObject.Find("azul-text");
        text = textLabel.GetComponent<Text>();
        text.text = containerAzul.ToString();

        text = mensagemLabel.GetComponent<Text>();
        text.text = dinheiroAlteradoText;

        tempoTexto++;
        if (tempoTexto > 100)
        {
            mensagemLabel.GetComponent<Text>().enabled = false;
            mensagemLixo.GetComponent<Text>().enabled = false;
        }
    }

    public void addAmarelo()
    {
        if (containerAmarelo == limiteConteiner)
        {
            cheioAmarelo.GetComponent<Text>().enabled = true;
        }
        else
        {
            lixoAmarelo++;
            containerAmarelo++;
        }
    }

    public bool remAmarelo()
    {
        if (containerAmarelo > 0)
        {
            cheioAmarelo.GetComponent<Text>().enabled = false;
            containerAmarelo--;
            return true;
        }

        return false;
    }

    public void addVerde()
    {
        if (containerVerde == limiteConteiner)
        {
            cheioVerde.GetComponent<Text>().enabled = true;
        }
        else
        {
            lixoVerde++;
            containerVerde++;
        }
    }

    public bool remVerde()
    {
        if (containerVerde > 0)
        {
            cheioVerde.GetComponent<Text>().enabled = false;
            containerVerde--;
            return true;
        }
        return false;
    }

    public void addAzul()
    {
        if (containerAzul == limiteConteiner)
        {
            cheioAzul.GetComponent<Text>().enabled = true;
        }
        else
        {
            lixoAzul++;
            containerAzul++;
        }
    }

    public bool remAzul()
    {
        if (containerAzul > 0)
        {
            cheioAzul.GetComponent<Text>().enabled = false;
            containerAzul--;
            return true;
        }
        return false;
    }

    public void addVermelho()
    {
        if (containerVermelho == limiteConteiner)
        {
            cheioVermelho.GetComponent<Text>().enabled = true;
        }
        else
        {
            lixoVermelho++;
            containerVermelho++;
        }
    }

    public bool remVermelho()
    {
        if (containerVermelho > 0)
        {
            cheioVermelho.GetComponent<Text>().enabled = false;
            containerVermelho--;
            return true;
        }
        return false;
    }

    public void addDinheiro()
    {
        dinheiro += 4;
        dinheiroAlteradoText = "+4";
        mensagemLabel.GetComponent<Text>().color = Color.blue;
        mensagemLabel.GetComponent<Text>().enabled = true;
        tempoTexto = 0;
    }

    public void remDinheiro()
    {
        dinheiro -= 1;
        dinheiroAlteradoText = "-1";
        mensagemLabel.GetComponent<Text>().color = Color.red;
        mensagemLabel.GetComponent<Text>().enabled = true;
        tempoTexto = 0;
    }

    public void mostrarLixoDiferente()
    {
        mensagemLixo.GetComponent<Text>().enabled = true;
        tempoTexto = 0;
    }

    public bool getExecutando()
    {
        return executando;
    }

    public bool getSaidaEsquerda()
    {
        return saidaEsquerda;
    }

    public bool getSaidaDireita()
    {
        return saidaDireita;
    }
}
