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
    public float steer = 20;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      rb.AddForce(0, -1*accel, 0);
      wheel_fr.motorTorque = wheel_fl.motorTorque = wheel_br.motorTorque = wheel_bl.motorTorque = accel;

      if(Input.GetKey(KeyCode.RightArrow))
      {
        wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, steer, 0.9f*Time.deltaTime);
      }
      else if(Input.GetKey(KeyCode.LeftArrow))
      {
        wheel_fr.steerAngle = wheel_fl.steerAngle = Mathf.Lerp(wheel_fr.steerAngle, -1*steer, 0.9f*Time.deltaTime);
      }
      else
      {
        wheel_fr.steerAngle = wheel_fl.steerAngle = 0;
      }
    }
}
