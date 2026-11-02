using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;

    void Update()
    {
        // Se o player foi destruído ou está marcado como morto
        if (player == null || player.GetComponent<PlayerControlls>().isDead)
        {
            SceneManager.LoadScene("GameOverScene"); // substitua pelo nome da cena de Game Over
        }

        // Se o boss foi destruído
        if (boss == null)
        {
            SceneManager.LoadScene("VictoryScene"); // substitua pelo nome da cena de Vitória
        }
    }
}
