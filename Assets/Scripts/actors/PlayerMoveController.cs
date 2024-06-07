using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody2D _body;
    public float _speedCoef = 0.75f * 5;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var to = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _body.MovePosition(transform.position + to * Time.deltaTime * _speedCoef);
    }
}