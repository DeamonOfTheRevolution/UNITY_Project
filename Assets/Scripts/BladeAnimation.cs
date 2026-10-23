using UnityEngine;

public class BladeAnimation : MonoBehaviour
{
    private Animator _characterAnimator;
    private void Awake(){
        _characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
            _characterAnimator.SetTrigger("SaberAttack");
    }
}
