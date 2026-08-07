using System;
using UnityEngine;

namespace _Game.Scripts.Core.UI
{
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen: MonoBehaviour
{
    public event Action<bool> OnStateChanged;
    
    private bool _isActive;
    private CanvasGroup _screen;

    protected virtual void Awake()
    {
        _screen =  GetComponent<CanvasGroup>();
    }

    public virtual void ToggleScreen()
    {
        if (_isActive)
        {
            HideScreen();
        }
        else
        {
            ShowScreen();
        }
    }

    public virtual void ShowScreen()
    {
        _screen.alpha = 1;
        _screen.blocksRaycasts = true;
        _screen.interactable = true;
        _isActive = true;
        OnStateChanged?.Invoke(_isActive);
    }

    public virtual void HideScreen()
    {
        _screen.alpha = 0;
        _screen.blocksRaycasts = false;
        _screen.interactable = false;
        _isActive = false;
        OnStateChanged?.Invoke(_isActive);
    }
}
}