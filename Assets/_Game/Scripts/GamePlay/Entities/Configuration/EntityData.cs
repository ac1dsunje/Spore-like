using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Configuration
{
public enum EntityAI
{
    Food = 0,
    Player = 1,
    SeaUrchin = 2,
}

public enum EntityHealth
{
    Basic = 0,
    Reflective = 1,
}

public enum EntityAttack
{
    Basic = 0,
    Player = 1,
}

public enum EntityDeath
{
    Basic = 0,
    Revival = 1
}

[Serializable]
public class EntityData
{
    [field: SerializeField] public EntityAI AIType { get; private set; }
    [field: SerializeField] public EntityHealth HealthType { get; private set; }
    [field: SerializeField] public EntityAttack AttackType { get; private set; }
    [field: SerializeField] public EntityDeath DeathType { get; private set; }
}
}