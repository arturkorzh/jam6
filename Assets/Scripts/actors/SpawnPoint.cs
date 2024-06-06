using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delaySec;
    [SerializeField] private GameObject _instance;

    private void OnEnable()
    {
        EventManager.OnItemCollected += OnItemCollected;
    }
    
    private void OnDisable()
    {
        EventManager.OnItemCollected -= OnItemCollected;
    }
    
    private void Start()
    {
        _instance ??= Instantiate(_prefab, transform.position, transform.rotation);
    }

    private void OnItemCollected(GameObject go)
    {
        if (_instance != go)
            return;

        _instance = null;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        do
        {
            yield return new WaitForSeconds(_delaySec);
        } while (IsSpawnWithCollision());
            
        Start();
    }

    private bool IsSpawnWithCollision()
    {
       var playerBounds = GetBoundsOf(PlayerView.Instance.gameObject);
       var instanceBounds = GetBoundsOf(_prefab);
       instanceBounds.center = transform.position;
       //instanceBounds.Expand(Vector3.one);

       return playerBounds.Intersects(instanceBounds);
    }

    private Bounds GetBoundsOf(GameObject go)
    {
        var renderers = go.GetComponentsInChildren<Renderer>();
        var bounds = renderers[0].bounds;
 
        for (var i = 1; i < renderers.Length; i++)
            bounds.Encapsulate(renderers[i].bounds);

        return bounds;
    }
}
