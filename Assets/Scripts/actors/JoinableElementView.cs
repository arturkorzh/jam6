using UnityEngine;

public class JoinableElementView : MonoBehaviour
{
    public JoinableView Parent;

    private MeshRenderer _renderer;
    private Rigidbody _body;
    private Collider _collider;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _body = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void JoinToPlayer()
    {
        _renderer.material.color = Color.blue;
        //_body.useGravity = true;
        _body.isKinematic = false;
        _collider.isTrigger = false;
    }
}
