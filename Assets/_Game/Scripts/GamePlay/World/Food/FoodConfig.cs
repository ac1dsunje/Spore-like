using _Game.Scripts.GamePlay.Animation;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Food
{
[CreateAssetMenu(fileName = "New Food Config", menuName = "Configs/Game/Food/Config")]
public class FoodConfig: ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 10;
    [field: SerializeField] public float Shield { get; private set; }
    [field: SerializeField] public int FeedAmount { get; private set; } = 1;
    [field: SerializeField] public Color Color { get; private set; } = Color.green;
    [field: SerializeField] public AnimationConfig AnimationConfig { get; private set; }
    [field: SerializeField] public ParticleSystem ParticlesPrefab { get; private set; }
}
}