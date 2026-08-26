using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinMovement: MonoBehaviour
{
    [SerializeField] private MovementController _controller;
    
    private MovementModule _movement;

    [Inject]
    private void Construct(MovementModule movement)
    {
        _movement = movement;
        _controller.SetMaterial(0.4f, 1f, 0.4f);
    }
    
    
}
}