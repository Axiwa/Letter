using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro0 : MonoBehaviour
{
    private bool begin;
    private SpriteRenderer text;
    private Color newColor;
    float fadeTime = 2f;
    void Start()
    {
        begin = false;
        text = gameObject.GetComponent<SpriteRenderer>();
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (begin){
            text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)){
            begin = true;
        }
    }
}
