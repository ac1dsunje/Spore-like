using UnityEngine;

namespace _Game.Scripts.Evolutions.Types.Spines
{
[CreateAssetMenu(fileName = "New Spines Config", menuName = "Configs/Game/Evolutions/Spines")]
public class SpinesConfig : EvolutionConfig
{
    public override Evolution CreateEvolution() => new Spines(this);
}
}