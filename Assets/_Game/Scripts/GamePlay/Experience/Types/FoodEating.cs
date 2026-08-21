using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class FoodEating: ExperienceService
{
    private readonly MouthModule _module;
    
    public FoodEating(MouthModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnFoodPointsAchieved += AddAmount;
    }

    public override void Dispose() => _module.OnFoodPointsAchieved -= AddAmount;
}
}