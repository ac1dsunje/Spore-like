using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinMovement: IFixedTickable
{
    [Inject] private MovementController _controller;
    [Inject] private MovementModule _movement;

    public void FixedTick()
    {
        _controller.SetMaterial(_movement.Friction, _movement.Bounciness);
    }
}
}