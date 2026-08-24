using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Buffs.Types
{
public class BadPassAbility: Buff
{
    private readonly MovementModule _movement;
    
    public BadPassAbility(EntityStats entityStats, MovementModule movement, Ticker ticker, BuffConfig config)
        : base(entityStats, config, ticker)
    {
        _movement = movement;
    }
}
}