using _Game.Scripts.GamePlay.World.Biome;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World
{
public readonly struct RenderedTile
{
    public readonly BiomeConfig Biome;
    public readonly TileBase Tile;

    public RenderedTile(BiomeConfig biome, TileBase tile)
    {
        Biome = biome;
        Tile = tile;
    }
}
}