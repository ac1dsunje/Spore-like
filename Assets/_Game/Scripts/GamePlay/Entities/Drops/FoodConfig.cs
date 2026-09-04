using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Drops
{
[CreateAssetMenu(fileName = "New food Config", menuName = "Game/Foods/Food")]
public class FoodConfig: ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}