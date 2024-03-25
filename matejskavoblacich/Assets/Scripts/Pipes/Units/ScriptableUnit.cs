using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Type type;
    public BaseUnit unitPrefab;
    public Name uName;
}

public enum Type {
    Pipe = 0,
    Obstacle = 1,
    Core = 2
}

public enum Name{
    StraightPipe = 0,
    RoundPipe = 1,
    Wall = 2,
    Bomb = 3,
    EndPipe = 4,
    StartPipe = 5,
}