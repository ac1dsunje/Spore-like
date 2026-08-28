using UnityEngine;

namespace _Game.Scripts.GamePlay.UI.Bar
{
[CreateAssetMenu(fileName = "NewBarConfig", menuName = "Configs/Game/Bars/Bar")]
public class BarConfig: ScriptableObject
{
    [field: SerializeField] public bool MaxValue { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}