using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entity.Interfaces;
using _Game.Scripts.GamePlay.Entity.Module;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DisguiseObjectFound: ExperienceService
{
    private readonly VisionModule _module;
    
    private readonly HashSet<IDisguisable> _disguisedObjects = new();

    public DisguiseObjectFound(VisionModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDisguiseAbleDiscovered += OnDisguiseObjectFound;
    }

    private void OnDisguiseObjectFound(IDisguisable gameObject)
    {
        if (!_disguisedObjects.Add(gameObject)) return;

        AddAmount(1);
    }

    public override void Dispose() => _module.OnDisguiseAbleDiscovered -= OnDisguiseObjectFound;
}
}