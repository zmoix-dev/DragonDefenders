using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] int cost = 100;
    [SerializeField] float buildTime = 1f;

    GameObject structuresParent;
    Bank bank;

    void Start() {
        StartCoroutine(Build());
    }

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

    IEnumerator Build() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child) {
                grandchild.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTime);
            foreach (Transform grandchild in child) {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
