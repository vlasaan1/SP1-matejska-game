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

    private void PrintPath(List<Vector2> path){
        for(int i = 0; i < path.Count; i++){
            Debug.Log(path[i].y + " " + path[i].x + '\n');
        }
    }

    private bool ValidateCoords(Vector2 coords)
    {
        return coords.x >= 0 && coords.y >= 0 && coords.x < size && coords.y < size;
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

    private List<Vector2> ShowPath(Dictionary<Vector2, string> board, Vector2 start, Vector2 end, Dictionary<Vector2, Vector2> P)
    {
        List<Vector2> path = new List<Vector2>();
        path.Add(end);
        Vector2 curr = end;
        while (curr != start)
        {
            curr = P[curr];
            path.Add(curr);
        }
        path.Reverse();
        SymbolsPrint(board, start, end, path);
        return path;
    }

    private void SymbolsPrint(Dictionary<Vector2, string> board, Vector2 start, Vector2 end, List<Vector2> path)
    {
        for (int i = 1; i < path.Count - 1; i++)
        {
            board[path[i]] = "#";
        }
    }
    private void ProcessNeigh(Vector2 next, Vector2 prev, Dictionary<Vector2, string> board, Dictionary<Vector2, Vector2> P, Queue<Vector2> q, HashSet<Vector2> visited)
    {
        if (ValidateCoords(next) && !visited.Contains(next) && (board[next] == "V" || board[next] == "E"))
        {
            P[next] = prev;
            q.Enqueue(next);
            visited.Add(next);
        }
    }
    private (bool, Dictionary<Vector2, string>, List<Vector2>) BFS (Dictionary<Vector2, string> board, Vector2 start, Vector2 end){
        HashSet<Vector2> visited = new HashSet<Vector2>();
        Dictionary<Vector2, Vector2> P = new Dictionary<Vector2, Vector2>();
        Queue<Vector2> q = new Queue<Vector2>();
        List<Vector2> path = new List<Vector2>();
        bool found = false;

        q.Enqueue(start);
        visited.Add(start);

        while (q.Count > 0)
        {
            Vector2 curr = q.Dequeue();
            if (curr == end)
            {
                found = true;
                path = ShowPath(board, start, end, P);
                break;
            }
            else
            {
                ProcessNeigh(new Vector2(curr.x + 1, curr.y), curr, board, P, q, visited);
                ProcessNeigh(new Vector2(curr.x - 1, curr.y), curr, board, P, q, visited);
                ProcessNeigh(new Vector2(curr.x, curr.y + 1), curr, board, P, q, visited);
                ProcessNeigh(new Vector2(curr.x, curr.y - 1), curr, board, P, q, visited);
            }
        }

        return (found, board, path);
    }

    private (Dictionary<Vector2, string>, List<Vector2>) ObstaclesAndPath(Dictionary<Vector2, string> board, Vector2 start, Vector2 end){
        Dictionary<Vector2, string> copy = MakeDeepCopy(board);
        for(int i = 0; i < obstacles; i++){
            PlaceObstacles(copy);
        }
        (bool found, Dictionary<Vector2, string> nboard, List<Vector2> path) = BFS(copy, start, end);
        if(found){
            return (copy, path);
        }
        else{
            return ObstaclesAndPath(board, start, end);
        }
    }

    public void GenerateMap(int ssize, int oobstacles)
    {
        size = ssize;
        obstacles = oobstacles;
        Dictionary<Vector2, string> board = new Dictionary<Vector2, string>();
        List<Vector2> path;

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
        (board, path) = ObstaclesAndPath(board, start, end);
        PrintBoard(board);
        PrintPath(path);
    }
    
}
