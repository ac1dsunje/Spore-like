using System;
using UnityEngine;

namespace _Game.Scripts.UI
{
public abstract class ScreenManager: MonoBehaviour
{
    private CanvasGroup _screen;

    private void Awake()
    {
        _screen =  GetComponent<CanvasGroup>();
    }

    public void ShowScreen()
    {
        _screen.alpha = 1;
        _screen.blocksRaycasts = true;
        _screen.interactable = true;
    }

    public void HideScreen()
    {
        _screen.alpha = 0;
        _screen.blocksRaycasts = false;
        _screen.interactable = false;
    }
}
}