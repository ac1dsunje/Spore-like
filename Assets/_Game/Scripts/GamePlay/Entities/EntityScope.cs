using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public abstract class EntityScope: LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EntityStats>(Lifetime.Scoped);
    }
}
}