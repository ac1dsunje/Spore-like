using System.Collections;
using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Player.Network;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerHealth: EntityNetworkBehaviour
{
    private HealthModule _module;
    
    [Inject]
    public void Construct(HealthModule module)
    {
        _module = module;
        _module.OnDamageTaken += StopRegeneration;
        _module.OnDeath += Die;
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

    private void Die()
    {
        if (!IsLocal)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
        _module.OnDamageTaken -= StopRegeneration;
        _module.OnDeath -= Die;
    }
}
}