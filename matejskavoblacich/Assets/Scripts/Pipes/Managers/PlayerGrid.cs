using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// handles the grid, that fill be used to place playboard on. Also spawns and handles each tile of the game on which will be game elements placed
/// </summary>
public class PlayerGrid : MonoBehaviour
{
    [Header("------------------- Managers --------------")]
    [SerializeField] Tile tilePrefab;
    [SerializeField] Minigame minigame;
    [SerializeField] PipesAudioManager pipesAudioManager;

    private SpriteRenderer spriteRenderer;
    private float tileSize = 0f;
    private int scaler = 0;
    private int fieldSize = 0;
    public Dictionary<Vector2, Tile> grid;
    private System.Random random;
    private int shufflingConstant = 20;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Function that is called from GameMaster, generating and spawning tiles to its right places
    /// </summary>
    /// <param name="size"></param>
    /// <param name="s"></param>
    public void GenerateGrid(int size, int s, int seed){
        fieldSize = size;
        tileSize = 1f/size;
        scaler = s;
        transform.localScale = new Vector3(scaler,scaler, 1);
        random = new System.Random(seed);

        grid = new Dictionary<Vector2, Tile>();
        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                var spawnedTile = Instantiate(tilePrefab, getPos(x, y), Quaternion.identity, transform);
                spawnedTile.transform.localScale = new Vector3(tileSize, tileSize, 1);
                spawnedTile.name = "Tile_" + x + "_" + y;
                spawnedTile.sizeOfFiled = fieldSize;
                grid[new Vector2(x,y)] = spawnedTile;
                spawnedTile.possitionOnGrid = new Vector2(x,y);
            }
        }
    }

    private float getXPos(int x){
        return scaler*((tileSize/2) + x*tileSize - 0.5f) + transform.position.x;
    }

    private float getYPos(int y){
        float offset = tileSize * scaler;
        return scaler*((tileSize/2) - y*tileSize + 0.5f) + transform.position.y - offset;
    }

    /// <summary>
    /// callculating position
    /// </summary>
    /// <param name="x">x param in grid</param>
    /// <param name="y">y param in grid</param>
    /// <returns>position of the tile.transform</returns>
    private Vector3 getPos (int x, int y){
        return new Vector3(getXPos(x), getYPos(y), 1);
    }


    /// <summary>
    /// returns tile at given Position
    /// Strictly bound to PipeInput
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Tile GetTileAtPosition(Vector2Int pos){
        if(grid.TryGetValue(pos, out var tile)){
            return tile;
        }
        return null;
    }

    /// <summary>
    /// Moving method
    /// Strictly bound to PipeInput
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>true if tile is occupied and unit is moveable</returns>
    public bool CanGetTileAtPosition(Vector2Int pos){
        return grid[pos].isOccupied && grid[pos].occupiedUnit.IsMoveable && !minigame.isFinished;
    }

    /// <summary>
    /// swaping units and given Tiles, tiles are given by Vector2 position board[y][x]
    /// Strictly bound to PipeInput
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="playSound">if sound should by played</param>
    public void SwapTiles(Vector2Int first, Vector2Int second, bool playSound){
        if(CanGetTileAtPosition(first) && CanGetTileAtPosition(second)){
            if(playSound)
                pipesAudioManager.PlaySFX(pipesAudioManager.move);
            grid[first].SwapUnits(grid[second]);
            grid[first].occupiedUnit.transform.position = getPos(first.x, first.y);
            grid[second].occupiedUnit.transform.position = getPos(second.x, second.y);
        }
    }

    /// <summary>
    /// randomly shuffle grid
    /// </summary>
    public void Shuffle(){
        for(int y = 1; y < fieldSize - 1; y++){
            for(int x = 1; x < fieldSize - 1; x++){
                Vector2Int ovec = new Vector2Int(x, y);
                int i = 0;
                while(i < shufflingConstant){
                    Vector2Int nvec = new Vector2Int(random.Next(1, fieldSize - 1), random.Next(1, fieldSize - 1));
                    if(CanGetTileAtPosition(ovec) && CanGetTileAtPosition(nvec)){
                        SwapTiles(ovec, nvec, false);
                        break;
                    }
                    i++;
                }
            }
        }
    }

    /// <summary>
    /// sets the grid background sprite color
    /// </summary>
    /// <param name="color"></param>
    public void setSpriteRendererColor(Color color){
        spriteRenderer.color = color;
    }
}
