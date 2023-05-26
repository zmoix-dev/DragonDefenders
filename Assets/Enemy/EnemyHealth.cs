using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints;
    int currentHitPoints;

    void OnEnable() {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit();
        ProcessShouldKill();
    }

    void ProcessHit() {
        Debug.Log("Enemy hit!");
        currentHitPoints--;
    }

    void ProcessShouldKill() {
        if (currentHitPoints == 0) {
            gameObject.SetActive(false);
        }
    }
}
