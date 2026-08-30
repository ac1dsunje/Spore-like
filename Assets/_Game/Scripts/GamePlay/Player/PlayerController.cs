using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Player.Modules.Experience;
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
    [Inject] private ExperienceModule _experience;

    public void Initialize()
    {
        Model.Initialize();
        _animation.SetConfig(_animationSettings);
        _playerRegistry.AddPlayer(this);
        _experience.Initialize(Model);
    }
}
}