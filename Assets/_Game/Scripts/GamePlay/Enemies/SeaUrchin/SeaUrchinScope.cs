using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Entity;
using _Game.Scripts.GamePlay.Entity.Module;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinScope: LifetimeScope
{
    [SerializeField] private EntityStatsConfig _entityStatsConfig;
    [SerializeField] private AnimationSettings _animationSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_animationSettings);
        
        builder.RegisterComponent(GetComponent<SeaUrchinController>());
        builder.RegisterComponent(GetComponent<SeaUrchinAttackBehaviour>());
        builder.RegisterComponent(GetComponent<ItemAnimation>());

        builder.Register<DefenseModule>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<AttackModule>(Lifetime.Scoped);
        
        builder.Register<EntityStats>(Lifetime.Scoped);
    }
}
}