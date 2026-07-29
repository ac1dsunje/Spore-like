using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Player.Modules.Experience
{
public class ExperienceBarUI: MonoBehaviour
{
    [SerializeField] private Image _bar;

    private ExperienceController _module;
    private int _experience;
    private int _set;
    
    public void Construct(ExperienceController module)
    {
        _module = module;
        
        _module.OnExperienceChanged += UpdateExperience;
        _module.OnLevelSetChanged += UpdateLevelSet;

        _set = _module.LevelSet;
    }
    
    private void UpdateExperience(int amount)
    {
        _experience = amount;
        UpdateUI();
    }

    private void UpdateLevelSet(int amount)
    {
        _set = amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _bar.fillAmount = (float)_experience / _set;
    }

    private void OnDestroy()
    {
        _module.OnExperienceChanged -= UpdateExperience;
        _module.OnLevelSetChanged -= UpdateLevelSet;
    }
}
}