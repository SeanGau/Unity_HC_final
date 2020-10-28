using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour
{
    public WheelCollider wheel_fr;
    public WheelCollider wheel_fl;
    public WheelCollider wheel_br;
    public WheelCollider wheel_bl;
    public Transform wheel_fr_obj;
    public Transform wheel_fl_obj;
    public float accel = 4000f;
    public float breakv = 200f;
    public float steer = 30;
    public float speedLimit = 100f;
    public float hp = 100f;

    static int checkcount = 0;
    public Text checkText;
    public Text speedText;
    public Image torqueImage;
    public Image hpImage;
    public Text gunName;
    public Text bulletCount;

    public Vector3 lastCheckpoint = new Vector3(0, 0, 0);
    public GameObject burnFire;
    public Transform weaponPoint;

    private Rigidbody rb;
    private float nowTorque = 0;
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
        ChangeWeapon(2);
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

        else if(src.tag == "zombie")
        {
            hurt = 5;
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
        UpdateDashboard();
        if (isDead)
            return;
        Move();
    }

    public void ChangeWeapon(int cat)
    {
        switch (cat)
        {
            case 1: //messil
                weaponPoint.GetChild(0).gameObject.SetActive(false);
                weaponPoint.GetChild(1).gameObject.SetActive(true);
                gunName.text = "missile";
                bulletCount.text = weaponPoint.GetComponentInChildren<MessileControl>().bullet.ToString();
                break;
            case 2:
                weaponPoint.GetChild(0).gameObject.SetActive(true);
                weaponPoint.GetChild(1).gameObject.SetActive(false);
                bulletCount.text = "n/a";
                gunName.text = "minigun";
                break; //minigun
        }
    }
    void UpdateDashboard()
    {
        int forwardSpeed = Mathf.Abs((int)localVelocity.z);
        int dSpeed = (int)Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);
        speedText.text = dSpeed.ToString();
        torqueImage.fillAmount = Mathf.Abs(nowTorque) / accel * 0.5f;
        hpImage.fillAmount = hp / 100;
    }

    IEnumerator Re(bool m = true)
    {
        yield return new WaitForSeconds(3f);
        if(Input.GetKey(KeyCode.R) == true || !m)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
            transform.position = lastCheckpoint + new Vector3(0,1,0);
            rb.velocity = Vector3.zero;
            isDead = false;
            hp = 100;
            burnFire.SetActive(false);
        }
    }
    private void Move()
    {
        localVelocity = transform.InverseTransformDirection(rb.velocity);
        rb.AddForce(-transform.up * localVelocity.sqrMagnitude * Time.deltaTime * 50);

        float v = Input.GetAxis("Vertical");
        nowTorque = Mathf.Lerp(nowTorque, accel * v, 0.5f * Time.deltaTime * 50);
        if (Input.GetKey(KeyCode.Space)) //手煞車
        {
            rb.AddForce(rb.velocity * -breakv * Time.deltaTime * 5);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = breakv * 1000;
        }
        else if(localVelocity.z * v < -10f || v==0) //反向或滑行
        {
            rb.AddForce(rb.velocity * -breakv * Time.deltaTime);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = breakv * 1000;
        }
        else
        {
            wheel_br.brakeTorque = wheel_bl.brakeTorque = 0;
        }
        wheel_br.motorTorque = wheel_bl.motorTorque = wheel_fr.motorTorque = wheel_fl.motorTorque = nowTorque;

        float dSpeed = speedLimit - Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);
        if (dSpeed > 0)
        {
            rb.AddForce(transform.forward * v * Time.deltaTime * accel * dSpeed);
        }
        else
        {
            rb.AddForce(transform.forward * v * Time.deltaTime * accel * dSpeed * dSpeed * -1);
        }
        float h = Input.GetAxis("Horizontal");
        wheel_fr.steerAngle = wheel_fl.steerAngle =  h * steer;

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Re());
        }
    }
    private void OnTriggerEnter(Collider cl)
    {
        switch (cl.gameObject.tag)
        {
            case "checkpoint":
                checkcount++;
                checkText.text = checkcount.ToString();
                lastCheckpoint = transform.position;
                break;
            case "weapon":
                /*
                if (weaponPoint.childCount != 0)
                    return;
                cl.GetComponent<BoxCollider>().isTrigger = false;
                cl.gameObject.transform.position = weaponPoint.position;
                cl.gameObject.transform.SetParent(weaponPoint);
                cl.transform.GetChild(0).GetComponent<GunBase>().Set();*/
                Destroy(cl.gameObject);
                weaponPoint.GetChild(1).GetComponentInChildren<GunBase>().bullet+=20;
                ChangeWeapon(1);
                break;
            case "bullet":
                //StartCoroutine(GetHit(cl.gameObject));
                break;
        }
    }    

    private void OnParticleCollision(GameObject other)
    {
        StartCoroutine(GetHit(other));
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(GetHit(collision.gameObject));
    }        
}
