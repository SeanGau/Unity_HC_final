using UnityEngine;
using System.Collections;

public class BulletBase : MonoBehaviour
{
    [Header("攻擊力"), Range(0, 100)]
    public float attack = 2.5f;
    [Header("速度"), Range(0, 5000)]
    public float speed = 2000;
    [Header("類別"), Range(1,3)]
    public int cat = 1;
    [Header("射程"), Range(10, 200)]
    public float distance = 50;
    [Header("爆炸特效")]
    public GameObject Effects;
    [Header("爆炸音效")]
    public AudioClip Boom;
    [Header("音量")]
    public float volume = 1;

    public Rigidbody rig;
    private Vector3 posA;
    private float t1;
    public Transform Point;
    public AudioSource aud;
    bool isHit= false;

    private IEnumerator Distance()
    {
        float dis = Vector3.Distance(posA,transform.position);
        while(!isHit)
        {
            if(Time.time - t1 > distance/25)
            {
                break;
            }
            yield return null;
        }
        var boomb = Instantiate(Effects, Point.position, Point.rotation);
        boomb.SetActive(true);
        boomb.GetComponent<AudioSource>().PlayOneShot(Boom, volume*0.05f);
        isHit = true;

        Destroy(boomb,1);

        Destroy(gameObject);
        /*
        if (dis >= distance || isHit)
        {
            var boomb = Instantiate(Effects, Point.position, Point.rotation);
            boomb.SetActive(true);
            boomb.GetComponent<AudioSource>().PlayOneShot(Boom, volume);
            isHit = true;

            Destroy(boomb,1);

            Destroy(gameObject);

            yield return null;
        }*/
    }

    private void Update()
    {
        if (cat == 1)
        {
            rig = GetComponent<Rigidbody>();
            rig.velocity = GetComponentInParent<Rigidbody>().velocity;
            rig.AddForce(transform.up * speed * Time.deltaTime);
            StartCoroutine(Distance());
        }
    }

    private void Awake()
    {
        if (cat == 1)
        {
            posA = transform.position;
            t1 = Time.time;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "bullet" && other.gameObject.tag != "checkpoint")
            isHit = true;
    }
}