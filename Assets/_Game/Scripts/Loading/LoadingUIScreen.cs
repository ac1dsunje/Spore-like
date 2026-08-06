using _Game.Scripts.Core.UI;
using UnityEngine;

namespace _Game.Scripts.Loading
{
public class LoadingUIScreen: UIScreen
{
    [SerializeField] private Transform _loadingSquare;

    private void Update()
    {
        _loadingSquare.Rotate(Vector3.forward, 90 * Time.deltaTime);
    }
}
}