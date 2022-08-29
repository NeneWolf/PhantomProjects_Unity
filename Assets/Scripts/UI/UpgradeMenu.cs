using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;
using System;
using System.Linq;

public class UpgradeMenu : MonoBehaviour, IDataPersistance
{
    public int currentPoints { get; private set; }
    int abilityPrice;
    string abilityName;

    GameObject UIManager;
    GameObject UpgradeSystem;
    //GameObject UpgradeManager;
    bool loadedTree = false;
    List<SkillControl> abilities;

    int rows = 0;
    string skillString = string.Empty;
    public int[,] skills { get; private set; }

    
    int startPosition;
    int endPosition;

    private void Start()
    {
        //print("Tree awake");
        UIManager = GameObject.FindObjectOfType<UIManager>().gameObject;
        UpgradeSystem = GameObject.FindObjectOfType<UpgradeManager>().gameObject;
    }

    private void Update()
    {
        currentPoints = UIManager.GetComponent<UIManager>().currentMutationPoints;
    }

    public void Unlock(int abilityNumber)
    {
        if (currentPoints >= skills[abilityNumber,2])
        {
            skills[abilityNumber, 0] = 1;
            UIManager.GetComponent<UIManager>().MutationPointsRemove(skills[abilityNumber,2]);
            UpgradeSystem.GetComponent<UpgradeManager>().UpdateSkill(skills[abilityNumber,1]);
            RefreshAllTheInstanceOfSkillUnbought();
        }
    }

    //Load To Game Data
    public void LoadData(GameData data)
    {
        //print("Loaded Tree");
        loadedTree = data.saveTree;
        RevertToArray(data);
        RefreshAllTheInstanceOfSkillUnbought();

    }

    // Revert From string to Array
    void RevertToArray(GameData data)
    {
        // By Default always create this array with the default values
        skills = new int[,]
        {
            //{Bool,id, price}
            {0,11,10},
            {0,12,10},
            {0,13,10},
            {0,21,10},
            {0,22,10},
            {0,23,10},
            {0,31,10},
            {0,32,10},
            {0,33,10},
            {0,41,10},
            {0,42,10},
            {0,43,10},
            {0,99,10},
            {0,51,10},
            {0,52,10},
            {0,53,10}

        };

        //If it's a saved tree, load it on top of the created one
        if (loadedTree == true)
        {
            skillString = data.skillTree;

            int j = 0;


            for (int i = 0; i < skillString.Length; i++)
            {
                if (skillString[i] == '{')
                {
                    startPosition = i + 1;
                }
                else if (skillString[i] == '}')
                {
                    endPosition = i;
                    string ability = skillString.Substring(startPosition, endPosition - startPosition);
                    string[] abilitySplit = ability.Split(',');
                    skills[j, 0] = int.Parse(abilitySplit[0]);
                    skills[j, 1] = int.Parse(abilitySplit[1]);
                    skills[j, 2] = int.Parse(abilitySplit[2]);
                    j++;
                }
            }
        }
    }

    //Save to Game Data
    public void SaveData(GameData data)
    {

        loadedTree = true;
        GenerateAsString();
        data.saveTree = loadedTree;
        data.skillTree = skillString;
    }

    void GenerateAsString()
    {
        int nRows = skills.GetLength(0);
        int nCollumns = skills.GetLength(1);

        var results = string.Join(",",
            Enumerable.Range(0, skills.GetUpperBound(0) + 1)
                .Select(x => Enumerable.Range(0, skills.GetUpperBound(1) + 1)
                    .Select(y => skills[x, y]))
                .Select(z => "{" + string.Join(",", z) + "}"));


        skillString = results;
    }

    //Refresh each skill
    void RefreshAllTheInstanceOfSkillUnbought()
    {
        abilities = FindAllSkillsControls();

        foreach (SkillControl skillControl in abilities)
        {
            if (!skillControl.buyClicked)
                skillControl.Refresh();
        }
    }

    private List<SkillControl> FindAllSkillsControls()
    {
        IEnumerable<SkillControl> abilities = FindObjectsOfType<SkillControl>();

        return new List<SkillControl>(abilities);
    }
}
