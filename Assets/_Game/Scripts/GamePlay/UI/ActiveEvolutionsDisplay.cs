using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Evolutions.UI;
using _Game.Scripts.GamePlay.Player;
using UnityEngine;

namespace _Game.Scripts.GamePlay.UI
{
public class ActiveEvolutionsDisplay: MonoBehaviour
{
    [Header("Evolutions")]
    [SerializeField] private GameObject _evolutionSlotPrefab;
    [SerializeField] private Transform  _evolutionsParent;
    
    private PlayerModel _player;

    public void Construct(PlayerModel player)
    {
        _player = player;
        _player.Stats.OnEvolutionAdded += AddEvolution;
    }

    private void AddEvolution(Evolution evolution)
    {
        var slot = Instantiate(_evolutionSlotPrefab, _evolutionsParent).GetComponent<ActiveEvolutionSlotUI>();
        slot.Construct(evolution);
    }

    private void OnDestroy()
    {
        _player.Stats.OnEvolutionAdded -= AddEvolution;
    }
}
}