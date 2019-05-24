using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;

public class Entrada : MonoBehaviour
{
    Random random = new Random();
    Sprite[] sprites;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("quadrados");
        gameObject.GetComponent<Image>().sprite = sprites[random.Next(4)];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        atualizaEntrada();
    }

    private void atualizaEntrada()
    {
        if (timer > 5)
        {
            gameObject.GetComponent<Image>().sprite = sprites[random.Next(4)];
            timer = 0.0f;
        }
    }
}
