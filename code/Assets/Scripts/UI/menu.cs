using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    float timeLeft;
    float cursorPosition;
    bool catchCursor = true;
    float visibleCursorTimer = 3f;

    void LateUpdate()
    {
        if(catchCursor){
            catchCursor = false;
            cursorPosition = Input.GetAxis("Mouse X");
        }
        if(Input.GetAxis("Mouse X") == cursorPosition){
            timeLeft -= Time.deltaTime;
            if ( timeLeft < 0 ){                
                timeLeft = visibleCursorTimer;
                gameObject.GetComponent<CanvasGroup>().alpha = 0;
                catchCursor=true;
            }
        }
        else{
            timeLeft = visibleCursorTimer;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }            
    }
}
