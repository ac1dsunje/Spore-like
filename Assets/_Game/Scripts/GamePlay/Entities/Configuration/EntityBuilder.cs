using _Game.Scripts.GamePlay.Entities.AIs;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Death;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Projectiles;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
public class EntityBuilder
{
    public void ChooseBehaviour(EntityData config, IContainerBuilder builder, ProjectileConfig projectileConfig)
    {
        SetAI(config.AIType, builder);
        
        SetAttack(projectileConfig, builder);
        
        SetDeath(config.DeathType, builder);
    }

    private void SetAI(EntityAI config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityAI.Plant:
                builder.RegisterEntryPoint<PlantAI>(Lifetime.Scoped);
                break;
            
            case EntityAI.Player:
                builder.RegisterEntryPoint<PlayerAI>(Lifetime.Scoped);
                builder.RegisterEntryPoint<PlayerVision>(Lifetime.Scoped);
                break;
            
            case EntityAI.SeaUrchin:
                builder.RegisterEntryPoint<SeaUrchinAI>(Lifetime.Scoped);
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
            builder.RegisterEntryPoint<EntityBasicAttack>();
        }
    }

    private void SetDeath(EntityDeath config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityDeath.Basic:
                builder.RegisterEntryPoint<EntityBasicDeath>();
                break;
            case EntityDeath.Revival:
                builder.RegisterEntryPoint<EntityRevivalDeath>();
                break;
        }
    }
}
}