using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] [Range(1, 6)]private int timeScale = 1;
    private int currentTimeScale = 1;

    private void Start()
    {
        currentTimeScale = timeScale;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            Time.timeScale = Time.timeScale == 0 ? currentTimeScale : 0;

        if(timeScale != currentTimeScale)
        {
            currentTimeScale = timeScale;
            Time.timeScale = currentTimeScale;
        }
    }


}
