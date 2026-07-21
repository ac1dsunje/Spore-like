using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Types.Vision.CameraEyes
{
public class CameraEyes: Evolution
{
    public CameraEyes(EvolutionConfig config) : base(config) {}

    public override void Apply(PlayerStats playerStats)
    {
        base.Apply(playerStats);
        Player.OnExperienceGained += UpdateExperience;
    }

    public override void Dispose()
    {
        if (Player != null) 
            Player.OnExperienceGained -= UpdateExperience;
    }
}
}