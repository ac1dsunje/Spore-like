using System.Collections.Generic;

namespace _Game.Scripts.GamePlay.Stats
{
public interface IStatSource
{
    List<Stat> GetStats();
}
}