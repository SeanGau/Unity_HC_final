using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

public class MessileControl : GunBase
{
    /// <summary>
    /// 射擊
    /// </summary>
    private void shot()
    {
        bool leftmouse = Input.GetKey(KeyCode.Mouse0);
        ani.SetBool("發射", leftmouse);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Effects.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Effects.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Mouse0) && bullet > 0f)
        {
            //StartCoroutine(oneshot());
            //ps.loop = true;
            //ps.transform.SetParent(Point);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //ps.loop = false;
            //ps.transform.SetParent(null);
        }
    }

    protected override void Action()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !aud.isPlaying)
        {
            aud.PlayOneShot(soundShot, volume);
        }
    }
    private void Mouse()
    {
        //Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Vector3 targetPos = new Vector3(mousePos.x - 0.5f, 90, mousePos.y - 0.5f);
        //Gun.forward = targetPos;

        Vector3 posMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 posWorld = Camera.main.ScreenToWorldPoint(posMouse);
        //Vector3 posMouse = new Vector3(posWorld.x - 0.5f, 0, posWorld.y - 0.5f);

        posWorld.y = transform.position.y;

        Vector3 direction = posWorld - transform.position;

        transform.forward = direction;
    }

    protected override void Update()
    {
        base.Update();
        shot();
        Mouse();
    }
    private void Start()
    {
        
    }
}
