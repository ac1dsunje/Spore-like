using _Game.Scripts.GamePlay.UI;

namespace _Game.Scripts.GamePlay.Player.Modules.Health
{
public class HealthBarUI: BarUI
{
    private IHealth _module;
    
    public void Construct(IHealth module)
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