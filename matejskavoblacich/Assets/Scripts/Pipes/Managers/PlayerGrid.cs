using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerGrid : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;

    private float baseXPos = -0.5f;

    private float baseYPos = 0.5f;

    private float tileSize = 0f;

    private int scaler = 0;

    public static PlayerGrid instance;

    public Dictionary<Vector2, Tile> grid;

    void Awake(){
        instance = this;
    }

    public void GenerateGrid(int size, int s){
        tileSize = 1f/size;
        scaler = s;

        grid = new Dictionary<Vector2, Tile>();
        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                var spawnedTile = Instantiate(tilePrefab, getPos(x, y), Quaternion.identity, transform);
                spawnedTile.transform.localScale = new Vector3(tileSize, tileSize, 1);
                spawnedTile.name = "Tile_" + x + "_" + y;
                grid[new Vector2(x,y)] = spawnedTile;
            }
        }
    }

    private float getXPos(int x){
        return scaler*(baseXPos + (tileSize/2) + x*tileSize);
    }

    private float getYPos(int y){
        return scaler*(baseYPos - (tileSize/2) - y*tileSize);
    }

    private Vector3 getPos (int x, int y){
        return new Vector3(getXPos(x), getYPos(y), 1);
    }



    public Tile GetTileAtPosition(Vector2Int pos){
        if(grid.TryGetValue(pos, out var tile)){
            return tile;
        }
        return null;
    }

    //Pripraveny jenom at to muzu volat z inputu
    public bool CanGetTileAtPosition(Vector2Int pos){
        return grid[pos].isOccupied && grid[pos].occupiedUnit.isMoveable;
    }

    public void SwapTiles(Vector2Int first, Vector2Int second){
        //Drzel jsem first a pustil jsem ho na second, jestli se daji swapnout tak to udelej, jestli ne
        // tak nemusis delat nic a vsechno by melo byt v poradku
        if(CanGetTileAtPosition(first) && CanGetTileAtPosition(second)){
            grid[first].SwapUnits(grid[second]);
            grid[first].occupiedUnit.transform.position = getPos(first.x, first.y);
            grid[second].occupiedUnit.transform.position = getPos(second.x, second.y);
        }
    }
}
