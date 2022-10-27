using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro3 : MonoBehaviour
{
    private GameObject girl;
    private Color newColor;
    float fadeTime = 0.5f;
    private SpriteRenderer text;

    // Start is called before the first frame update
    void Start()
    {
        girl = GameObject.FindWithTag("Girl");
        text = GetComponent<SpriteRenderer>();
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (text.color.a < 0.1f)
        {
            Destroy(gameObject);
        }

        transform.position = girl.transform.position + new Vector3(0.5f, 1, 0);
        text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
    }
}
