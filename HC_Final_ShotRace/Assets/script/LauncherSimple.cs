using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LauncherSimple : MonoBehaviour
{
    public GameObject firstCanvas;
    public GameObject controlPanel;
    public GameObject progressLabel;
    void Awake()
    {
        firstCanvas.SetActive(true);
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);        
    }
    
    void Update()
    {
        if (Input.anyKey && firstCanvas.activeSelf)
        {
            firstCanvas.SetActive(false);
        }
    }
    
    public void Loadgame()
    {
        controlPanel.SetActive(false);
        progressLabel.SetActive(true);
        SceneManager.LoadScene("遊戲場景");
    }
}
