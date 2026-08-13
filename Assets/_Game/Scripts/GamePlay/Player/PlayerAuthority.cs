using System;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerAuthority
{
    public event Action<bool> OnAuthorityInitialized;

    public bool IsInitialized { get; private set; }
    public bool IsLocal { get; private set; }

    public void SetNetworkType(bool isLocal)
    {
        IsInitialized = true;
        IsLocal = isLocal;

        OnAuthorityInitialized?.Invoke(isLocal);
    }
}
}