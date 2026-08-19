using System.Collections.Generic;

namespace _Game.Scripts.GamePlay.Entity
{
public interface IStatSource
{
    List<SourceStat> GetStats();
}
}