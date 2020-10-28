using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject[] zombieSets;
    public int zombieCount = 0;
    public int maxZombie = 10;
    public Transform ground;
    public Vector2 limit = new Vector2(0f, 1f);
    private IEnumerator Spawn()
    {
        Vector3 spawnPos = new Vector3(ground.localScale.x * Random.Range(limit.x, limit.y), 0, ground.localScale.z * Random.Range(limit.x, limit.y));
        transform.position = GameManager.playerCar.transform.position + (100 * new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)));        
        if(zombieCount < maxZombie)
        {
            int zombieStyle = Random.Range(0, zombieSets.Length);
            var z = Instantiate(zombieSets[zombieStyle], spawnPos, transform.rotation);
            z.SetActive(true);
            zombieCount++;
            //print("Zombie: " + zombieCount);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Spawn());
        
    }
    void Start()
    {
        StartCoroutine(Spawn());
    }
}
