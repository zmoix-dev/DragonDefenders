using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHandler))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    EnemyHandler handler;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        StartCoroutine(TraversePath());
    }

    void Start() {
        handler = FindObjectOfType<EnemyHandler>();
    }

    void ReturnToStart() {
        FindPath();
        if (path.Count > 0) {
            transform.position = path[0].transform.position;
            path.RemoveAt(0);
        }
    }

    void FindPath() {
        path.Clear();

        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform waypoint in pathParent.transform) {
            Tile way = waypoint.GetComponent<Tile>();
            if (way != null) {
                path.Add(way);
            }
        }
    }

    IEnumerator TraversePath() {
        foreach (Tile wp in path) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(wp.transform.position.x, transform.position.y, wp.transform.position.z);
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
