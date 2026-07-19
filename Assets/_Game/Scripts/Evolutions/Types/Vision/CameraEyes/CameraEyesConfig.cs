using UnityEngine;
namespace _Game.Scripts.Evolutions.Types.Vision.CameraEyes
{
[CreateAssetMenu(fileName = "New CameraEyes Config", menuName = "Configs/Game/Evolutions/Vision/Camera Eyes")]
public class CameraEyesConfig: EvolutionConfig
{
    public override Evolution CreateEvolution()
    {
        var evo = new CameraEyes();
        evo.SetConfig(this);
        return evo;
    }
}
}