using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Follows same format as LV1tiles
public class LV2tiles : MonoBehaviour
{
    TileManager tm;

    GameObject tileMap;
    List<Tile> allTiles;

    Tile currentTile;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("LV2tiles");
        allTiles = new List<Tile>();

        //Get tiles from floorTileMap
        foreach (Transform tileObject in tileMap.transform)
        {
            Tile t = new(tileObject);
            allTiles.Add(t);
        }

        //Set all nextiles, same idea as with LV1, just different "exception" tiles
        for (int i = 0; i < (allTiles.Count) - 1; i++)
        {
            //t13 points to nothing, but t5 points to t14
            if (i == 13)
            {
                //give t5 t14 as its second next tile
                allTiles[5].setNext2(allTiles[i + 1]);
            }
            //t20 points to nothing, but t17 point to t21
            else if (i == 20)
            {
                //give t17 t21 as its second tile
                allTiles[17].setNext2(allTiles[i + 1]);
            }
            //T27 points to nothing, but t23 points to t28
            else if(i == 27)
            {
                //give t23 t28 as its second tile
                allTiles[23].setNext2(allTiles[i + 1]);
            }
            //if not one of those special cases, each tile will have it's first tile set to the next tile in the index
            else
            {
                allTiles[i].setNext1(allTiles[i + 1]);
            }
        }

        //A tiles previous tile is the tile that is pointing to it
        //No tile will show up as the nexttile1 (or 2) for multiple tiles
        foreach (Tile t in allTiles)
        {
            //if a tile has a next tile, set that tiles previous tile to the current tile
            if (t.getNextTile1() != null)
            {
                t.getNextTile1().setPrevious(t);
            }
            if (t.getNextTile2() != null)
            {
                t.getNextTile2().setPrevious(t);
            }
        }

        //set current tile to the first tile
        currentTile = allTiles[0];

        tm = new TileManager(allTiles, currentTile);

        //Hide all tiles except the first one, and we're ready to start the game
        tm.resetTiles();
    }

    //When a tile is triggered, we receive it here. Send to manager.
    public void giveParentTrigger(GameObject triggeredTile)
    {
        //tm.giveManagerTiles(triggeredTile);
        tm.giveManagerTiles(triggeredTile);
    }
}
