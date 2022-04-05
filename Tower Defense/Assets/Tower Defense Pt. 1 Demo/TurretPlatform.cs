using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurretPlatform : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;
    
    private Renderer rend;
    private Color startColor;

    public int Coins = 20;

    private BuildingManager buildingManager;
    public GameObject coinText;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        coinText.GetComponent<TextMeshProUGUI>().text = Coins.ToString().PadLeft(2, '0');

        buildingManager = BuildingManager.instance;
    }

    private void OnMouseDown()
    {
        if (buildingManager.GetTurretToBuild() == null)
        {
            return;
        }
        
        if (turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        if (PlayerStats.Money < 12)
        {
            Debug.Log("Not Enough coins");
            return;
        }
        
        if (Coins >= 12)
        {
            GameObject turretToBuild = buildingManager.GetTurretToBuild();
            turret = (GameObject) Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
            PlayerStats.Money -= 12;
        }
    }

    private void OnMouseEnter()
    {
        if (buildingManager.GetTurretToBuild() == null)
        {
            return;
        }
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
