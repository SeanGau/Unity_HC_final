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

    static int checkcount = 0;
    public Text checkText;
    public Text speedText;
    public Image torqueImage;

    private Rigidbody rb;
    private float nowTorque = 0;
    private int nowGear = 0; // P: 0, D: 1, R: -1
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
    }

    void Update()
    {
        Move();
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
        if(localVelocity.z > 0.5)
        {
          nowGear = 1;
        }
        else if(localVelocity.z < -0.5)
        {
          nowGear = -1;
        }
        else
        {
          nowGear = 0;
        }
        rb.AddForce(-transform.up * localVelocity.sqrMagnitude * Time.deltaTime * 50);

        float v = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(rb.velocity * -breakv * Time.deltaTime);
            nowTorque = Mathf.Lerp(nowTorque, 0, 0.5f * Time.deltaTime * 50);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = breakv * 1000;
        }
        else if (v == 0) //滑行
        {
            rb.AddForce(rb.velocity * -breakv * Time.deltaTime);
            nowTorque = Mathf.Lerp(nowTorque, 0, 0.5f * Time.deltaTime *50);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
        }
        else if (localVelocity.z * v > 0 || nowGear == 0) //加速or低速狀態
        {
            nowTorque = Mathf.Lerp(nowTorque, accel * v, 0.5f * Time.deltaTime * 50);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
        }
        else
        {
            if (localVelocity.z * v < 0) //切換方向
            {
              rb.AddForce(rb.velocity * -breakv * Time.deltaTime * 50);
            }
            nowTorque = Mathf.Lerp(nowTorque, 0, 0.5f * Time.deltaTime *50);
            wheel_br.brakeTorque = wheel_bl.brakeTorque = breakv * 1000;
        }
        int dSpeed = (int)Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);
        if (dSpeed < speedLimit)
            wheel_br.motorTorque = wheel_bl.motorTorque = wheel_fr.motorTorque = wheel_fl.motorTorque = nowTorque;
        else
            wheel_br.motorTorque = wheel_bl.motorTorque = wheel_fr.motorTorque = wheel_fl.motorTorque = 0;
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
