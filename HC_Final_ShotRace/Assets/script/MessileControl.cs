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

        if (Input.GetKeyDown(KeyCode.Mouse0) && bullet > 0)
        {
            var Missile = Instantiate(Bullet, Point.position, Point.rotation);
            Missile.SetActive(true);
            Rigidbody msrb = Missile.GetComponent<Rigidbody>();
            msrb.velocity = GetComponentInParent<Rigidbody>().velocity;
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
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(mousePos.x - 0.5f, 0, mousePos.y - 0.5f);
        transform.forward = targetPos;
    }

    protected override void Update()
    {
        if (!isSet) return;
        shot();
        Mouse();
    }
    private void Start()
    {
        
    }
}
