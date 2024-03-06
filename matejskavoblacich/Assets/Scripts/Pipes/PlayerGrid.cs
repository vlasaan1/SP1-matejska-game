using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerGrid : MonoBehaviour
{

    [SerializeField] int size;

    [SerializeField] Tile tilePrefab;

    GridLayoutGroup gridLayoutGroup;

    private Dictionary<Vector2, Tile> grid;

    void Awake(){
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    public void GenerateGrid(int size){

        gridLayoutGroup.constraintCount = size;

        grid = new Dictionary<Vector2, Tile>();
        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                var spawnedTile = Instantiate(tilePrefab, new Vector3(y, x, 0), Quaternion.identity, transform);
                spawnedTile.name = "Tile_" + y + "_" + x;

                grid[new Vector2(y,x)] = spawnedTile;
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 pos){
        if(grid.TryGetValue(pos, out var tile)){
            return tile;
        }
        return null;
    }
}
