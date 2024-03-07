using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.WebSockets;

// using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class GeneratingAlgo : MonoBehaviour
{

    public static GeneratingAlgo instance;

    void Awake(){
        instance = this;
    }

    private class Item
    {
        public int x;
        public int y;

        public Item(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Item))
                return false;
            Item other = (Item)obj;
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            return (x, y).GetHashCode();
        }
    }

    private void printBoard(int size, Dictionary<Vector2, string> board){
        string matrixString = "\n";
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Vector2 pos = new Vector2(row, col);
                matrixString += board[pos];
            }
            matrixString += "\n";
        }
        Debug.Log(matrixString);
    }

    public void GenerateMap(int size, int obstacles)
    {
        Dictionary<Vector2, string> board = new Dictionary<Vector2, string>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(i == 0 || j == 0 || i == size - 1 || j == size -1){
                    board[new Vector2(i,j)] = "X";
                }
                else{
                    board[new Vector2(i, j)] = " ";
                }
            }
        }

        printBoard(size, board);

    }
    
}
