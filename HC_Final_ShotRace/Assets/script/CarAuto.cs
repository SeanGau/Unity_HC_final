using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CarAuto : MonoBehaviour
{
    public WheelCollider wheel_fr;
    public WheelCollider wheel_fl;
    public WheelCollider wheel_br;
    public WheelCollider wheel_bl;
    public float accel = 4000f;
    public float breakv = 200f;
    public float steer = 30;
    public float speedLimit = 100f;
    public float hp = 100f;
    
    public GameObject burnFire;
    public Transform weaponPoint;

    private Rigidbody rb;
    private bool isDead = false;

    Vector3 localVelocity = Vector3.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        burnFire.SetActive(false);
    }
    void Start()
    {
        wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
    }

    IEnumerator GetHit(GameObject src)
    {
        print(src.tag);
        float hurt = 0;
        if(src.tag == "buildings")
        {
            Vector3 speedHit = localVelocity;
            yield return new WaitForSeconds(0.01f);
            hurt = (speedHit - localVelocity).magnitude / 2;
            if (hurt < 10)
                hurt = 0;
        }
        
        else if(src.tag == "bullet")
        {
            hurt = src.GetComponent<BulletBase>().attack;
            yield return null;
        }

        hp -= hurt;
        if (hp <= 0 && !isDead)
        {
            burnFire.SetActive(true);
            isDead = true;
            StartCoroutine(Re(false));
            rb.AddForce(transform.up * 1000 + rb.velocity*1000);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = breakv * 1000;
        }
    }

    void Update()
    {
        if (isDead)
            return;
        Move();
    }

    IEnumerator Re(bool m = true)
    {
        yield return new WaitForSeconds(3f);
        if(Input.GetKey(KeyCode.R) == true || !m)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
            transform.position = new Vector3(10,1,10);
            isDead = false;
            hp = 100;
            burnFire.SetActive(false);
            if (weaponPoint.childCount != 0)
                Destroy(weaponPoint.GetChild(0).gameObject);
        }
    }
    private void Move()
    {
        wheel_fl.motorTorque = wheel_fr.motorTorque = 1000;
        wheel_fr.steerAngle = wheel_fl.steerAngle =  25;

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Re());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            StartCoroutine(GetHit(other.gameObject));
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        StartCoroutine(GetHit(other));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "buildings")
        {
            StartCoroutine(GetHit(collision.gameObject));
        }
    }    
}
