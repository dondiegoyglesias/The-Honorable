using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverInicio : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MenuInicialScene");
        }
    }
}
