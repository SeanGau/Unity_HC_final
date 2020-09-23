using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShowCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 1f, 1.5f);
    public static int nowGun = 0;
    public Transform gunSet;
    public Text gunName;
    int gunAmount;

    void Start()
    {
       gunAmount = gunSet.childCount;
       print(gunAmount);
    }

    public void ChangeCar(bool goRight = true)
    {
        int dir = goRight ? 1:-1;
        nowGun=(nowGun+dir)%gunAmount;
        if(nowGun < 0)
          nowGun = gunAmount-1;
        gunName.text = "Gun" + (nowGun+1);
    }


    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 pos = gunSet.GetChild(nowGun).position + offset;
        transform.position = Vector3.Lerp(transform.position, pos, 0.5f * Time.deltaTime * 10);
    }
}
