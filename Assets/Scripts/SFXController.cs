using UnityEngine;

public abstract class SFXController : MonoBehaviour
{
    private FPC_Controller _FPCController;
    private void Awake(){
        _FPCController = GetComponent<FPC_Controller>();
        Initialization();
    }
    private void OnEnable(){
        _FPCController.Moving += OnPlayerMoving;
    }
    protected abstract void Initialization();
    protected abstract void OnPlayerMoving(bool moving);
}
