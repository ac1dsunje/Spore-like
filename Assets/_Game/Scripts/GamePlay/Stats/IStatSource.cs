using System.Collections.Generic;

namespace _Game.Scripts.GamePlay
{
public interface IStatSource
{
    List<SourceStat> GetStats();
}
}