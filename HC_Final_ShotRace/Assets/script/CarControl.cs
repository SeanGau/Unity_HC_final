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

    static int checkcount = 0;
    public Text checkText;
    public Text speedText;
    public Image torqueImage;

    private Rigidbody rb;
    private float nowTorque = 0;
    private int nowGear = 0; // P: 0, D: 1, R: -1
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        UpdateDashboard();
    }
    void UpdateDashboard()
    {
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        int forwardSpeed = Mathf.Abs((int)localVelocity.z);
        int dSpeed = (int)Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);
        speedText.text = dSpeed.ToString();
        torqueImage.fillAmount = Mathf.Abs(nowTorque) / accel * 0.5f;
    }
    private void Move()
    {
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        if(localVelocity.z > 0.1)
        {
          nowGear = 1;
        }
        else if(localVelocity.z <= -0.1)
        {
          nowGear = -1;
        }
        else
        {
          nowGear = 0;
        }
        rb.AddForce(-transform.up * localVelocity.sqrMagnitude);

        float v = Input.GetAxis("Vertical");
        if (v == 0)
        {
            rb.AddForce(rb.velocity * -breakv);
            nowTorque = Mathf.Lerp(nowTorque, 0, 0.5f);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
        }
        else if (localVelocity.z * v > 0 || nowGear == 0)
        {
            nowTorque = Mathf.Lerp(nowTorque, accel * v, 0.5f);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
        }
        else
        {
            if (localVelocity.z * v < 0)
            {
              rb.AddForce(rb.velocity * -breakv);
            }
            nowTorque = Mathf.Lerp(nowTorque, 0, 0.5f);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = breakv * 1000;
        }
        wheel_br.motorTorque = wheel_bl.motorTorque = wheel_fr.motorTorque = wheel_fl.motorTorque = nowTorque;

        float h = Input.GetAxis("Horizontal");
        wheel_fr.steerAngle = wheel_fl.steerAngle =  h * steer;

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 1, 0);
        }
    }

    private void OnTriggerEnter(Collider cl)
    {
      if(cl.gameObject.tag == "checkpoint")
      {
        checkcount++;
        checkText.text = checkcount.ToString();
      }
    }
}
