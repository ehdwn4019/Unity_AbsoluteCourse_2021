using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);

        foreach(Transform point in spawnPointGroup)
        {
            points.Add(point);
        }
    }
}
