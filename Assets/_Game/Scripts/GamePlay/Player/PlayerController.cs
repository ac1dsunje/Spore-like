using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Player.Modules.Endurance;
using _Game.Scripts.GamePlay.Player.Modules.Health;
using _Game.Scripts.GamePlay.Player.Modules.Mouth;
using _Game.Scripts.GamePlay.Player.Modules.Movement;
using _Game.Scripts.GamePlay.Player.Modules.Vision;
using _Game.Scripts.GamePlay.Rarities;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    [Header("Config")]
    [SerializeField] private PlayerConfig _playerConfig;
    [Header("Modules")]
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public PlayerHealth Health { get; private set; }
    [field: SerializeField] public PlayerVision Vision { get; private set; }
    [field: SerializeField] public PlayerMouth Mouth { get; private set; }
    [field: SerializeField] public PlayerEndurance Endurance { get; private set; }
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    public PlayerModel Model { get; private set; }

    [Inject] private Ticker _ticker;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private WorldModel _worldModel;

    private Biome _currentBiome;

    public void Initialize()
    {
        CreateModel(_ticker);
        InitializeActiveModules();
        _playerRegistry.NotifyPlayerAdded(this);

        Movement.OnGridPositionChanged += TryEnterBiome;
        EnterBiome(_worldModel.GetBiome(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0)));
    }

    private void TryEnterBiome(PlayerMovement player, Vector3Int position)
    {
        var currentBiome = _worldModel.GetBiome(position);
        if (currentBiome == _currentBiome) return;
        EnterBiome(currentBiome);
    }

    private void EnterBiome(Biome biome)
    {
        _currentBiome = biome;
        Debug.Log("Entering biome: " + biome.Name);

        ApplyTemperature(biome.Temperature);
    }

    private void ApplyTemperature(float temperature)
    {
        if (Model.Temperature.IsLethal(temperature))
        {
            Model.Health.TakeDamage(Model.Health.MaxHealth);
            Debug.Log($"Temperature {temperature} is lethal");
        }
        else if (Model.Temperature.IsUncomfortable(temperature))
        {
            Debug.Log($"Temperature {temperature} is not comfortable");
        }
        else
        {
            Debug.Log($"Temperature {temperature} is comfortable");
        }
    }

    private void CreateModel(Ticker ticker)
    {
        Model = new(_playerConfig);
        
        var abilityFactory = new AbilityFactory(Model, ticker);
        Model.Abilities.SetFactory(abilityFactory);
        
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
    }

    private void InitializeActiveModules()
    {
        Vision.Construct(Model.Vision);
        Movement.Construct(Model.Movement);
        Mouth.Construct(Model.MouthModule);
        Health.Construct(Model.Health);
        Endurance.Construct(Model.Endurance);
    }

    public void TakeDamage(float value, IDamageAble damager)
    {
        var amount = Model.Defense.ApplyResistance(value);
        
        Model.Health.TakeDamage(amount);
        Model.Defense.ReflectDamage(amount, damager);
    }

    private void OnDestroy()
    {
        Model.Dispose();
        Movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}