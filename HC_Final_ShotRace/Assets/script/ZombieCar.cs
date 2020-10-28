using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCar : MonoBehaviour
{
    private NavMeshAgent nma;
    private float hp = 50f;
    private bool isBoomed = false;
    void GetHit(GameObject src)
    {
        float hurt = src.GetComponent<BulletBase>().attack;

        hp -= hurt;
        if (hp <= 0 && !isBoomed)
        {
            StartCoroutine(boom());
            GameObject.Find("SpawnPoint").GetComponent<ZombieSpawn>().zombieCount--;
        }
    }

    private IEnumerator boom()
    {
        isBoomed = true;
        transform.Find("BurnFire").gameObject.SetActive(true);
        nma.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBoomed)
            nma.SetDestination(GameManager.playerCar.transform.position);
        if(transform.position.y < -1)
            StartCoroutine(boom());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !isBoomed)
        {
            StartCoroutine(boom());
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "bullet")
            GetHit(other);
    }
    private void OnTriggerEnter(Collider cl)
    {
        if (cl.gameObject.tag == "bullet")
            GetHit(cl.gameObject);
    }
}
