using _Game.Scripts.GamePlay.Animation;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Player.Network;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerDisguise : EntityNetworkBehaviour, IDisguisable
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