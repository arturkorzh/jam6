using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delaySec;
    [SerializeField] private GameObject _instance;

    private void OnEnable()
    {
        EventManager.OnItemCollected += TrySpawn;
        EventManager.OnEnemyDied += TrySpawn;
    }
    
    private void OnDisable()
    {
        EventManager.OnItemCollected -= TrySpawn;
        EventManager.OnEnemyDied -= TrySpawn;

    }
    
    private void Start()
    {
        _instance ??= Instantiate(_prefab, transform.position, transform.rotation);
        if (_instance.GetComponent<EnemyFollowerController>() is { } enemy)
            enemy.SpawnPosition = transform;
    }

    private void TrySpawn(GameObject go)
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
