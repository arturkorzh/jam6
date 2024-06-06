using UnityEngine;

public class PlayerView : MonoBehaviour
{
  public static PlayerView Instance;
  
  [SerializeField] private JoinableView _joinable;

  private void Awake()
  {
    Instance = this;
  }
  
  private void OnEnable()
  {
    EventManager.OnPlayerGetDamage += ApplyDamage;
  }
  
  private void OnDisable()
  {
    EventManager.OnPlayerGetDamage -= ApplyDamage;
  }

  private void ApplyDamage(JoinableView toRemove)
  {
    if (toRemove == _joinable)
    {
      EventManager.Lose();
      return;
    }

    foreach (var removeElement in toRemove.Elements)
      _joinable.Elements.Remove(removeElement);
    
    Destroy(toRemove.gameObject);
  }
}
