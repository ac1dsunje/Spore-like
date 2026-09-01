using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySpawner: MonoBehaviour
{
    [SerializeField] private EntityScope _prefab;
    [SerializeField] private Vector2 _spawnPoint;
    [SerializeField] private EntityConfig _entityConfig;
    
    private void Awake()
    {
        Spawn();
    }
    
    [ContextMenu("Spawn")]
    protected void Spawn()
    {
        var entity = Instantiate(_prefab, _spawnPoint, Quaternion.identity, transform);
        entity.SetConfig(_entityConfig);
        entity.Build();
    }
}
}