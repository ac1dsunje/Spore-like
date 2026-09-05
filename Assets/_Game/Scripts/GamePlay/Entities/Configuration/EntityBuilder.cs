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
    public void ChooseBehaviour(EntityAI aiType, IContainerBuilder builder, ProjectileConfig projectileConfig)
    {
        SetAI(aiType, builder);
        
        SetAttack(projectileConfig, builder);
    }

    private void SetAI(EntityAI aiType, IContainerBuilder builder)
    {
        switch (aiType)
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
}
}