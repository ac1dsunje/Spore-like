using System;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.World;
using _Game.Scripts.GamePlay.World.Biomes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class BiomeChecker: IStartable, IDisposable
{
    private readonly WorldModel _worldModel;
    
    private readonly MovementModule _movement;
    private readonly EnvironmentModule _environment;
    
    private readonly BuffsModule _buffsModule;
    
    private Biome _currentBiome;
    
    [Inject]
    public BiomeChecker(WorldModel worldModel, MovementModule movement, EnvironmentModule environment, BuffsModule buffsModule)
    {
        _worldModel = worldModel;
        _movement = movement;
        _environment = environment;
        _buffsModule = buffsModule;
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
        CheckPassability(biome.PassAbility);
        ApplyTemperature(biome.Temperature);
        CheckBreathing(biome.OxygenBreathing, biome.HydrogenBreathing);
    }

    private void CheckPassability(float passability)
    {
        _buffsModule.Set(BuffType.BadPassAbility, passability > _environment.PassAbility);
    }

    private void ApplyTemperature(float temperature)
    {
        _buffsModule.Set(BuffType.Cold, temperature < _environment.MinimalComfortable);
        _buffsModule.Set(BuffType.Heat, temperature > _environment.MaximumComfortable);
    }

    private void CheckBreathing(float oxygen, float hydrogen)
    {
        var oxygenRequirement = _environment.OxygenBreathing;
        var hydrogenRequirement = _environment.HydrogenBreathing;

        var hasOxygen = oxygenRequirement > 0f && oxygen >= oxygenRequirement;
        var hasHydrogen = hydrogenRequirement > 0f && hydrogen >= hydrogenRequirement;

        var needsToBreathe = oxygenRequirement > 0f || hydrogenRequirement > 0f;
        var suffocate = needsToBreathe && !hasOxygen && !hasHydrogen;

        _buffsModule.Set(BuffType.Suffocating, suffocate);
    }

    public void Dispose()
    {
        _movement.OnGridPositionChanged -= TryEnterBiome;
    }
}
}