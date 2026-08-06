using _Game.Scripts.GamePlay.UI;

namespace _Game.Scripts.GamePlay.Player.Modules.Endurance
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