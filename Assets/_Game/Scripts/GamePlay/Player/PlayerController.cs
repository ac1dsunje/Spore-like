using _Game.Scripts.GamePlay.Animation;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerController : MonoBehaviour
{
    [Inject] public PlayerModel Model { get; private set; }
    [Inject] private AnimationSettings _animationSettings;
    [Inject] private PlayerRegistry _playerRegistry;
    [Inject] private ItemAnimation _animation;

    public void Initialize()
    {
        Model.Initialize();
        _animation.SetConfig(_animationSettings);
        _playerRegistry.AddPlayer(this);
    }
}
}