using System.Collections.Generic;
using UnityEngine;

public class JoinableView : MonoBehaviour
{
  private const float Threshold = 0.00001f;
  private const float JoinSpeed = 6f;
  
  public List<JoinableElementView> Elements;
  
  private Rigidbody2D _body;

  private bool _needRemoveBody;
  private Vector3 _offset;
  
  private void Awake()
  {
    _body = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    RemoveBodyIfNeed();
    ApplyOffsetIfNeed();
  }

  private void RemoveBodyIfNeed()
  {
    if (!_needRemoveBody)
      return;
    
    DestroyImmediate(_body);

    _needRemoveBody = false;
  }

  private void ApplyOffsetIfNeed()
  {
    if (_offset.sqrMagnitude < Threshold)
      return;

    var curDelta = Vector3.Lerp(Vector3.zero, _offset, Time.deltaTime * JoinSpeed);
    transform.position += curDelta;
    
    _offset -= curDelta;
  }

  public void JoinToPlayer((JoinableElementView Element, JoinableElementView PlayerElement) closestPair)
  {
    _needRemoveBody = true;
    gameObject.layer = LayerMask.NameToLayer("Player");
    
    foreach (var newElement in Elements)
      newElement.JoinToPlayer();

    if (_offset.sqrMagnitude < Threshold)
      _offset = CalcOffset(closestPair.Element.Anchores, closestPair.PlayerElement.Anchores);
  }

  private Vector3 CalcOffset(Transform[] movable, Transform[] target)
  {
    var twoClosestIdx = new (int targetIdx, int movableIdx, float distance)[]
    {
      (0, 0, Mathf.Infinity),
      (0, 0, Mathf.Infinity),
    };
    
    for (var i = 0; i < target.Length; i++)
    {
      for (var j = 0; j < movable.Length; j++)
      {
        var distance = (target[i].position - movable[j].position).magnitude;
        if (twoClosestIdx[0].distance > distance)
        {
          twoClosestIdx[1] = twoClosestIdx[0];
          twoClosestIdx[0] = (i, j, distance);
        }
        else if (twoClosestIdx[1].distance > distance)
        {
          twoClosestIdx[1] = (i, j, distance);
        }
      }
    }

    foreach (var closestToCheck in twoClosestIdx)
    {
      var offset = target[closestToCheck.targetIdx].position - movable[closestToCheck.movableIdx].position;

      for (var i = 0; i < target.Length; i++)
      {
        for (var j = 0; j < movable.Length; j++)
        {
          if (i == closestToCheck.targetIdx && j == closestToCheck.movableIdx)
            continue;

          if ((target[i].position - (movable[j].position + offset)).sqrMagnitude < 0.001f)
            return offset;
        }
      }
    }
    
    return Vector3.zero;
  }
}
