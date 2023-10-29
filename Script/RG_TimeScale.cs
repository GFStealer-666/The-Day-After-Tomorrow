using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_TimeScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    private void OnEnable(){
        RG_CamControl.OnGameStarted += OngamePause;
        RG_GuideControl.OnPanelClosing += OnGameResume;
        RG_GuideControl.OnPanelOpening += OngamePause;
    }

    private void OnDisable(){
        RG_CamControl.OnGameStarted -= OngamePause;     
        RG_GuideControl.OnPanelClosing -= OnGameResume;
        RG_GuideControl.OnPanelOpening += OngamePause;  
    }




    private void OngamePause(){
        Time.timeScale = 0;
    }

    private void OnGameResume(){
        Time.timeScale = 1;
    }
}
