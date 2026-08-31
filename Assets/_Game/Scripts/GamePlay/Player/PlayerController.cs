using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Animation;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : IStartable
{
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] public EntityBuffsModule Buffs { get; private set; }
    
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private EntityAnimation _animation;

    public void Start()
    {
        Model.Initialize();
        _animation.SetConfig(_animationSettings);
        _playerRegistry.AddPlayer(this);
        
        Buffs.Initialize();
    }
}
}