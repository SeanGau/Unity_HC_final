using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayCamControl : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 40, 0);
    // Start is called before the first frame update
    void Awake()
    {
        target = GameManager.playerCar.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
      transform.position = target.position+offset;
      //transform.position = Vector3.Lerp(transform.position, pos+offset, 0.7f * Time.deltaTime * 10);

    }
}
