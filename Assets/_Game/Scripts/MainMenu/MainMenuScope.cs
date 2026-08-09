using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.MainMenu
{
public class MainMenuScope: LifetimeScope
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_mainMenuScreen);
    }
}
}