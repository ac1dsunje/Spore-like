using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Picker
{
public class EntityPicker: ITickable
{
    [Inject] private PickerHitbox _pickerHitbox;
    [Inject] private PickingModule _pickingModule;

    public void Tick()
    {
        _pickerHitbox.SetSize(_pickingModule.PickingRange);
    }
}
}