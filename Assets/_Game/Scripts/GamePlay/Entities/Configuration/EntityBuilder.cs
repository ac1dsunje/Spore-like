using _Game.Scripts.GamePlay.Enemies.SeaUrchin;
using _Game.Scripts.GamePlay.Entities.Attack;
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
        
        SetRegeneration(config.RegenerationType, builder);
        
        SetAttack(config.AttackType, builder);
        
        SetDeath(config.DeathType, builder);
    }

    private void SetAI(EntityAI config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityAI.Food:
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

    protected void SetHealth(EntityHealth config, IContainerBuilder builder)
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

    private void SetRegeneration(EntityRegeneration config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityRegeneration.Disabled:
                break;
            
            case EntityRegeneration.Enabled:
                builder.RegisterEntryPoint<EntityRegeneration>(Lifetime.Scoped);
                break;
        }
    }

    private void SetAttack(EntityAttack config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityAttack.Basic:
                builder.RegisterEntryPoint<EntityBasicAttackBehaviour>().As<IDamageSource>().As<IAttackController>();
                break;
            case EntityAttack.Player:
                builder.RegisterEntryPoint<PlayerAttack>().As<IDamageSource>().As<IAttackController>();
                break;
        }
    }

    private void SetDeath(EntityDeath config, IContainerBuilder builder)
    {
        switch (config)
        {
            case EntityDeath.Basic:
                builder.RegisterEntryPoint<EntityDeathModule>();
                break;
            case EntityDeath.Player:
                break;
        }
    }
}
}