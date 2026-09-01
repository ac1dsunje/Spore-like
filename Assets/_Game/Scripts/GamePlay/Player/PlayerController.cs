using _Game.Scripts.GamePlay.Entities;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : IStartable
{
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] public EntityBuffsModule Buffs { get; private set; }
    [Inject] private PlayerRegistry _playerRegistry;

    public void Start()
    {
        Model.Initialize();
        _playerRegistry.AddPlayer(this);
        
        Buffs.Initialize();
    }
}
}