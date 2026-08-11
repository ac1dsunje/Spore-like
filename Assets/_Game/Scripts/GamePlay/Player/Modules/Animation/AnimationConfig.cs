using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Animation
{
[CreateAssetMenu(fileName = "New Animation Config", menuName = "Configs/Game/Animation")]
public class AnimationConfig: ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
}
}