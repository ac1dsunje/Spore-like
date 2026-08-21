using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class XRayDiscovering: ExperienceService
{
    private readonly VisionModule _module;
    
    private readonly HashSet<IDisguisable> _disguisedObjects = new();

    public XRayDiscovering(VisionModule module, float amount) : base(amount)
    {
        _module = module;
        _module.OnDiscoveredWithXRay += OnObjectDiscoveredWithXRay;
    }

    private void OnObjectDiscoveredWithXRay(IDisguisable gameObject)
    {
        if (!_disguisedObjects.Add(gameObject)) return;

        AddAmount(1);
    }

    public override void Dispose() => _module.OnDiscoveredWithXRay -= OnObjectDiscoveredWithXRay;
}
}