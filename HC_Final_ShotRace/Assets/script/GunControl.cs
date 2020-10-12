using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class GunControl : GunBase
{
    private IEnumerator oneshot()
    {
        var psN = Instantiate(Bullet, Point.position, Point.rotation).GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(1);
        Destroy(psN);
    }

    /// <summary>
    /// 射擊
    /// </summary>
    protected override IEnumerator Action()
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
            StartCoroutine(oneshot());
            //ps.loop = true;
            //ps.transform.SetParent(Point);

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            //ps.loop = false;
            //ps.transform.SetParent(null);
        }

        return base.Action();
    }

    private void Mouse()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(mousePos.x - 0.5f,0 ,mousePos.y - 0.5f);
        Gun.forward = targetPos;
    }

    private void Update()
    {
        Action();
        Mouse();
    }
    private void Start()
    {
        
    }
}
