using UnityEngine;

public class ObstacleDamageController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsNotPlayer(other.gameObject))
            return;

        EventManager.AddPlayerDamage(other.gameObject.GetComponent<JoinableView>());
        switch (other.gameObject.GetComponent<JoinableView>().BuffType)
        {
            case 0:
            {
                EventManager.SpeedModified(false);
                break;
            }
            case 1:
            {
                break;
            }
        }
    }

    private bool IsNotPlayer(GameObject other) =>
        other.layer != LayerMasks.Player;
}