using System.Collections;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Health
{
public class PlayerHealth: MonoBehaviour
{
    private HealthModule _module;
    
    public void Construct(HealthModule module)
    {
        _module = module;
        _module.OnDamageTaken += StopRegeneration;
        StartRegeneration();
    }

    private void StartRegeneration() => StartCoroutine(Regenerate());

    private void StopRegeneration(float damage)
    {
        StopAllCoroutines();
        StartCoroutine(WaitBeforeRegeneration());
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _module.Heal(_module.Regeneration);
        }
    }

    private IEnumerator WaitBeforeRegeneration()
    {
        yield return new WaitForSeconds(1f);
        StartRegeneration();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        _module.OnDamageTaken -= StopRegeneration;
    }
}
}