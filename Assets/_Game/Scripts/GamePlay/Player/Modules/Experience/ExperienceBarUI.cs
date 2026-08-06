using _Game.Scripts.GamePlay.UI;

namespace _Game.Scripts.GamePlay.Player.Modules.Experience
{
public class ExperienceBarUI: BarUI
{
    private ExperienceController _module;
    private int _experience;
    private int _set;
    
    public void Construct(ExperienceController module)
    {
        _module = module;
        
        _module.OnExperienceChanged += UpdateExperience;
        _module.OnLevelSetChanged += UpdateLevelSet;

        _experience = _module.Experience;
        _set = _module.LevelSet;
        
        UpdateBar(_experience, _set);
    }
    
    private void UpdateExperience(int amount)
    {
        _experience = amount;
        UpdateBar(_experience, _set);
    }

    private void UpdateLevelSet(int amount)
    {
        _set = amount;
        UpdateBar(_experience, _set);
    }

    private void OnDestroy()
    {
        _module.OnExperienceChanged -= UpdateExperience;
        _module.OnLevelSetChanged -= UpdateLevelSet;
    }
}
}