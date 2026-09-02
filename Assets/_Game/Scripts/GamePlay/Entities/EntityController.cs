using _Game.Scripts.GamePlay.Player.Modules.Experience;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityController: IStartable
{
    [Inject] public EntityModel Model { get; private set; }
    [Inject] public ExperienceModule Experience { get; private set; }

    public virtual void Start()
    {
        Model.Stats.Initialize();
    }
}
}