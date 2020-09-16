using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsControl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "buildings")
            Destroy(collision.gameObject);
    }
}
