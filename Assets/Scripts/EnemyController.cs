using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _sightRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _walkRange;

    private NavMeshAgent _enemyAgent;
    private Vector3 _walkPoint;
    private bool _walkPointSet;

    private void Awake(){
        _enemyAgent = GetComponent <NavMeshAgent>();
        if (_enemyAgent == null)
        Debug.LogError("NavMeshAgent not found");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    public event System.Action PlayerReached;
    void Update()
    {
        if(IsPlayerInSight())
            MoveToTarget(_playerTransform.position);
        else Patrol();
        if(IsPlayerInAttackRange()){
            PlayerReached?.Invoke();
        }
    }
    private void MoveToTarget(Vector3 target){
        _enemyAgent.SetDestination(target);
    }
    private bool IsPlayerInSight(){
        Vector3 direction = _playerTransform.position - transform.position;
        Debug.DrawLine(transform.position, transform.position + direction.normalized * _sightRange, Color.magenta);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + direction.normalized * direction.magnitude, Color.blue);
        bool player = Physics.Raycast(transform.position, direction, _sightRange, _playerMask);
        bool obstacle = Physics.Raycast(transform.position, direction, direction.magnitude, ~_playerMask);
        return !(!player | obstacle);
    }
    private bool IsPlayerInAttackRange(){
        return Physics.CheckSphere(transform.position, _attackRange, _playerMask);
    }
    private void Patrol(){
        Vector3 directionToPoint = _walkPoint - transform.position;
        if (directionToPoint.magnitude < 1f)
        _walkPointSet = false;
        if (_walkPointSet) MoveToTarget(_walkPoint);
        else SearchPoint();
    }
    private void SearchPoint(){
        float randomOffsetX = Random.Range(-_walkRange, _walkRange);
        float randomOffsetZ = Random.Range(-_walkRange, _walkRange);
        _walkPoint = new Vector3(transform.position.x + randomOffsetX, transform.position.y, transform.position.z + randomOffsetZ);

        NavMeshPath path = new NavMeshPath();
        _enemyAgent.CalculatePath(_walkPoint, path);
        if (path.status == NavMeshPathStatus.PathComplete){
            _walkPointSet = true;
            Debug.DrawLine(_walkPoint, _walkPoint + Vector3.up, Color.green, 3f);
        } else
            Debug.DrawLine(_walkPoint, _walkPoint + Vector3.up, Color.red, 3f);
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(_walkRange * 2, transform.position.y, _walkRange * 2));
    }
}
