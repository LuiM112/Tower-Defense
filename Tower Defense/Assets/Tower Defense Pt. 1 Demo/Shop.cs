using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildingManager buildingManager;

    private void Start()
    {
        buildingManager = BuildingManager.instance;
    }

    public void PurchaseTurrets()
    {
        Debug.Log("Turret Purchased");
        buildingManager.SetTurretToBuild(buildingManager.turrentPrefab);
    }
}
