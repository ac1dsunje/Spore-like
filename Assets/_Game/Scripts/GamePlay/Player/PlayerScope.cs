using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerScope: LifetimeScope
{
    [SerializeField] private PlayerConfig _playerConfig;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_playerConfig);
        
        builder.RegisterComponentInHierarchy<PlayerController>();
        builder.Register<PlayerModel>(Lifetime.Scoped);
        builder.Register<PlayerStats>(Lifetime.Scoped);
    }
}
}