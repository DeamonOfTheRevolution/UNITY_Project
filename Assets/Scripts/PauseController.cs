using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused;

    void Start(){
        pausePanel.SetActive(false);

        isPaused = false;
        GamePauseState.IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Pause(){
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        isPaused = true;
        GamePauseState.IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        isPaused = false;
        GamePauseState.IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame(){
        Time.timeScale = 1f;

        isPaused = false;
        GamePauseState.IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu(string menuSceneName){
        Time.timeScale = 1f;

        isPaused = false;
        GamePauseState.IsPaused = false;

        SceneManager.LoadScene(menuSceneName);
    }
}