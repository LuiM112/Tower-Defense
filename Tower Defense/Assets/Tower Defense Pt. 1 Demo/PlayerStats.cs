using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    
    public GameObject coinText;

    public int startMoney = 20;
    // Start is called before the first frame update
    void Start()
    {
        Money = startMoney;
        coinText.GetComponent<TextMeshProUGUI>().text = Money.ToString().PadLeft(2, '0');
    }

    private void Update()
    {
        coinText.GetComponent<TextMeshProUGUI>().text = Money.ToString().PadLeft(2, '0');
    }
}
