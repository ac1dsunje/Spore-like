using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Interfaces;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinScope: EntityScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);

        builder.RegisterEntryPoint<SeaUrchinAI>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<CombatBinder>(Lifetime.Scoped);
        
        builder.RegisterComponent(GetComponent<SeaUrchinHealth>()).AsSelf().As<IDamageReceiverController>();
        builder.RegisterEntryPoint<SeaUrchinAttackBehaviour>().AsSelf().As<IDamageSource>().As<IDamageSourceController>();
    }
}
}