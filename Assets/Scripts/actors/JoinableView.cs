using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockType
{
    S,
    T,
    L,
    I,
    O,
}

public enum BuffType
{
    Red,
    Orange,
    Blue,
    Green,
    Yellow,
    Pink,
}

public class JoinableView : MonoBehaviour
{
    public List<JoinableElementView> Elements;

    private Rigidbody2D _body;
    private Collider2D _collider;

    private bool _needRemoveBody;
    public int BlockType = 1;
    public int BuffType = 1;

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