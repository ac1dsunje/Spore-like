using _Game.Scripts.UI;

namespace _Game.Scripts.Player.Modules.Endurance
{
public class EnduranceBarUI: BarUI
{
    private EnduranceModule _module;
    
    public void Construct(EnduranceModule module)
    {
        _module = module;
        _module.OnEnduranceChanged += UpdateBar;
    }

    private void OnDestroy()
    {
        _module.OnEnduranceChanged -= UpdateBar;
    }
}
}