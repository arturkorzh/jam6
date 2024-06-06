using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
  [SerializeField] private Transform _target;

  private NavMeshAgent _agent;

  private void Awake()
  {
    _agent = GetComponent<NavMeshAgent>();
  }

  private void Update()
  {
    _agent.destination = _target.position - (_target.position - transform.position).normalized * 1.5f;
  }
}
