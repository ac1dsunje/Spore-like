using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Player.Modules;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : EntityController
{
    [Inject] public EntityBuffsModule Buffs { get; private set; }
    [Inject] private PlayerRegistry _playerRegistry;
    
    [Inject] public EvolutionsModule Evolutions { get; private set; }
    [Inject] public AbilitiesModule Abilities { get; private set; }

    public override void Start()
    {
        base.Start();
        Buffs.Initialize();
        Abilities.SetModel(Model);
        Evolutions.SetModel(Model);
        _playerRegistry.AddPlayer(this);
    }
}
}