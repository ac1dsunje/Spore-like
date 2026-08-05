using UnityEngine;

namespace _Game.Scripts.Player.Modules.Endurance
{
public class PlayerEndurance: MonoBehaviour
{
    private EnduranceModule _module;
    
    public void Construct(EnduranceModule module)
    {
        _module = module;
    }

    private void Update()
    {
        if (_module.IsUsed)
        {
            _module.UseEndurance(1f * Time.deltaTime);
        }
        else
        {
            _module.AddEndurance(_module.EnduranceRecovery * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
}