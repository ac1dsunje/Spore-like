using UnityEngine;

namespace _Game.Scripts.Evolutions.Types.Vision.CompoundEyes
{
[CreateAssetMenu(fileName = "New CompoundEyes Config", menuName = "Configs/Game/Evolutions/Vision/Compound Eyes")]
public class CompoundEyesConfig: EvolutionConfig
{
    public override Evolution CreateEvolution()
    {
        var evo = new CompoundEyes();
        evo.SetConfig(this);
        return evo;
    }
}
}