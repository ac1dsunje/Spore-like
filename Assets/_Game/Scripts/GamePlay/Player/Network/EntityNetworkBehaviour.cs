using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Network
{
public abstract class EntityNetworkBehaviour : MonoBehaviour
{
    protected bool IsLocal { get; private set; }

    private EntityAuthority _authority;

    [Inject]
    private void Construct(EntityAuthority authority)
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