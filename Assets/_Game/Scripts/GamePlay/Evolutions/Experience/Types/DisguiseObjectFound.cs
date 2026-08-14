using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Player;
using _Game.Scripts.GamePlay.Player.Modules;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DisguiseObjectFound: EvolutionExperienceService
{
    private readonly HashSet<IDisguiseAble> _disguisedObjects = new();

    public DisguiseObjectFound(PlayerModel playerModel, float amount) : base(playerModel, amount) => PlayerModel.Vision.OnDisguiseAbleDiscovered += OnDisguiseObjectFound;

    private void OnDisguiseObjectFound(IDisguiseAble gameObject)
    {
        if (!_disguisedObjects.Add(gameObject)) return;

        AddAmount(1);
    }

    public override void Dispose() => PlayerModel.Vision.OnDisguiseAbleDiscovered -= OnDisguiseObjectFound;
}
}