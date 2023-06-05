using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHandler))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    EnemyHandler handler;
    GridManager gridManager;
    Pathfinder pathfinder;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        StartCoroutine(TraversePath());
    }

    void Awake() {
        handler = FindObjectOfType<EnemyHandler>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void ReturnToStart() {
        FindPath();
        if (path.Count > 0) {
            transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
        }
    }

    void FindPath() {
        path.Clear();
        path = pathfinder.GetNewPath();
    }

    IEnumerator TraversePath() {
        foreach (Node node in path) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(node.coordinates);
            float travelPercent = 0f;

            while(travelPercent <= 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.LookAt(endPosition);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    void FinishPath() {
        handler.StealGold();
        gameObject.SetActive(false);
    }
}
