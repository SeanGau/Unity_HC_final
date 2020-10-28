using UnityEngine;
using System.Collections;
using UnityEditor;

public class GunControl : GunBase
{
    private IEnumerator oneshot()
    {
        var psN = Instantiate(Bullet, Point.position, Point.rotation).GetComponent<ParticleSystem>();
        Rigidbody psrb = psN.GetComponent<Rigidbody>();
        psrb.velocity = GetComponentInParent<Rigidbody>().velocity;
        while(psN.IsAlive())
            yield return null;
        
        Destroy(psN);
    }

    /// <summary>
    /// 射擊
    /// </summary>
    private void shot()
    {
        bool leftmouse = Input.GetKey(KeyCode.Mouse0);
        ani.SetBool("射擊", leftmouse);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Effects.SetActive(true);
        }
        else
        {
            Effects.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Mouse0) && bullet > 0f)
        {
            StartCoroutine(oneshot());
        }
    }

    protected override void Action()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !aud.isPlaying)
        {
            aud.PlayOneShot(soundShot, volume);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            aud.Stop();
        }
    }

    protected override void Update()
    {
        if (!isSet) return;
        base.Update();
    }

    private void FixedUpdate()
    {
        if (!isSet) return;
        shot();
    }
}
