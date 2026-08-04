using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Endurance
{
public class PlayerEndurance: MonoBehaviour
{
    private EnduranceModule _module;
    
    public void Construct(EnduranceModule module)
    {
        _module = module;
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _module.AddEndurance(_module.EnduranceRecovery);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
}