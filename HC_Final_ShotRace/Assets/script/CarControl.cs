using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public WheelCollider wheel_fr;
    public WheelCollider wheel_fl;
    public WheelCollider wheel_br;
    public WheelCollider wheel_bl;
    public float accel = 1000f;
    public float steer = 30;

    private Rigidbody rb;
    private float nowTorque = 0;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            nowTorque = Mathf.Lerp(nowTorque, accel, 0.7f * Time.deltaTime * 10);
        }
        else
        {
            nowTorque = Mathf.Lerp(nowTorque, 0, 0.7f * Time.deltaTime * 10);
        }
        wheel_fr.motorTorque = wheel_fl.motorTorque = nowTorque;
        rb.AddForce(0, -1 * nowTorque, 0);
        print(nowTorque);

        if (Input.GetKey(KeyCode.DownArrow))
        {
            wheel_br.brakeTorque = wheel_bl.brakeTorque = accel;
        }
        else
        {
            wheel_br.brakeTorque = wheel_bl.brakeTorque = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, steer, 0.7f * Time.deltaTime * 10);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, -1 * steer, 0.7f * Time.deltaTime * 10);
        }
        else
        {
            wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, 0, 0.9f * Time.deltaTime *10);
        }
    }
}
