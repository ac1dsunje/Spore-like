using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player
{
public abstract class PlayerNetworkBehaviour : MonoBehaviour
{
    protected bool IsLocal { get; private set; }

    private PlayerAuthority _authority;

    [Inject]
    private void Construct(PlayerAuthority authority)
    {
        _authority = authority;

        if (_authority.IsInitialized)
        {
            Initialize(_authority.IsLocal);
        }
        else
        {
            _authority.OnAuthorityInitialized += Initialize;
        }
    }

    private void Initialize(bool local)
    {
        IsLocal = local;
        OnNetworkInitialized();
    }

    protected virtual void OnNetworkInitialized()
    {
    }

    protected virtual void OnDestroy()
    {
        _authority.OnAuthorityInitialized -= Initialize;
    }
}
}