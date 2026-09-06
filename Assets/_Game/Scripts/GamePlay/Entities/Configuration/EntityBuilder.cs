using _Game.Scripts.GamePlay.Entities.AIs;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Projectiles;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
public class EntityBuilder
{
    public void ChooseBehaviour(EntityAIType aiTypeType, IContainerBuilder builder, ProjectileConfig projectileConfig)
    {
        SetAI(aiTypeType, builder);
        
        SetAttack(projectileConfig, builder);
    }

    private void SetAI(EntityAIType aiTypeType, IContainerBuilder builder)
    {
        switch (aiTypeType)
        {
            case EntityAIType.Entity:
                builder.RegisterEntryPoint<EntityAI>(Lifetime.Scoped);
                break;
            
            case EntityAIType.Player:
                builder.RegisterEntryPoint<PlayerAI>(Lifetime.Scoped);
                builder.RegisterEntryPoint<PlayerVision>(Lifetime.Scoped);
                break;
        }
    }

    private void SetAttack(ProjectileConfig config, IContainerBuilder builder)
    {
        
        if (config != null)
        {
            builder.RegisterInstance(config);
            builder.RegisterEntryPoint<EntityWeaponAttack>().As<IDamageSource>().As<IAttackController>();
        }
        else
        {
            builder.RegisterEntryPoint<EntityEmptyAttack>();
        }
    }
}
}