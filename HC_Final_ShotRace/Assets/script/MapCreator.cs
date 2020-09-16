using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public int buildingAmount = 10;
    public GameObject[] buildings;
    public Transform ground;
    public Vector2 limit = new Vector2(0f, 1f);
    private void Awake()
    {
        print(buildings.Length);
        if (buildings.Length < 1)
            return;
        for(int i=0; i< buildingAmount; i++)
        {
            int rid = (int)(Random.Range(0f, 1f) * (buildings.Length-1));
            Vector3 buildPos = new Vector3(ground.localScale.x * Random.Range(limit.x, limit.y), 0, ground.localScale.z * Random.Range(limit.x, limit.y));
            Quaternion rot = new Quaternion(0,0,0,0);
            GameObject obj = Object.Instantiate(buildings[rid], buildPos, rot);
            print("building:" + rid + " pos:" + buildPos);
        }
    }
}
