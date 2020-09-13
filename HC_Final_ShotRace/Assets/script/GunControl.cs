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

    //float yRotation = 0f;

    public Transform Gun;
    public Animator ani;
    public Canvas can;
    public AudioSource aud;

    /// <summary>
    /// 射擊
    /// </summary>
    private void shot()
    {
        bool leftmouse = Input.GetKey(KeyCode.Mouse0);
        ani.SetBool("射擊", leftmouse);
        if (Input.GetKeyDown(KeyCode.Mouse0)) aud.PlayOneShot(soundShot, 0.8f);
    }

    private void Mouse()
    {
        //Vector2 v2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Vector2 newPoint = new Vector2(v2.x - 0.5f, v2.y - 0.5f);
        //Vector2 temp = new Vector2(newPoint.x * this.width, newPoint.y * this.height);
    }
       

    private void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yRotation -= mouseY;
        //yRotation = Mathf.Clamp(yRotation, -7f, 0f);

        //xRotation += mouseX;

        //Gun.localRotation = Quaternion.Euler(0f, 0f, yRotation);

        //transform.Rotate(Vector3.right * mouseX);
        //Gun.Rotate(Vector3.back * mouseY);

        shot();
        Mouse();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
