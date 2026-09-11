using System;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules.Health
{
public class LifeModule : StatModule
{
    public event Action<LifeModule> OnDeath;
    public event Action<LifeModule> OnRevived;
    
    private bool _isDead;
    private float _extraLives;
    private float _extraLivesUsed;

    protected override void Configure()
    {
        BindStat(StatType.ExtraLife, UpdateExtraLife);
    }

    public void TryConsumeLife()
    {
        if (_isDead) return;
        if (_extraLives > 0f)
        {
            _extraLivesUsed++;
            _extraLives--;
            OnRevived?.Invoke(this);
        }
        else
        {
            _isDead = true;
            OnDeath?.Invoke(this);
        }
    }
    
    private void UpdateExtraLife(float value) => _extraLives = value - _extraLivesUsed;
}
}