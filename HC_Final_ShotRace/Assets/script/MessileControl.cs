using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

public class MessileControl : GunBase
{
    /// <summary>
    /// 射擊
    /// </summary>
    public GameObject minigun;
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
            bullet--;
        }
        bulletCount.text = bullet.ToString();
        if (bullet <= 0)
        {
            GetComponentInParent<CarControl>().ChangeWeapon(2);
        }
    }

    protected override void Action()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !aud.isPlaying)
        {
            aud.PlayOneShot(soundShot, volume);
        }
    }
    protected override void Update()
    {
        if (!isSet) return;
        base.Update();
        shot();
    }
    private void Start()
    {

    }
}
