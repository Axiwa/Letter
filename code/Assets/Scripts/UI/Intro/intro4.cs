using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI.Table;

public class intro4 : MonoBehaviour
{
    private bool begin;
    private bool end;

    [SerializeField]
    private GameObject line;

    private SpriteRenderer text;
    private Color oldColor;
    private Color newColor;
    float fadeTime = 1.5f;

    void Start()
    {
        begin = true;
        end = false;
        text = gameObject.GetComponent<SpriteRenderer>();
        oldColor = new Color(text.color.r, text.color.g, text.color.b, 1);
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin && end)
        {
            text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            return;
        }
        if (begin)
        {
            text.color = Color.Lerp(text.color, oldColor, fadeTime * Time.deltaTime);
        }
        if (line.GetComponent<lineInfo>().hasCollided)
        {
            end = true;
        }
    }
}
