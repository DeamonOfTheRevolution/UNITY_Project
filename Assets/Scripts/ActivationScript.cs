using UnityEngine;
using System.Collections;

public class ActivationScript : MonoBehaviour
{
    public GameObject npc;
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player"))
            npc.SetActive(true);
    }
}
