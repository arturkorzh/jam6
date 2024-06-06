using UnityEngine;

public class JoinToPlayerController : MonoBehaviour
{
  [SerializeField] private JoinableView _player;

  private const float Threshold = 0.2f;
  
  private int _joinableLayer;

  private void Awake()
  {
    _joinableLayer = LayerMask.NameToLayer("Joinable");
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (IsNotJoinable(other.gameObject))
      return;

    TryJoinToPlayer(other.gameObject);
  }

  private void TryJoinToPlayer(GameObject go)
  {
    var closestPair = GetElementToJoinFrom(go.GetComponent<JoinableView>());
    if (!closestPair.HasValue)
      return;
    
    _player.Elements.AddRange(closestPair.Value.Element.Parent.Elements);
    closestPair.Value.Element.Parent.JoinToPlayer(closestPair.Value);
    closestPair.Value.Element.Parent.transform.SetParent(_player.transform);
  }

  private bool IsNotJoinable(GameObject other) =>
    other.layer != _joinableLayer;

  private (JoinableElementView Element, JoinableElementView PlayerElement)? GetElementToJoinFrom(JoinableView joinCandidate)
  {
    (JoinableElementView Element, JoinableElementView PlayerElement, float Distance) closest = default;

    foreach (var playerElement in _player.Elements)
    {
      foreach (var element in joinCandidate.Elements)
      {
        var curDistance = (playerElement.transform.position - element.transform.position).magnitude;
        if (closest.Element == null || curDistance < closest.Distance)
          closest = (element, playerElement, curDistance);
      }
    }
    
    return closest.Element != null && closest.Distance < Threshold ? (closest.Element, closest.PlayerElement) : null;
  }
}
