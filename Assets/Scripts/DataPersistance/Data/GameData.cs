using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool loadedPlayer;
    public int characterSelected;

    public float currentHealth;
    public float currentEnergy;
    public int mutationPoints;

    //Level
    public int currentLevelIndex;
    public int modeSelected;

    //Skill Tree
    public bool saveTree;
    public string skillTree;

    //TODO: Add time played on this "save"


}
