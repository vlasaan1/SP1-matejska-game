using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    private List<ScriptableUnit> units;

    void Awake(){
        instance = this;

        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }
}
