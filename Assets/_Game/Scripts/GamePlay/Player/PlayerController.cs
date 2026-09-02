using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : IStartable
{
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] public EntityBuffsModule Buffs { get; private set; }
    [Inject] private PlayerRegistry _playerRegistry;
    
    [Inject] public EvolutionsModule Evolutions { get; private set; }
    [Inject] public ExperienceModule Experience { get; private set; }
    [Inject] public AbilitiesModule Abilities { get; private set; }

    public void Start()
    {
        Buffs.Initialize();
        _playerRegistry.AddPlayer(this);
        Model.Stats.Initialize();
        Abilities.SetModel(Model);
        Evolutions.SetModel(Model);
        Experience.Initialize(Model);
    }
}
}