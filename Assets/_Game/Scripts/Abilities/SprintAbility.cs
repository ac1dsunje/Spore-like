using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules.Movement;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
public class SprintAbility: AbilityController
{
    private MovementModule _movement;

    private bool _isActive;

    public void Construct(PlayerModel model)
    {
        base.Construct(model.Endurance);
        _movement = model.Movement;
    }

    private void Update()
    {
        var cost = 1f * Time.deltaTime;
        var isAble = _endurance.HasEnoughEndurance(cost);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && isAble)
        {
            StartUsing();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Stop();
            return;
        }

        if (!_isActive) return;

        if (isAble)
            Do(cost);
        else
            Stop();
    }

    private void StartUsing()
    {
        _isActive = true;
        _endurance.AddUser(this);
    }

    private void Do(float cost)
    {
        _endurance.UseEndurance(cost);
        _movement.UseSprint = true;
    }

    private void Stop()
    {
        _isActive = false;
        _endurance.RemoveUser(this);
        _movement.UseSprint = false;
    }
}
}