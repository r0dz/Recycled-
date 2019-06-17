using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool instrucoes = false;

    public void IniciarJogo()
    {
        SceneManager.LoadScene("dificuldade");
    }

    public void RetornarJogo()
    {
        SceneManager.LoadScene("dificuldade");
    }

    public void JogoFacil()
    {
        PlayerPrefs.SetFloat("velocidade", 1.0f);
        PlayerPrefs.SetInt("limiteconteiner", 10);
        PlayerPrefs.SetInt("tempointervalo", 200);
        PlayerPrefs.SetInt("facil", 1);
        SceneManager.LoadScene("jogo");
    }

    public void JogoMedio()
    {
        PlayerPrefs.SetFloat("velocidade", 3.0f);
        PlayerPrefs.SetInt("limiteconteiner", 8);
        PlayerPrefs.SetInt("tempointervalo", 150);
        PlayerPrefs.SetInt("facil", 0);
        SceneManager.LoadScene("jogo");
    }

    public void JogoDificil()
    {
        PlayerPrefs.SetFloat("velocidade", 4.0f);
        PlayerPrefs.SetInt("limiteconteiner", 6);
        PlayerPrefs.SetInt("tempointervalo", 120);
        PlayerPrefs.SetInt("facil", 0);
        SceneManager.LoadScene("jogo");
    }

    public void mostrarInformacoes()
    {
        if (instrucoes)
        {
            GameObject.Find("instrucoes").GetComponent<Image>().enabled = false;
            instrucoes = false;
        } else
        {
            GameObject.Find("GameController").GetComponent<GameController>().setZeroTempoIntrucoes();
            GameObject.Find("instrucoes").GetComponent<Image>().enabled = true;
            instrucoes = true;
        }
    }

    public void setFalseBool()
    {
        instrucoes = false;
    }
}
