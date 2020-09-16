using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMapper : MonoBehaviour
{
    public LineRenderer lr;
    public Transform target;
    public Transform player;

    public GameObject smallMap;
    public GameObject bigMap;
    private bool toggleMap = false;
    
    private void Awake()
    {
        
    }

    private void LateUpdate()
    {
        lr.SetPositions(new Vector3[]{ target.position, player.position});
        
        //Vector3 mousepos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //float angle = Mathf.Atan((mousepos.y-0.5f) / (mousepos.x-0.5f))* Mathf.Rad2Deg;
        //print("mouse: " + mousepos + " angle: "+ angle);

        if (Input.GetKeyDown(KeyCode.M))
        {
            smallMap.SetActive(toggleMap);
            toggleMap = !toggleMap;
            float lwidth = toggleMap ? 5f:1.5f;
            lr.startWidth = lr.endWidth = lwidth;
            bigMap.SetActive(toggleMap);
        }
    }
}
