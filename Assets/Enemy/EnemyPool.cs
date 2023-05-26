using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int poolSize = 5;
    [SerializeField] int spawnInterval = 2;
    GameObject[] pool;

    void Awake() {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    void PopulatePool() {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator Spawn() {
        while(true) {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /** Find first non-active object in pool and enable it **/
    void EnableObjectInPool() {
        foreach (GameObject obj in pool) {
            if (obj.activeSelf == false) {
                obj.SetActive(true);
                return;
            }
        }
    }
}
