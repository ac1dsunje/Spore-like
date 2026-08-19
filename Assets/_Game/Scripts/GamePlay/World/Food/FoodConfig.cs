using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Entity;
using UnityEngine;

namespace _Game.Scripts.GamePlay.World.Food
{
[CreateAssetMenu(fileName = "New Food Config", menuName = "Configs/Game/Food/Config")]
public class FoodConfig: ScriptableObject
{
    [field: SerializeField] public StatsConfig StatsConfig { get; private set; }
    [field: SerializeField] public int FeedAmount { get; private set; } = 1;
    [field: SerializeField] public AnimationSettings AnimationSettings { get; private set; }
    [field: SerializeField] public ParticlesSettings ParticlesSettings { get; private set; }
}
}