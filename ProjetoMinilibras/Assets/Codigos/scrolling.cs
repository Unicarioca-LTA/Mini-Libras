using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrolling : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
       
            float mousePosX = Input.mousePosition.x;
            int scrollDistance = 130;
            float scrollSpeed = 140;
            if (mousePosX > scrollDistance)
            {
                transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
            }

            if (mousePosX <= Screen.width - scrollDistance)
            {
                transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
            }
        
    }
}
