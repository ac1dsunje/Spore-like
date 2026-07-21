using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Types.Vision.EyeSpots
{
[CreateAssetMenu(fileName = "New EyeSpotsConfig", menuName = "Configs/Game/Evolutions/Vision/Eye Spots")]
public class EyeSpotsConfig: EvolutionConfig
{
    public override Evolution CreateEvolution() => new EyeSpots(this);
}
}