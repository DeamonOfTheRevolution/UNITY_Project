using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private void Awake(){
        _navMeshAgent = GetComponent <NavMeshAgent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        SetDestinationToMousePosition();    
    }
    private void SetDestinationToMousePosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        _navMeshAgent.SetDestination(hit.point);
    }
}
