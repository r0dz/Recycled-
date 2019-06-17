using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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
        SceneManager.LoadScene("jogo");
    }

    public void JogoMedio()
    {
        PlayerPrefs.SetFloat("velocidade", 3.0f);
        PlayerPrefs.SetInt("limiteconteiner", 8);
        PlayerPrefs.SetInt("tempointervalo", 150);
        SceneManager.LoadScene("jogo");
    }

    public void JogoDificil()
    {
        PlayerPrefs.SetFloat("velocidade", 4.0f);
        PlayerPrefs.SetInt("limiteconteiner", 6);
        PlayerPrefs.SetInt("tempointervalo", 120);
        SceneManager.LoadScene("jogo");
    }
}
