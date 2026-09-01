using _Game.Scripts.GamePlay.Entities.Animation;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySpawner: MonoBehaviour
{
    [SerializeField] private EntityScope _prefab;
    [SerializeField] private Vector2 _spawnPoint;
    [SerializeField] private EntityStatsConfig _entityStatsConfig;
    [SerializeField] private AnimationSettings _animationSettings;
    
    private void Awake()
    {
        Spawn();
    }
    
    [ContextMenu("Spawn")]
    protected void Spawn()
    {
        var entity = Instantiate(_prefab, _spawnPoint, Quaternion.identity, transform);
        entity.SetAnimationSetting(_animationSettings);
        entity.SetStatsSettings(_entityStatsConfig);
        entity.Build();
    }
}
}