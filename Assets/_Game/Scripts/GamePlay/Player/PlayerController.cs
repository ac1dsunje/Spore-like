using _Game.Scripts.GamePlay.Entities;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : EntityController
{
    [Inject] private PlayerRegistry _playerRegistry;
    
    public override void Start()
    {
        base.Start();
        _playerRegistry.AddPlayer(this);
    }
}
}