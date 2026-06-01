using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingtimer : MonoBehaviour
{
    public float timeRemaining;
    public GameObject panelLaoding;

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else{
            panelLaoding.SetActive(false);
        }
    }
}
