using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.World.Biome
{
[CreateAssetMenu(fileName = "New Biome Config", menuName = "Configs/Game/World/Biomes/Biome")]
public class BiomeConfig: ScriptableObject
{
    [field: SerializeField] public List<TileBase> Tiles { get; private set; }
    [field: SerializeField] public int Height { get; private set; }

    public TileBase RandomTile => Tiles[Random.Range(0, Tiles.Count)];
}
}