using UnityEngine;

namespace _Game.Scripts.Core.UI
{
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen: MonoBehaviour
{
    private CanvasGroup _screen;

    protected virtual void Awake()
    {
        _screen =  GetComponent<CanvasGroup>();
    }

    protected void ShowScreen()
    {
        _screen.alpha = 1;
        _screen.blocksRaycasts = true;
        _screen.interactable = true;
    }

    protected void HideScreen()
    {
        _screen.alpha = 0;
        _screen.blocksRaycasts = false;
        _screen.interactable = false;
    }
}
}