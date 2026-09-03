using System;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.World.Biomes.Plant
{
public class PlantAI : IStartable, IDisposable
{
    [Inject] private IHealthController _healthController;
    [Inject] private BodyHitbox _hitBox;

    public void Start()
    {
        _hitBox.OnHit += TakeDamage;
    }

    private void TakeDamage(HitInfo hit)
    {
        _healthController.TakeDamage(hit);
    }

    public void Dispose()
    {
        _hitBox.OnHit -= TakeDamage;
    }
}
}