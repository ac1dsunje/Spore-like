using System;
using System.Linq;
using _Game.Scripts.GamePlay.Drops;
using _Game.Scripts.GamePlay.Entities.Configuration;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityPicker: IStartable, ITickable, IDisposable
{
    [Inject] private PickerHitbox _pickerHitbox;
    [Inject] private PickingModule _pickingModule;
    [Inject] private StomachModule _stomach;
    [Inject] private EntityConfig _config;

    public void Start()
    {
        _pickerHitbox.OnPicked += Pick;
    }

    public void Tick()
    {
        _pickerHitbox.SetSize(_pickingModule.PickingRange);
    }

    private void Pick(Drop drop)
    {
        switch (drop.GetType)
        {
            case DropType.Food:
                if (_config.ExperienceConfig.ExperienceConfig.ExperienceTypes.Any(exp => exp.Type == ExperienceType.FoodEating))
                {
                    _stomach.GetExperienceFromFood(1);
                    _pickerHitbox.DestroyDrop(drop);
                }
                break;
            case DropType.Experience:
                if (_config.ExperienceConfig.ExperienceConfig.ExperienceTypes.Any(exp => exp.Type == ExperienceType.ExperienceCollecting))
                {
                    _pickingModule.GetExperiencePoint(1);
                    _pickerHitbox.DestroyDrop(drop);
                }
                break;
        }
    }

    public void Dispose()
    {
        _pickerHitbox.OnPicked -= Pick;
    }
}
}