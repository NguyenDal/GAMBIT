using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tile class to store a tile
/// </summary>
/// 
/// <param name="previousTile">The tile visible to the current tile, with an index smaller than the current tile. Every tile, except for the starting tile, will have one</param>
/// <param name="nextTile1">One of the possible tiles visible to the current tile, with a greater index. Will be null if the tile is the end of its path</param>
/// <param name="nextTile2">Same as nextTile1, but will be null unless the tile has a third route option</param>
/// 
/// <param name="ingameTile">The tile object being referenced to/controlled</param>
/// 

public class Tile
{
    private Tile previousTile = null;
    private Tile nextTile1 = null;
    private Tile nextTile2 = null;

    private Transform ingameTile;


    //Constructor
    public Tile(Transform gameTile)
    {
        ingameTile = gameTile;
    }

    //Setters
    public void setOnlyPreviousTile(Tile pt)
    {
        previousTile = pt;
    }
    public void setOnlyNextTile1(Tile t1)
    {
        nextTile1 = t1;
    }
    public void setOnlyNextTile2(Tile t2)
    {
        nextTile2 = t2;
    }
    //For most tiles, will want to set up multiple connected tiles at once
    public void setTilesPrevNext1(Tile pt, Tile t1)
    {
        previousTile = pt;
        nextTile1 = t1;
    }
    public void setTilesPrevNext1Next2(Tile pt, Tile t1, Tile t2)
    {
        previousTile = pt;
        nextTile1 = t1;
        nextTile2 = t2;
    }

    //Getters
    public Tile getPreviousTile()
    {
        return previousTile;
    }
    public Tile getNextTile1()
    {
        return nextTile1;
    }
    public Tile getNextTile2()
    {
        return nextTile2;
    }

    public Transform getGameTile()
    {
        return ingameTile;
    }
}
