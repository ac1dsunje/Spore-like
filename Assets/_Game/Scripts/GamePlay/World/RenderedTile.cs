using UnityEngine.Tilemaps;

namespace _Game.Scripts.GamePlay.World
{
public readonly struct RenderedTile
{
    public readonly int BiomeIndex;
    public readonly TileBase Tile;

    public RenderedTile(int biomeIndex, TileBase tile)
    {
        BiomeIndex = biomeIndex;
        Tile = tile;
    }
}
}