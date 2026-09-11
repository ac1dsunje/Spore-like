using System;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Environment;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class BiomeChecker : IStartable, IDisposable
{
    private readonly WorldModel _worldModel;
    private readonly MovementModule _movement;
    private readonly TemperatureModule _temperature;
    private readonly PassabilityModule _passability;
    private readonly BreathingModule _breathing;
    private readonly BuffsModule _buffsModule;
    
    private Biome _currentBiome;
    private IDisposable _positionSubscription;
    
    public BiomeChecker(WorldModel worldModel, MovementModule movement, TemperatureModule temperature, 
        PassabilityModule passability, BuffsModule buffsModule, BreathingModule breathing)
    {
        _worldModel = worldModel;
        _movement = movement;
        _temperature = temperature;
        _passability = passability;
        _buffsModule = buffsModule;
        _breathing = breathing;
    }

    public void Start()
    {
        _positionSubscription = _movement.GridPosition
            .Subscribe(TryEnterBiome);
    }

    private void TryEnterBiome(Vector3Int position)
    {
        var currentBiome = _worldModel.GetBiome(position);
        if (currentBiome == _currentBiome) return;
        EnterBiome(currentBiome);
    }

    private void EnterBiome(Biome biome)
    {
        _currentBiome = biome;
        
        _buffsModule.Set(BuffType.BadPassAbility, _passability.IsBadPassAbility(biome.PassAbility));
        _buffsModule.Set(BuffType.Cold, _temperature.IsCold(biome.Temperature));
        _buffsModule.Set(BuffType.Heat, _temperature.IsHot(biome.Temperature));
        _buffsModule.Set(BuffType.Suffocating, _breathing.IsSuffocating(biome.OxygenBreathing, biome.HydrogenBreathing));
    }

    public void Dispose()
    {
        _positionSubscription?.Dispose();
    }
}
}