using System.Collections;
using System.Collections.Generic;
using initi.prefabScripts;
using UnityEngine;

public class ScoreKeeper : BaseHittable
{
    int score = 0;

    public int GetScore(){
        return score;
    }

    public void ModifyScore(int value){
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore(){
        score = 0;
    }


}
