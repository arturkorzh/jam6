using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsNotPlayer(other.gameObject))
            return;

        EventManager.AddPlayerDamage(other.gameObject.GetComponent<JoinableView>());
        EventManager.EnemyDied(gameObject);
    }
  
    private bool IsNotPlayer(GameObject other) =>
        other.layer != LayerMasks.Player;
}
