using Pathfinding;
using UnityEngine;

public class EnemyFollowerController : MonoBehaviour
{
  private const float CheckDelay = 0.5f;
  private const float DamageDistance = 1.5f;
  
  public Transform SpawnPosition;
  [SerializeField] private float _distanceToStart;
  [SerializeField] private float _distanceToStop;
  [SerializeField] private AIDestinationSetter _destinationSetter;

  private float _checkTimer = CheckDelay;
  private bool _isFollow;

  private void OnEnable()
  {
    EventManager.OnEnemyDied += DieHandler;
  }

  private void OnDisable()
  {
    EventManager.OnEnemyDied -= DieHandler;
  }

  private void DieHandler(GameObject go)
  {
    if (go == gameObject)
      Destroy(gameObject);
  }
  
  private void Update()
  {
    _checkTimer -= Time.deltaTime;
    if (_checkTimer > 0f)
      return;

    if (GetCloserPlayerElement() is {} element)
    {
      _destinationSetter.target = element;
      _isFollow = true;
    }
    else if (_isFollow)
    {
      _destinationSetter.target = SpawnPosition;
      _isFollow = false;
    }

    _checkTimer = CheckDelay;
  }

  private Transform GetCloserPlayerElement()
  {
    var playerElements = PlayerView.Instance.Joinable.Elements;
    (JoinableElementView Element, float Distance) closest = (null, Mathf.NegativeInfinity);
    var curThreshold = !_isFollow ? _distanceToStart : _distanceToStop;
    
    foreach (var element in playerElements)
    {
      var direction = element.transform.position - transform.position;
      var distance = direction.magnitude;
      var isCloseEnough = distance < curThreshold;
      if (!isCloseEnough)
        continue;

      if (closest.Distance > distance)
        continue;

      if (Physics.Raycast(new(transform.position, direction.normalized), distance))
        continue;
      
      closest = (element, distance);
    }

    return closest.Element != null ? closest.Element.transform : null;
  }
}
