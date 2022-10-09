using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro3 : MonoBehaviour
{
    private GameObject letter;
    [SerializeField]
    private GameObject line;
    private Text text;
    private bool begin;
    private bool end;
    private Color oldColor;
    private Color newColor;
    float fadeTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        letter = GameObject.FindWithTag("Player");
        text = GetComponent<Text>();
        begin = false;
        end = false;
        oldColor = new Color(0.5566f, 0.5566f, 0.5566f, 1);
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (end){
            text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            return;
        }

        if (line.GetComponent<lineInfo>().hasCollided){
            end = true;
        }
    }
}
