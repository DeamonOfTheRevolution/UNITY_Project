using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    private EnemyController _enemy;
    private UIPause _UIPause;

    public static Game Instance {get; private set;}
    public PauseManager PauseManager {get; private set;}
    private void Awake(){
        Instance = this;
        PauseManager = new PauseManager();
        _enemy = FindObjectOfType<EnemyController>();
        _UIPause = FindObjectOfType<UIPause>();
    }
    private void OnEnable(){
        //_UIPause.Paused += OnPauseClicked;
        _enemy.PlayerReached += OnPlayerReached;
    }
    private void OnPlayerReached(){
        Debug.Log("You are under attack!");
    }
}
