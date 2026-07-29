using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Health
{
public class PlayerHealth: MonoBehaviour
{
    private HealthModule _module;
    
    public void Construct(HealthModule module)
    {
        _module = module;
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _module.Heal(_module.Regeneration);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
}