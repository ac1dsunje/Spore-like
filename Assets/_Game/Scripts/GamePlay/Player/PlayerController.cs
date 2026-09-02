using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Player.Modules;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : EntityController
{
    [Inject] public EvolutionsModule Evolutions { get; private set; }
    [Inject] private PlayerRegistry _playerRegistry;
    

    public override void Start()
    {
        base.Start();
        Evolutions.SetModel(Model);
        _playerRegistry.AddPlayer(this);
    }
}
}