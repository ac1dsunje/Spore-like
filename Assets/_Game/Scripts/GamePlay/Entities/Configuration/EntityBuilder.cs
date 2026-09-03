using _Game.Scripts.GamePlay.Enemies.SeaUrchin;
using _Game.Scripts.GamePlay.Entities.Attack;
using _Game.Scripts.GamePlay.Entities.Death;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.World.Biomes.Plant;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
public class EntityBuilder
{
    public void ChooseBehaviour(EntityData config, IContainerBuilder builder)
    {
        SetAI(config.AIType, builder);
        
        SetHealth(config.HealthType, builder);
        
        SetAttack(config.AttackType, builder);
        
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

    private void SetHealth(EntityHealth config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityHealth.Basic:
                builder.RegisterEntryPoint<EntityBasicHealth>().As<IHealthController>();
                break;
            
            case EntityHealth.Reflective:
                builder.RegisterEntryPoint<EntityReflectiveHealth>().As<IHealthController>();
                break;
        }
    }

    private void SetAttack(EntityAttack config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityAttack.Basic:
                builder.RegisterEntryPoint<EntityBasicAttack>().As<IDamageSource>().As<IAttackController>();
                break;
            case EntityAttack.Weapon:
                builder.RegisterEntryPoint<EntityWeaponAttack>().As<IDamageSource>().As<IAttackController>();
                break;
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