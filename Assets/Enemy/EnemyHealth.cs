using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHandler))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField][Range(0, 10)] int maxHitPoints;

    [Tooltip("Adds amount to maxHitPoints when enemy slain.")]
    [SerializeField][Range(0,2)] int scalingHitPoints = 2;
    int currentHitPoints;

    EnemyHandler handler;
    GridManager gridManager;

    void Awake() {
        handler = FindObjectOfType<EnemyHandler>();
        gridManager = FindObjectOfType<GridManager>();
    }

    void OnEnable() {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit();
        ProcessShouldKill();
    }

    void ProcessHit() {
        currentHitPoints--;
    }

    void ProcessShouldKill() {
        if (currentHitPoints == 0) {
            gameObject.SetActive(false);
            handler.AwardGold();
            maxHitPoints += scalingHitPoints;
            IncreaseLocationDanger();
        }
    }

    void IncreaseLocationDanger() {
        Vector2Int coords = gridManager.GetCoordinatesFromPosition(transform.position);
        gridManager.IncrementNodeWeight(coords);
        // foreach (Vector2Int direction in Direction.Ordinals) {
        //     gridManager.IncrementNodeWeight(coords + direction);
        // }
    }
}
