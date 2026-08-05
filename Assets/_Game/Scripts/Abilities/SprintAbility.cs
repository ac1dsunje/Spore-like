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
        var cost = Config.InUseCost * Time.deltaTime;
        var isAble = Endurance.HasEnoughEndurance(cost);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && Endurance.HasEnoughEndurance(Config.StartCost) && !_isActive)
        {
            StartUsing(Config.StartCost);
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

    private void StartUsing(float startCost)
    {
        _isActive = true;
        Endurance.AddUser(this);
        _movement.UseSprint = true;
        Endurance.UseEndurance(startCost);
    }

    private void Do(float cost)
    {
        Endurance.UseEndurance(cost);
    }

    private void Stop()
    {
        _isActive = false;
        Endurance.RemoveUser(this);
        _movement.UseSprint = false;
    }
}
}