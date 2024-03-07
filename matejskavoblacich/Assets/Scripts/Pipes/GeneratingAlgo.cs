using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.WebSockets;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering;

public class GeneratingAlgo : MonoBehaviour
{

    public static GeneratingAlgo instance;

    private int size = 0;

    private int obstacles = 0;

    private static System.Random random = new System.Random();

    void Awake(){
        instance = this;
    }

    private void PrintBoard(Dictionary<Vector2, string> board){
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

    private void PlaceString(Dictionary<Vector2, string> board, Vector2 pos, string str){
        board[pos] = str;
    }

    private (Vector2, Vector2) InitializeStart(Dictionary<Vector2, string> board, int size)
    {
        System.Random random = new System.Random();
        string side = new string[] { "top", "bottom", "left", "right" }[random.Next(0, 4)];

        Vector2 start, end;
        int row, col;
        
        if (side == "top")
        {
            row = 0;
            col = random.Next(1, size - 1);
            start = new Vector2(col, row);
            PlaceString(board, start, "S");
            end = new Vector2(random.Next(1, size - 1), size - 1);
            PlaceString(board, end, "E");
        }
        else if (side == "bottom")
        {
            row = size - 1;
            col = random.Next(1, size - 1);
            start = new Vector2(col, row);
            PlaceString(board, start, "S");
            end = new Vector2(random.Next(1, size - 1), 0);
            PlaceString(board, end, "E");
        }
        else if (side == "left")
        {
            col = 0;
            row = random.Next(1, size - 1);
            start = new Vector2(col, row);
            PlaceString(board, start, "S");
            end = new Vector2(size - 1, random.Next(1, size - 1));
            PlaceString(board, end, "E");
        }
        else // "right"
        {
            col = size - 1;
            row = random.Next(1, size - 1);
            start = new Vector2(col, row);
            PlaceString(board, start, "S");
            end = new Vector2(0, random.Next(1, size - 1));
            PlaceString(board, end, "E");
        }

        return (start, end);
    }

    private Dictionary<Vector2, string> MakeDeepCopy(Dictionary<Vector2, string> board){
        Dictionary<Vector2, string> copy = new Dictionary<Vector2, string>();
        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                Vector2 pos = new Vector2(i,j);
                copy[pos] = new string(board[pos].ToCharArray());
            }
        }
        return copy;
    }

    private void PlaceObstacles(Dictionary<Vector2, string> board){
        int rx = random.Next(1, size - 1);
        int ry = random.Next(1, size - 1);
        Vector2 pos = new Vector2(ry, rx);

        if (board[pos] == "V")
        {
            board[pos] = "B";
        }
        else
        {
            PlaceObstacles(board); // Recursive call to try again if position is not empty
        }
    }

    private Dictionary<Vector2, string> ObstaclesAndPath(Dictionary<Vector2, string> board, Vector2 start, Vector2 end){
        Dictionary<Vector2, string> copy = MakeDeepCopy(board);
        for(int i = 0; i < obstacles; i++){
            PlaceObstacles(copy);
        }
        return copy;
    }

    public void GenerateMap(int ssize, int oobstacles)
    {
        size = ssize;
        obstacles = oobstacles;
        Dictionary<Vector2, string> board = new Dictionary<Vector2, string>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(i == 0 || j == 0 || i == size - 1 || j == size -1){
                    board[new Vector2(i,j)] = "X";
                }
                else{
                    board[new Vector2(i, j)] = "V";
                }
            }
        }

        (Vector2 start, Vector2 end) = InitializeStart(board, size);
        board = ObstaclesAndPath(board, start, end);
        PrintBoard(board);
    }
    
}
