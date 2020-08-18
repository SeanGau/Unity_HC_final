using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarShowCamera : MonoBehaviour
{
    public static int nowCar = 0;
    public Transform carSet;
    public Text carName;
    int carAmount;

    void Start()
    {
       carAmount = carSet.childCount;
       print(carAmount);
    }

    public void ChangeCar(bool goRight = true)
    {
        int dir = goRight ? 1:-1;
        nowCar=(nowCar+dir)%carAmount;
        if(nowCar < 0)
          nowCar = carAmount-1;
        carName.text = "Car" + (nowCar+1);
    }


    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 pos = new Vector3(-20*(nowCar+1), 3.5f, 17);
        transform.position = Vector3.Lerp(transform.position, pos, 0.5f * Time.deltaTime * 10);
    }
}
