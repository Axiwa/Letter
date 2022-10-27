using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro : MonoBehaviour
{
    [SerializeField]
    private bool begin;
    [SerializeField]
    private bool end;
    private SpriteRenderer text;
    private Color oldColor;
    private Color newColor;
    [SerializeField]
    bool hasTrigger = false;

    [SerializeField]
    private GameObject refer;

    [SerializeField]
    float fadeTime = 1.5f;

    void Start()
    {
        begin = false;
        end = false;
        hasTrigger = false;
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
        if (!refer || (refer.GetComponent<bonus>() && refer.GetComponent<bonus>().hasTrigger))
        {
            end = true;
            return;
        }
        if (begin && !end)
        {
            text.color = Color.Lerp(text.color, oldColor, fadeTime * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTrigger)
        {
            return;
        }
        
        if (other.CompareTag("Player") || other.CompareTag("Girl"))
        {
            hasTrigger = true;
            begin = true;
        }
    }
}