using _Game.Scripts.GamePlay.Entities.Experience;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityController
{
    [Inject] public EntityModel Model { get; private set; }
    [Inject] public ExperienceModule Experience { get; private set; }
    [Inject] public BuffsModule Buffs { get; private set; }
    [Inject] public AbilitiesModule Abilities { get; private set; }
    [Inject] public EvolutionsModule Evolutions { get; private set; }
}
}