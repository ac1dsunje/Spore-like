using System;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class BiomeChecker: IStartable, IDisposable
{
    private readonly WorldModel _worldModel;
    private readonly MovementModule _movement;
    private readonly EnvironmentModule _environment;
    private readonly BuffsModule _buffsModule;
    private readonly BreathingModule _breathingModule;
    
    private Biome _currentBiome;
    
    public BiomeChecker(WorldModel worldModel, MovementModule movement, EnvironmentModule environment, 
        BuffsModule buffsModule, BreathingModule breathingModule)
    {
        _worldModel = worldModel;
        _movement = movement;
        _environment = environment;
        _buffsModule = buffsModule;
        _breathingModule = breathingModule;
    }

    public void Start()
    {
        _movement.OnGridPositionChanged += TryEnterBiome;
        EnterBiome(_worldModel.GetBiome(_movement.GridPosition));
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
        
        _buffsModule.Set(BuffType.BadPassAbility, _environment.IsBadPassAbility(biome.PassAbility));
        _buffsModule.Set(BuffType.Cold, _environment.IsCold(biome.Temperature));
        _buffsModule.Set(BuffType.Heat, _environment.IsHot(biome.Temperature));
        _buffsModule.Set(BuffType.Suffocating, _breathingModule.IsSuffocating(biome.OxygenBreathing, biome.HydrogenBreathing));
    }

    public void Dispose()
    {
        _movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}