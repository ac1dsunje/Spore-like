using System;
using _Game.Scripts.GamePlay.Entities.Drops;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Picker
{
public class EntityPicker: IStartable, ITickable, IDisposable
{
    [Inject] private PickerHitbox _pickerHitbox;
    [Inject] private PickingModule _pickingModule;
    [Inject] private StomachModule _stomach;

    public void Start()
    {
        _pickerHitbox.OnPicked += Pick;
    }

    public void Tick()
    {
        _pickerHitbox.SetSize(_pickingModule.PickingRange);
    }

    private void Pick(DropType dropType)
    {
        switch (dropType)
        {
            case DropType.Food:
                _stomach.GetExperienceFromFood(1);
                break;
            case DropType.Experience:
                // ToDo: add experience picking method 
                _pickingModule.GetExperiencePoint(1);
                Debug.Log("experience point got!");
                break;
        }
    }

    public void Dispose()
    {
        _pickerHitbox.OnPicked -= Pick;
    }
}
}