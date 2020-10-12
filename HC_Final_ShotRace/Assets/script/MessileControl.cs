using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

public class MessileControl : GunBase
{
    /// <summary>
    /// 射擊
    /// </summary>
    protected override void Action()
    {
        bool leftmouse = Input.GetKey(KeyCode.Mouse0);
        ani.SetBool("射擊", leftmouse);
        if (Input.GetKey(KeyCode.Mouse0) && !aud.isPlaying)
        {
            aud.PlayOneShot(soundShot, volume);
            Effects.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            aud.Stop();
            Effects.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Mouse0))
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

    private void Mouse()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(mousePos.x - 0.5f, 90, mousePos.y - 0.5f);
        Gun.forward = targetPos;
    }

    [System.Obsolete]
    private void Update()
    {
        Mouse();
    }
    private void Start()
    {
        
    }
}
