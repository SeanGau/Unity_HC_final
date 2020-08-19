using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject firstCanvas;
    // Start is called before the first frame update
    void Start()
    {
        firstCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && firstCanvas.activeSelf)
        {
            firstCanvas.SetActive(false);
        }    
    }
}
