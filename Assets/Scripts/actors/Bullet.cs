using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        var dist = Vector3.Distance(startPosition, transform.position);
        if (dist > 10f) 
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    }
}