using System.Collections;
using _Game.Scripts.Player.Modules.Movement;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Endurance
{
public class PlayerEndurance: MonoBehaviour
{
    private EnduranceModule _module;
    private PlayerMovement _playerMovement;
    
    public void Construct(EnduranceModule module, PlayerMovement playerMovement)
    {
        _module = module;
        _playerMovement = playerMovement;
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_playerMovement.IsSprintInput) continue;
            _module.AddEndurance(_module.EnduranceRecovery);
        }
    }

    private void Update()
    {
        if (!_playerMovement.IsSprintInput) return;
        _module.UseEndurance(1f * Time.deltaTime);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
}