using _Game.Scripts.GamePlay.Module;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies
{
public class EnemyScope: LifetimeScope
{
    [SerializeField] private EntityStatsConfig _entityStatsConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_entityStatsConfig);
        
        builder.RegisterComponent(GetComponent<EnemyController>());

        builder.Register<DefenseModule>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<AttackModule>(Lifetime.Scoped);
        
        builder.Register<EntityStats>(Lifetime.Scoped);
    }
}
}