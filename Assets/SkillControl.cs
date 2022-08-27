using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhantomProjects.Managers;

public class SkillControl : MonoBehaviour
{
    GameObject parentTree;
    int[,] skill;

    [SerializeField] int id;
    int target;
    [SerializeField] Text price;
    [SerializeField] GameObject purchasedImage;
    [SerializeField] GameObject button;
    [SerializeField] GameObject purchased;

    public bool buyClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        parentTree = GameObject.FindObjectOfType<UpgradeMenu>().gameObject;
        skill = parentTree.GetComponent<UpgradeMenu>().skills;

        UpdateSkill();

        //if (skill[target, 0] == 1)
        //{
        //    buyClicked = true;

        //    // Disable buttons
        //    button.SetActive(false);
        //    purchased.SetActive(true);

        //    // Update skill Image after purchase
        //    purchasedImage.SetActive(false);

        //    price.text = "0";
        //}
    }


    public void purchasedClick()
    {
        Purchased();
    }

    void Purchased()
    {
        if(parentTree.GetComponent<UpgradeMenu>().currentPoints >= skill[target, 2])
        {
            buyClicked = true;
            
            parentTree.GetComponent<UpgradeMenu>().Unlock(target);

            // Disable buttons
            button.SetActive(false);
            purchased.SetActive(true);

            // Update skill Image after purchase
            purchasedImage.SetActive(false);
        }
    }

    public void Refresh()
    {
        skill = parentTree.GetComponent<UpgradeMenu>().skills;
        UpdateSkill();
    }

    void UpdateSkill()
    {
        for (int i = 0; i < 13; i++)
        {
            if (skill[i, 1] == id)
            {
                if (i == 12 || i == 9 || i == 6 || i == 3 || i == 0)
                {
                    button.SetActive(true);
                }
                else
                {
                    button.SetActive(false);
                }

                if (i != 0 && skill[i - 1, 0] == 1)
                {
                    button.SetActive(true);
                }

                if (id == 99 && (skill[8, 0] == 1  && skill[11, 0] == 1))
                {
                    button.SetActive(true);
                }

                target = i;
                price.text = "" + skill[i, 2];
                return;
            }
        }
    }
}
