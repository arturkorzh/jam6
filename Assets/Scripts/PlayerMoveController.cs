using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody _body;
    private float _speedCoef = 2f;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        var to = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _body.velocity = to * _speedCoef;
    }
}
