using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class darkComplaint : MonoBehaviour
{
    private GameObject girl;
    private Text text;
    private Color oldColor;
    private Color newColor;
    float fadeTime = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        girl = GameObject.FindWithTag("Girl");
        text = gameObject.GetComponent<Text>();

        transform.position = girl.transform.position + new Vector3(0, 3, 0);
        
        text.color = new Color(1, 1, 1, 0f);
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!girl){
            return;
        }
        transform.position = girl.transform.position + new Vector3(0.5f, 1, 0);
        text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
    }
}
