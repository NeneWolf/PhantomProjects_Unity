using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

public class UpgradeMenu : MonoBehaviour, IDataPersistance
{
    public int currentPoints { get; private set; }
    int abilityPrice;
    string abilityName;

    GameObject UIManager;
    GameObject UpgradeSystem;
    bool loadedTree = false;
    List<SkillControl> abilities;

    public int[,] skills { get; private set; }

    private void Awake()
    {
        print("Tree awake");
        UIManager = GameObject.FindObjectOfType<UIManager>().gameObject;
        UpgradeSystem = GameObject.FindObjectOfType<UpgradeMenu>().gameObject;

        //if (loadedTree == false)
        //{


        //    loadedTree = true;
        //}
        
        //print(skills); // row, collum
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
            RefreshAllTheInstanceOfSkillUnbought();
        }
    }

    void RefreshAllTheInstanceOfSkillUnbought()
    {
        abilities = FindAllSkillsControls();

        foreach(SkillControl skillControl in abilities)
        {
            if(!skillControl.buyClicked)
                skillControl.Refresh();
        }
    }

    private List<SkillControl> FindAllSkillsControls()
    {
        IEnumerable<SkillControl> abilities = FindObjectsOfType<SkillControl>();

        return new List<SkillControl>(abilities);
    }

    public void LoadData(GameData data)
    {
        print("Loaded Tree");
        //loadedTree = data.saveTree;

        if(data.skillTree != null)
            skills = data.skillTree;
        else
        {
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
            {0,51,10},
            {0,52,10},
            {0,53,10},
            {0,99,10}
            };
        }
    }

    public void SaveData(GameData data)
    {
        print("Save");
        
        data.saveTree = loadedTree;
        data.skillTree = skills;
    }
}
