using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Player.Modules;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController: MonoBehaviour, IDamageAble
{
    [Header("Modules")]
    [field: SerializeField] public ItemAnimation Animation { get; private set; }
    [Header("Evolutions")]
    [SerializeField] private EvolutionsDatabase _evolutionsDatabase;
    [SerializeField] private RaritiesDatabase _raritiesDatabase;
    [SerializeField] private int _minEvolutions;
    
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] private PlayerConfig _playerConfig;
    [Inject] private AnimationConfig _animationConfig;

    [Inject] private AbilityFactory _abilityFactory;

    public void Initialize()
    {
        CreateModel();
        InitializeActiveModules();
    }

    private void CreateModel()
    {
        Model.Attack.SetOwner(this);
        Model.Abilities.SetModel(Model);
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
    }

    private void InitializeActiveModules()
    {
        Animation.SetConfig(_animationConfig);
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = Model.Defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        Model.Health.TakeDamage(damage);
        var returnedDamage = Model.Defense.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, Model.Attack.IgnoreResistance, null);
        hit.Owner?.TakeDamage(returnedHit);
    }

    private void OnDestroy()
    {
        Model.Dispose();
    }
}
}