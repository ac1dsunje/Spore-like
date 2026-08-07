using UnityEngine;

namespace _Game.Scripts.Core.Services
{
public class CoroutinePerformer: MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(gameObject);
}
}