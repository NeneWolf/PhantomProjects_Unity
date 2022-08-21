using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDontDestroy : MonoBehaviour
{
    GameObject[] otherPlayers;
    GameObject gameManager;
    PlayerStats stats;
    Transform spawnPosition;

    private void Update()
    {
        FindOtherPlayers();       
    }


    void FindOtherPlayers()
    {
        otherPlayers = GameObject.FindGameObjectsWithTag("Player");

        if (otherPlayers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
