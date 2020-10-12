using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GunBase : MonoBehaviour
{
    [Header("靈敏度"), Range(0, 1000)]
    public float mouseSensitivity = 100;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 20;
    [Header("子彈數量"), Range(0, 500)]
    public float bullet = 200;
    [Header("音效")]
    public AudioClip soundShot;
    [Header("音量")]
    public float volume = 1;
    [Header("開槍特效")]
    public GameObject Effects;
    [Header("子彈")]
    public GameObject Bullet;

    public Transform Gun;
    public Transform Point;
    public Animator ani;
    protected AudioSource aud;

    protected virtual void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    protected virtual void Action()
    {
        
    }

    protected virtual void Update()
    {
        Action();
    }
}
