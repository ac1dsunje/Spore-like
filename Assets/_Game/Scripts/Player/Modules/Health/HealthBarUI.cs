using _Game.Scripts.UI;

namespace _Game.Scripts.Player.Modules.Health
{
public class HealthBarUI: BarUI
{
    private HealthModule _module;
    
    public void Construct(HealthModule module)
    {
        _module = module;
        _module.OnHealthChanged += UpdateBar;
    }

    private void OnDestroy()
    {
        _module.OnHealthChanged -= UpdateBar;
    }
}
}