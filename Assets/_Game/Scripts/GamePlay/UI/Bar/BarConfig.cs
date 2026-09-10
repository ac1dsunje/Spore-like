using UnityEngine;

namespace _Game.Scripts.GamePlay.UI.Bar
{
public enum BarType
{
    Health,
    Experience,
    Endurance,
    Hunger,
}

[CreateAssetMenu(fileName = "NewBarConfig", menuName = "Game/Bars/Bar")]
public class BarConfig: ScriptableObject
{
    [field: SerializeField] public BarType BarType { get; private set; }
    [field: SerializeField] public bool MaxValue { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}