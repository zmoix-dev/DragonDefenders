using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectiles;
    [SerializeField] float range = 15f;
    [SerializeField] Transform target;

    AudioSource sfx;
    TowerHandler towerHandler;
    void Start() {
        target = GameObject.FindObjectOfType<EnemyHandler>().transform;
        towerHandler = GetComponent<TowerHandler>();
        sfx = GetComponent<AudioSource>();
    }
    void Update() {
        if (towerHandler.IsReady) {
            ProcessTargeting();
            ProcessAttack();
        }
    }

    void ProcessTargeting() {
        if (target == null || !target.gameObject.activeSelf || !IsTargetInRange()) {
            FindClosestTarget();
        }
    }

    bool IsTargetInRange() {
        return Vector3.Distance(transform.position, target.transform.position) < range;
    }

    void FindClosestTarget() {
        EnemyHandler[] enemies = FindObjectsOfType<EnemyHandler>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (EnemyHandler enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance) {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }   

    void ProcessAttack() {
        if (target == null) return;
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        bool shouldShoot = targetDistance < range;
        if (shouldShoot) {
            Attack();
        } else {
            Disengage();
        }
    }

    void Attack() {
        Vector3 targetAdjustedHeight = target.position + new Vector3(0, target.localScale.y / 2, 0);
        weapon.LookAt(targetAdjustedHeight);
        if (!sfx.isPlaying) {
            sfx.Play();
        }
        if (!projectiles.isEmitting) {
            projectiles.Play();
        }
    }

    void Disengage() {
        if (sfx.isPlaying) {
            sfx.Stop();
        }
        if (projectiles.isEmitting) {
            projectiles.Stop();
        }
    }
}
