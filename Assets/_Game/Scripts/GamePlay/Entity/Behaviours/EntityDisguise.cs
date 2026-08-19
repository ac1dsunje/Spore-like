using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Entity.Interfaces;
using _Game.Scripts.GamePlay.Entity.Module;
using _Game.Scripts.GamePlay.Network;
using VContainer;

namespace _Game.Scripts.GamePlay.Behaviours
{
public class EntityDisguise : EntityNetworkBehaviour, IDisguisable
{
    [Inject] private ItemAnimation _animation;
    [Inject] private DisguiseModule _disguise;

    public bool IsDetected(float sensorics, bool xRay)
    {
        var show = _disguise.TryNotice(sensorics, xRay);

        if (!IsLocal)
        {
            _animation.SetVisible(show);
        }

        return show;
    }
}
}