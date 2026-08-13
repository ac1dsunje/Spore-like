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
    [Inject] private AnimationConfig _animationConfig;

    public void Initialize()
    {
        Model.Initialize(this);
        Model.Evolutions.Initialize(_evolutionsDatabase, _raritiesDatabase, _minEvolutions);
        Animation.SetConfig(_animationConfig);
    }

    public void TakeDamage(HitInfo hit) => Model.TakeDamage(hit);
}
}