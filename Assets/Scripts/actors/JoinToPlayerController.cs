using UnityEngine;

public class JoinToPlayerController : MonoBehaviour
{
  [SerializeField] private JoinableView _player;

  private const float Threshold = 1.11f;
  
  private int _playerLayer;
  private int _joinableLayer;

  private void Awake()
  {
    _playerLayer = LayerMask.NameToLayer("Player");
    _joinableLayer = LayerMask.NameToLayer("Joinable");
  }

  private void OnCollisionEnter(Collision other)
  {
    if (IsNotJoinable(other.gameObject))
      return;

    TryJoinToPlayer(other.gameObject);
  }

  private void OnTriggerStay(Collider other)
  {
    if (IsNotJoinable(other.gameObject))
      return;

    TryJoinToPlayer(other.gameObject);
  }

  private void TryJoinToPlayer(GameObject go)
  {
    var element = GetElementToJoinFrom(go.GetComponent<JoinableElementView>()?.Parent);
    if (element == null)
      return;
    
    _player.Elements.AddRange(element.Parent.Elements);
    element.Parent.transform.SetParent(_player.transform);

    foreach (var newElement in element.Parent.Elements)
    {
      newElement.JoinToPlayer();
      newElement.gameObject.layer = _playerLayer;
    }
  }

  private bool IsNotJoinable(GameObject other) =>
    other.layer != _joinableLayer;

  private JoinableElementView GetElementToJoinFrom(JoinableView joinCandidate)
  {
    (JoinableElementView Element, float Distance) closest = default;

    foreach (var playerElement in _player.Elements)
    {
      foreach (var element in joinCandidate.Elements)
      {
        var curDistance = (playerElement.transform.position - element.transform.position).magnitude;
        if (closest.Element == null || curDistance < closest.Distance)
          closest = (element, curDistance);
      }
    }
    
    return closest.Element != null && closest.Distance < Threshold ? closest.Element : null;
  }
}
