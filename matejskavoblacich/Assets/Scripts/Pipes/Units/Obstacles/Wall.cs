using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : BaseObstacle
{
    [SerializeField] Sprite roundWallSprite;
    private bool swapedSprite = false;
    public override void CalculateRotation()
    {
        Vector2 pos = occupiedTile.possitionOnGrid;
        int sizeOfField = occupiedTile.sizeOfFiled;
        swapToCornerWall(pos, sizeOfField);
        if(swapedSprite){
            if(pos.x == 0 && pos.y == sizeOfField - 1){
                transform.Rotate(Vector3.forward, 90f);
            }
            else if(pos.x == sizeOfField - 1 && pos.y == sizeOfField - 1){
                transform.Rotate(Vector3.forward, 180f);
            }
            else if(pos.x == sizeOfField - 1 && pos.y == 0){
                transform.Rotate(Vector3.forward, 270f);
            }
        }
        else{
            if(pos.y == 0 || pos.y == sizeOfField - 1){
                transform.Rotate(Vector3.forward, 90f);
            }
        }
        rotation = transform.rotation.z;
    }

    private void swapToCornerWall(Vector2 pos, int sizeOfField){
        if(math.abs(pos.x - pos.y) == 0 || math.abs(pos.x - pos.y) == sizeOfField - 1){
            spriteRenderer.sprite = roundWallSprite;
            swapedSprite = true;
        }
    }
}
