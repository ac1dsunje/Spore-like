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
        switch (config.AIType)
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

        switch (config.HealthType)
        {
            case EntityHealth.Basic:
                builder.RegisterEntryPoint<EntityBasicHealth>().As<IHealthController>();
                break;
            
            case EntityHealth.Reflective:
                builder.RegisterEntryPoint<EntityReflectiveHealth>().As<IHealthController>();
                break;
        }

        switch (config.RegenerationType)
        {
            case EntityRegeneration.Disabled:
                break;
            
            case EntityRegeneration.Enabled:
                builder.RegisterEntryPoint<EntityRegeneration>(Lifetime.Scoped);
                break;
        }

        switch (config.AttackType)
        {
            case EntityAttack.Basic:
                builder.RegisterEntryPoint<EntityBasicAttackBehaviour>().As<IDamageSource>().As<IAttackController>();
                break;
            case EntityAttack.Player:
                builder.RegisterEntryPoint<PlayerAttack>().As<IDamageSource>().As<IAttackController>();
                break;
        }

        switch (config.DeathType)
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