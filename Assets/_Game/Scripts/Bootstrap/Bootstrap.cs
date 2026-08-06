using _Game.Scripts.Core.Services;
using UnityEngine;

namespace _Game.Scripts.Bootstrap
{
public class Bootstrap: MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _sceneLoader.LoadGamePlayScene();
    }
}
}