using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class EntitiesDiscovering: ExperienceService
{
    private readonly VisionModule _module;
    
    private readonly HashSet<IVisible> _discovered = new();

    public EntitiesDiscovering(VisionModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnEntityDiscovered += OnEntityDiscovered;
    }

    private void OnEntityDiscovered(IVisible entity)
    {
        if (!_discovered.Add(entity)) return;

        AddAmount(1);
    }

    public override void Dispose() => _module.OnEntityDiscovered -= OnEntityDiscovered;
}
}