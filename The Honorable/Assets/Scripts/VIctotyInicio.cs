using UnityEngine;
using UnityEngine.SceneManagement;

public class VIctotyInicio : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MenuInicialScene");
        }
    }
}