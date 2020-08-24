using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayCamControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 30, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
      transform.position = target.position+offset;
      //transform.position = Vector3.Lerp(transform.position, pos+offset, 0.7f * Time.deltaTime * 10);

    }
}
