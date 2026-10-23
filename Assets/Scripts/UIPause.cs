using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    public event Action<bool>Paused;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            _pausePanel.SetActive(!_pausePanel.activeSelf);
            Paused?.Invoke(_pausePanel.activeSelf);
        }
    }
    public void ResumeGame(){
        Paused?.Invoke(false);
        _pausePanel.SetActive(false);
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame(string menuSceneName){
        SceneManager.LoadScene(menuSceneName);
    }
}
