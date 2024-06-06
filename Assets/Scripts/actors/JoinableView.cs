using System.Collections.Generic;
using UnityEngine;

public class JoinableView : MonoBehaviour
{
  public List<JoinableElementView> Elements;
  
  private Rigidbody2D _body;
  private Collider2D _collider;

  private bool _needRemoveBody;
  
  private void Awake()
  {
    _body = GetComponent<Rigidbody2D>();
    _collider = GetComponent<Collider2D>();
  }

  private void Update()
  {
    if (!_needRemoveBody)
      return;
    
    DestroyImmediate(_body);

    _needRemoveBody = false;
  }

  public void JoinToPlayer(int layer)
  {
    _needRemoveBody = true;
    gameObject.layer = layer;
    
    foreach (var newElement in Elements)
      newElement.JoinToPlayer();
  }
}
