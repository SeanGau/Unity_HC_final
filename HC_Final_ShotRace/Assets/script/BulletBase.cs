using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [Header("攻擊力"), Range(0, 100)]
    public float attack = 2.5f;
    [Header("速度"), Range(0, 500)]
    public float speed = 400;
    [Header("類別"), Range(1,3)]
    public int cat = 1;
    [Header("射程"), Range(10, 200)]
    public float distance = 50;

    public Rigidbody rig;
    private Vector3 posA;


    private void Distance()
    {
        float dis = Vector3.Distance(posA,transform.position);
        if (dis >= distance)
        {

        }
    }

    private void Update()
    {
        Distance();
    }

    private void Awake()
    {
        if (cat == 1)
        {
            rig = GetComponent<Rigidbody>();
            rig.velocity = GetComponentInParent<Rigidbody>().velocity;
            rig.AddForce(transform.up * speed);
            posA = transform.position;
        }
        
    }
}
