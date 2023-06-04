using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] int cost = 100;

    GameObject structuresParent;
    Bank bank;

    public bool PlaceTower(TowerHandler tower, Vector3 position) {
        bank = FindObjectOfType<Bank>();  
        structuresParent = GameObject.FindWithTag("StructuresParent");

        if(bank != null && bank.Withdraw(cost)) {
            TowerHandler placed = Instantiate(tower, position, Quaternion.identity);
            if (placed != null) {
                placed.transform.parent = structuresParent.transform;    
            }
            return true;
        }
        return false;
    }
}
