using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TraversePath());
    }

    IEnumerator TraversePath() {
        foreach (Waypoint wp in path) {
            transform.position = new Vector3(wp.transform.position.x, transform.position.y, wp.transform.position.z);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
