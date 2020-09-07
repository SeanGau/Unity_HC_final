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
    public float accel = 2000f;
    public float breakv = 5000f;
    public float steer = 20;

    static int checkcount = 0;
    public Text checkText;
    public Text speedText;
    public Image torqueImage;

    private Rigidbody rb;
    private float nowTorque = 0;
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
        torqueImage.fillAmount = nowTorque / accel * 0.5f;
    }
    private void Move()
    {
        float v = Input.GetAxis("Vertical");
        nowTorque = Mathf.Lerp(nowTorque, accel * v, 0.5f * Time.deltaTime * 10);
        wheel_br.motorTorque = wheel_bl.motorTorque = wheel_fr.motorTorque = wheel_fl.motorTorque = nowTorque;
        rb.AddForce(0, -1 * nowTorque, 0);

        if (Input.GetKey(KeyCode.DownArrow))
        {
            print(rb.velocity.sqrMagnitude);
            if (rb.velocity.sqrMagnitude > 1)
            {
                wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = breakv * 1000;
                rb.AddForce(rb.velocity * -breakv);
            }
            else
            {
                wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
            }
        }
        else
        {
            wheel_br.brakeTorque = wheel_bl.brakeTorque = wheel_fr.brakeTorque = wheel_fl.brakeTorque = 0;
            rb.AddForce(rb.velocity* -10);
        }
        float h = Input.GetAxis("Horizontal");
        wheel_fr.steerAngle = wheel_fl.steerAngle =  h * steer;
        wheel_fl_obj.rotation = wheel_fr_obj.rotation = Quaternion.Euler(0, h * steer, 0);
        /*
        if (h!=0)
        {
            wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, h*steer, 0.9f * Time.deltaTime * 10);
        }
        else
        {
            wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, 0, 0.9f * Time.deltaTime *10);            
        }*/


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
