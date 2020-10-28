using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GunBase : MonoBehaviour
{
    [Header("靈敏度"), Range(0, 1000)]
    public float mouseSensitivity = 100;
    [Header("子彈數量"), Range(0, 500)]
    public int bullet = 20;
    [Header("音效")]
    public AudioClip soundShot;
    [Header("音量")]
    public float volume = 1;
    [Header("開槍特效")]
    public GameObject Effects;
    [Header("子彈")]
    public GameObject Bullet;

    public Text bulletCount;

    public Transform Gun;
    public Transform Point;
    public Animator ani;
    protected bool isSet;
    protected AudioSource aud;

    public void Set()
    {
        isSet = true;
        print("Set");
    }

    private void Mouse()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3((mousePos.x - 0.5f)*1.7f, 0, mousePos.y - 0.5f);
        transform.forward = targetPos;
    }
    protected virtual void Awake()
    {
        isSet = true;
        aud = GetComponent<AudioSource>();
    }

    protected virtual void Action()
    {
        
    }

    protected virtual void Update()
    {
        Action();
        Mouse();
    }
}
