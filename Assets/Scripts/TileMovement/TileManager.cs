using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DS = data.DataSingleton;

public class TileManager
{
    int index;
    List<Tile> allTiles;
    Tile currentTile;

    public TileManager(List<Tile> tiles, Tile currTile)
    {
        allTiles = tiles;
        currentTile = currTile;
    }

    public void giveManagerTiles(GameObject triggeredTile)
    {
        //tile gameobject should have a name of format "Tile#"
        string tileName = triggeredTile.name;
        //Split string on e, to get "Til" and "[tile number]"
        string[] splitString = tileName.Split('e');

        foreach(string test in splitString)
        {
            //If the string isn't "Til"
            if (!(test.Equals("Til"))){
                //Then it's the number. Make this an int and use it to find the triggered tile in the array
                index = int.Parse(test);
            }
        }

        //because we have the current tile, we know which tiles to try to hide (instead of trying to hide all of them)
        currentTile.hideAll();

        //now can set current tile to the triggered tile
        currentTile = allTiles[index];

        //and show tiles connected to the new current tile
        currentTile.showAll();
    }

    //Hides all tiles, except for the first one
    public void resetTiles()
    {
        foreach(Tile t in allTiles)
        {
            t.getGameTile().gameObject.SetActive(false);
        }

        //If game was started with "Move with tiles" selected, show the first tile
        if (DS.GetData() != null && DS.GetData().CharacterData != null)
        {
            if ((PlayerPrefs.GetInt("MoveWithTiles" + "Enabled") == 1))
            {
                allTiles[0].getGameTile().gameObject.SetActive(true);
            }
            //No else. If we are are not moving with tiles, do not show first tile
            //No first tile = no tile can be selected = no tiles in the game
        }
    }
}
