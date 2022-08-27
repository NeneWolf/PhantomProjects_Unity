using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject purchased;

    public void Reset()
    {
        button.SetActive(true);
        purchased.SetActive(false);
    }

    public void Purchased()
    {
        button.SetActive(false);
        purchased.SetActive(true);
    }
}
