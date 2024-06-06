using UnityEngine;

public class JoinableElementView : MonoBehaviour
{
    public JoinableView Parent;

    private SpriteRenderer _renderer;
    

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void JoinToPlayer()
    {
        _renderer.material.color = Color.blue;
    }
}
