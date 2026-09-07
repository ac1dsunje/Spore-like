using _Game.Scripts.GamePlay.Entities.AIs;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
public class EntityBuilder
{
    public void ChooseBehaviour(EntityAIType aiType, IContainerBuilder builder)
    {
        switch (aiType)
        {
            case EntityAIType.Entity:
                builder.RegisterEntryPoint<EntityAI>(Lifetime.Scoped);
                break;
            
            case EntityAIType.Player:
                builder.RegisterEntryPoint<PlayerAI>(Lifetime.Scoped);
                break;
        }
    }
}
}