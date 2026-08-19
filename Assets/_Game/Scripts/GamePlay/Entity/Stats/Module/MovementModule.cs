using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entity.Module
{

public class MovementModule : StatModule
{
    public float MoveSpeed => _useSprint ? _moveSpeed * _sprintMultiplier : _moveSpeed;

    public float Acceleration => _acceleration / 100f;
    public float Inertia => _inertia / 100f;
    public float DashPower { get; private set; }
    public bool DashRequested { get; private set; }

    public Vector3Int GridPosition { get; private set; }

    public event Action<float> OnDistanceOvercome;
    public event Action<MovementModule> OnGridPositionChanged;
    public event Action OnSprint;
    
    private float _moveSpeed;
    private float _acceleration;
    private float _inertia;
    private float _sprintMultiplier;
    
    private bool _useSprint;
    
    protected override void Configure()
    {
        BindStat(StatType.MoveSpeed, UpdateMoveSpeed);
        BindStat(StatType.Acceleration, UpdateAcceleration);
        BindStat(StatType.Inertia, UpdateInertia);
        BindStat(StatType.SprintMultiplier, UpdateSprintMultiplier);
        BindStat(StatType.DashPower, UpdateDashPower);
    }
    
    public void SetDash(bool state)
    {
        DashRequested = state;
    }

    public void SetSprint(bool state)
    {
        _useSprint = state;
        OnSprint?.Invoke();
    }

    public void UpdateGridPosition(Vector3Int position)
    {
        if (position == GridPosition) return;
        GridPosition = position;
        OnGridPositionChanged?.Invoke(this);
        OnDistanceOvercome?.Invoke(1);
    }

    private void UpdateMoveSpeed(float value) => _moveSpeed = value;

    private void UpdateAcceleration(float value) => _acceleration = value;

    private void UpdateInertia(float value) => _inertia = value;

    private void UpdateSprintMultiplier(float value) => _sprintMultiplier = value;

    private void UpdateDashPower(float value) => DashPower = value;
}
}