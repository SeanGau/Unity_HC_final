using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarShowCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 3.5f, 17);
    public static int nowCar = 0;
    public Transform carSet;
    public Text carName;
    int carAmount;

    void Start()
    {
        carAmount = carSet.childCount;
        print(carAmount);
        carName.text = carSet.GetChild(nowCar).name;
    }

    public void ChangeCar(bool goRight = true)
    {
        int dir = goRight ? 1:-1;
        nowCar=(nowCar+dir)%carAmount;
        if(nowCar < 0)
          nowCar = carAmount-1;
        carName.text = carSet.GetChild(nowCar).name;
    }


    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 pos = carSet.GetChild(nowCar).position + offset;
        transform.position = Vector3.Lerp(transform.position, pos, 0.5f * Time.deltaTime * 10);
    }
}
