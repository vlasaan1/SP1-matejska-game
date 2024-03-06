using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    [SerializeField] PlayerGrid onePlayerGrid;

    [SerializeField] int numberOfBombs = 3;

    [SerializeField] int filedSize = 6;

    [SerializeField] int numberOfPlayers = 4;

    private List<PlayerGrid> playersHolder = new List<PlayerGrid>();

    void Start()
    {
        for(int i = 0; i < numberOfPlayers; i++){
            var spawnedOnePlayerField = Instantiate(onePlayerGrid, new Vector3(i, 0, 0), Quaternion.identity, transform);
            spawnedOnePlayerField.name = "Player" + i;
            playersHolder.Add(spawnedOnePlayerField);
        }
    }

}
