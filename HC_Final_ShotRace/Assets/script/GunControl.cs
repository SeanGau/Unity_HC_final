using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    [Header("靈敏度"), Range(0, 1000)]
    public float mouseSensitivity = 100;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 20;
    [Header("子彈數量"), Range(0, 500)]
    public float bullet = 200;
    [Header("音效")]
    public AudioClip soundShot;
    [Header("開槍特效")]
    public GameObject MuzzleFlash;
    [Header("射程")]

    //float yRotation = 0f;

    public Transform Gun;
    public Animator ani;
    public AudioSource aud;

    /// <summary>
    /// 射擊
    /// </summary>
    private void shot()
    {
        bool leftmouse = Input.GetKey(KeyCode.Mouse0);
        ani.SetBool("射擊", leftmouse);
        if (Input.GetKey(KeyCode.Mouse0) && !aud.isPlaying)
        {
            aud.PlayOneShot(soundShot, 0.8f);
            MuzzleFlash.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            aud.Stop();
            MuzzleFlash.SetActive(false);
        }
    }

    private void Mouse()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(mousePos.x - 0.5f,0 ,mousePos.y - 0.5f);
        Gun.forward = targetPos;
    }


    private void Update()
    {
        shot();
        Mouse();
    }
    private void Start()
    {
        
    }
}
