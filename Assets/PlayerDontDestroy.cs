using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDontDestroy : MonoBehaviour
{
    GameObject[] otherPlayers;
    GameObject gameManager;
    PlayerStats stats;
    Transform spawnPosition;

    private void OnEnable()
    {
        this.transform.position = GameObject.Find("PlayerSpawnPosition").gameObject.transform.position;

        otherPlayers = GameObject.FindGameObjectsWithTag("Player");
        stats = GetComponent<PlayerStats>();

        if(otherPlayers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
