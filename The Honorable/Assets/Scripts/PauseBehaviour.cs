using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    [Header("Referência ao painel do menu de pausa")]
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        // Apertar ESC abre/fecha o menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // Botão "Voltar ao jogo"
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // jogo volta ao normal
        isPaused = false;
    }

    // Botão "Menu principal"
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // garante que o tempo volte ao normal
        SceneManager.LoadScene("MenuInicialScene"); // substitua pelo nome da sua cena de menu principal
    }

    // Ativa o menu de pausa
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // congela o jogo
        isPaused = true;
    }
}