using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro4 : MonoBehaviour
{
    private bool begin;
    private bool end;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Text pressC;

    private GameObject girl;

    private Color oldColor;
    private Color newColor;
    float fadeTime = 1.5f;

    void Start()
    {
        text.color = new Color(0.5566f, 0.5566f, 0.5566f, 0);
        pressC.color = new Color(0.5566f, 0.5566f, 0.5566f, 0);
        begin = false;
        end = false;
        oldColor = new Color(0.5566f, 0.5566f, 0.5566f, 0.8f);
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0); 
        girl = GameObject.FindWithTag("Girl"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (end && text.color.a < 0.001f){
            end = false;
        }
        if (end){
            text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            pressC.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            return;
        }
        else if (begin){
            text.color = Color.Lerp(text.color, oldColor, fadeTime * Time.deltaTime);
            pressC.color = Color.Lerp(text.color, oldColor, fadeTime * Time.deltaTime);
        }
        if (girl!=null && girl.GetComponent<girl>().inside){
            end = true;
            begin = false;
        }        
    }


    void OnCollisionEnter2D(Collision2D other) {
        if (begin || end){
            return;
        }
        if (other.gameObject.CompareTag("Girl")){
            girl = other.gameObject;
            begin = true;
        }          
    }

    void OnCollisionExit2D(Collision2D other) {
        end = true;       
    }
}
