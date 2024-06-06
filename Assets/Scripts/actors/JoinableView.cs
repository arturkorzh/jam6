using System.Collections.Generic;
using UnityEngine;

public class JoinableView : MonoBehaviour
{
  public List<JoinableElementView> Elements;
  
  private Rigidbody2D _body;
  private Collider2D _collider;
  
  private void Awake()
  {
    _body = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
  }

  public void JoinToPlayer(int layer)
  {
    gameObject.layer = layer;
    _body.isKinematic = true;
    //_body.simulated = false;
    
    foreach (var newElement in Elements)
      newElement.JoinToPlayer();
  }
}
