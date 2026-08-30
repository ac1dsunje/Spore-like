using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
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

        builder.RegisterEntryPoint<SeaUrchinController>();
        builder.RegisterEntryPoint<SeaUrchinMovement>();
        
        builder.RegisterComponent(GetComponent<SeaUrchinHealth>()).AsSelf().As<IDamageReceiver>().As<IDamageReceiverController>();
        builder.RegisterComponent(GetComponent<SeaUrchinAttackBehaviour>()).AsSelf().As<IDamageSource>().As<IDamageSourceController>();
        
        builder.RegisterComponent(GetComponentInChildren<MovementController>());
        
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.Register<CombatBinder>(Lifetime.Scoped);

        builder.Register<DefenseModule>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<AttackModule>(Lifetime.Scoped);
        builder.Register<MovementModule>(Lifetime.Scoped);
        
        builder.Register<EntityStats>(Lifetime.Scoped);
    }
}
}