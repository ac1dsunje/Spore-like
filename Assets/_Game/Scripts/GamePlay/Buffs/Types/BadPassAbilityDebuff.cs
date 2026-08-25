using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Buffs.Types
{
public class BadPassAbility: Buff
{
    public BadPassAbility(EntityStats entityStats, Ticker ticker, BuffConfig config)
        : base(entityStats, config, ticker)
    { }
}
}