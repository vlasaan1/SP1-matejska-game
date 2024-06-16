using UnityEngine;

/// <summary>
/// This script has to be in every minigame prefab
/// </summary>
public class Minigame : MonoBehaviour
{
    public float startTime; //Time at which minigame starts
    public float endTime; //Time at which minigame ends
    public int score=0; //Exact number doesnt matter, player with the largest number is first, next is second etc. 
    public int seed; //You can use this to seed Random, this seed is randomly generated, but same for all instances in one minigame
    public int playerId; //Id of current minigame in range 0 - (numberOfPlayers-1)
    public bool isFinished = false; //Set this to true when your minigame ends sooner then endTime, if all instances ends, minigame is ended
}