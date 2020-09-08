using UnityEngine;

public class GunControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform Gun;

    //float xRotation = 0f;

    public Animator ani;

    /// <summary>
    /// 射擊
    /// </summary>
    private void shot()
    {
        bool leftmouse = Input.GetKey(KeyCode.Mouse0);
        ani.SetBool("射擊", leftmouse);
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //xRotation += mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);

        Gun.Rotate(Vector3.right * mouseX);
        //Gun.Rotate(Vector3.back * mouseY);

        shot();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
