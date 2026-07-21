using UnityEngine;

namespace _Game.Scripts.Evolutions.Types.Vision.CompoundEyes
{
[CreateAssetMenu(fileName = "New CompoundEyes Config", menuName = "Configs/Game/Evolutions/Vision/Compound Eyes")]
public class CompoundEyesConfig: EvolutionConfig
{
    public override Evolution CreateEvolution() => new CompoundEyes(this);
}
}