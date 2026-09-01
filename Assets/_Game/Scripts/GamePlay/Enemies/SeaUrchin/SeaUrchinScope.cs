using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Animation;
using _Game.Scripts.GamePlay.Entities.Movement;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinScope: EntityScope
{
    [SerializeField] private EntityStatsConfig _entityStatsConfig;
    [SerializeField] private AnimationSettings _animationSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.RegisterInstance(_entityStatsConfig);
        builder.RegisterInstance(_animationSettings);

        builder.RegisterEntryPoint<SeaUrchinAI>(Lifetime.Scoped);

        builder.RegisterEntryPoint<SeaUrchinController>();
        builder.RegisterEntryPoint<EntityBasicMovement>().As<IMovementController>();
        
        builder.RegisterComponent(GetComponent<SeaUrchinHealth>()).AsSelf().As<IDamageReceiver>().As<IDamageReceiverController>();
        builder.RegisterComponent(GetComponent<SeaUrchinAttackBehaviour>()).AsSelf().As<IDamageSource>().As<IDamageSourceController>();
        
        builder.RegisterComponent(GetComponentInChildren<RigidbodyController>());
        
        builder.RegisterComponent(GetComponentInChildren<EntityAnimation>());
        builder.Register<CombatBinder>(Lifetime.Scoped);

        builder.Register<DefenseModule>(Lifetime.Scoped);
        builder.Register<HealthModule>(Lifetime.Scoped);
        builder.Register<AttackModule>(Lifetime.Scoped);
        builder.Register<MovementModule>(Lifetime.Scoped);
        builder.Register<DisguiseModule>(Lifetime.Scoped);
    }
}
}