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
    }

    private void Update()
    {
        if (_playerMovement.IsSprinting)
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