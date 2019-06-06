using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("jogo");
    }

    public void RetornarJogo()
    {
        SceneManager.LoadScene("jogo");
    }

}
