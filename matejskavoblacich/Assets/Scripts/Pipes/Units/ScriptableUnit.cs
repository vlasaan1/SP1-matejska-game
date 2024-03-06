using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Type type;
    public BaseUnit unitPrefab;
}

public enum Type {
    Pipe = 0,
    Obstacle = 1,
    Core = 2
}