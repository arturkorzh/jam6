using UnityEngine;
using UnityEngine.Serialization;

public class PlayerView : MonoBehaviour
{
  public static PlayerView Instance;
  
  [FormerlySerializedAs("_joinable")]
  public JoinableView Joinable;

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
    if (toRemove == Joinable)
    {
      EventManager.Lose();
      return;
    }

    foreach (var removeElement in toRemove.Elements)
      Joinable.Elements.Remove(removeElement);
    
    Destroy(toRemove.gameObject);
  }
}
