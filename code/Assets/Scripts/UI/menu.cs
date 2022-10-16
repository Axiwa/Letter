using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    float timeLeft;
    float cursorPosition;
    bool catchCursor = true;
    float visibleCursorTimer = 3f;
    Color tmp;

    void Start(){
        tmp = gameObject.GetComponent<Image>().color;
    }

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
                tmp.a = 0f;
                gameObject.GetComponent<Image>().color = tmp;
                catchCursor=true;
            }
        }
        else{
            timeLeft = visibleCursorTimer;
            tmp.a = 1f;
            gameObject.GetComponent<Image>().color = tmp;
        }            
    }
}
