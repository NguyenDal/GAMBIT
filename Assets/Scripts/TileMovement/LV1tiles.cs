using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV1tiles : MonoBehaviour
{
    TileManager tm;

    GameObject tileMap;
    List<Tile> allTiles;

    Tile currentTile;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("LV1tiles");
        allTiles = new List<Tile>();

         //Go through FloorTileMap and get each tile. Tile numbers start at zero, and will match up with their index in array
        foreach (Transform tileObject in tileMap.transform)
        {
            Tile t = new(tileObject);
            allTiles.Add(t);
        }

        //Set next tiles. Most tiles follow patter of nextTile = [my index]+1
        //Except for a) tiles that have no next tile, and b) tiles that point to a second tile
        //Forunatly, there are only a few of these, and when one tile points to no tile, there will always be another tile that does
        //(Ex; tile 9 is a dead end, but tile 10 is still pointed to by tile 5)
        //Except for last tile, which we do not try to do anything with here
        for(int i = 0; i < (allTiles.Count)-1; i++)
        {
            //t9 points to nothing, but t5 points to t10
            if(i == 9)
            {
                //give t5 t10 as its second next tile
                allTiles[5].setNext2(allTiles[i + 1]);
            }
            //t15 points to nothing, but t10 points to t16
            else if(i == 15)
            {
                //give t10 t16 as its second tile
                allTiles[10].setNext2(allTiles[i + 1]);
            }
            //T21 points to nothing, but t18 points to t22
            else if(i == 21)
            {
                //give t18 t22 as its second tile
                allTiles[18].setNext2(allTiles[i + 1]);
            }
            //if not one of those special cases, each tile will have it's first tile set to the next tile in the index
            else
            {
                allTiles[i].setNext1(allTiles[i + 1]);
            }
        }

        //Now set the previous tiles
        //If a tile is pointed to, it's previous tile is the tile that pointed to it
        foreach(Tile t in allTiles)
        {
            //if a tile has a next tile, set that tiles previous tile to the current tile
            if(t.getNextTile1() != null)
            {
                t.getNextTile1().setPrevious(t);
            }
            if(t.getNextTile2() != null)
            {
                t.getNextTile2().setPrevious(t);
            }
        }

        //set current tile to the first tile
        currentTile = allTiles[0];

        //create tile manager, give it our array and current tile
        tm = new TileManager(allTiles, currentTile);

        //Hide all tiles except the first one, and we're ready to start the game
        tm.resetTiles();
    }

    //Each tile gets a script that detects when it is triggered by the player
    //When triggered, the tile calls this method with its own GameObject
    public void giveParentTrigger(GameObject triggeredTile)
    {
        //send tile manager the triggered tile
        tm.giveManagerTiles(triggeredTile);
    }
}
