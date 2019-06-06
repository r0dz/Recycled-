using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private Text text;
    private string dinheiroAlteradoText;
    private string nomeCena = "Fim";
    private bool executando = true;
    private int tempoTexto = 200;

    // Start is called before the first frame update
    void Start()
    {
        mensagemLabel = GameObject.Find("dinheiro-alterado");
        mensagemLixo = GameObject.Find("mensagens");
        atualizaLabels();
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;

        if(tempo <= 0)
        {
            executando = false;
            SceneManager.LoadScene(nomeCena);
        }

        if(executando)
        {
            atualizaLabels();
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
        containerAmarelo++;
    }

    public bool remAmarelo()
    {
        if (containerAmarelo > 0)
        {
            containerAmarelo--;
            return true;
        }

        return false;
    }

    public void addVerde()
    {
        containerVerde++;
    }

    public bool remVerde()
    {
        if (containerVerde > 0)
        {
            containerVerde--;
            return true;
        }
        return false;
    }

    public void addAzul()
    {
        containerAzul++;
    }

    public bool remAzul()
    {
        if (containerAzul > 0)
        {
            containerAzul--;
            return true;
        }
        return false;
    }

    public void addVermelho()
    {
        containerVermelho++;
    }

    public bool remVermelho()
    {
        if (containerVermelho > 0)
        {
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
}
