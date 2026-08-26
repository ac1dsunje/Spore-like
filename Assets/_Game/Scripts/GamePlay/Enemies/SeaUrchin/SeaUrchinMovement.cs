using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinMovement: MonoBehaviour
{
    [SerializeField] private MovementController _controller;
    
    [Inject] private MovementModule _movement;

    private void FixedUpdate()
    {
        _controller.SetMaterial(_movement.Friction, _movement.Bounciness);
    }
}
}