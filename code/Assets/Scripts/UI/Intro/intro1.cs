using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro1 : MonoBehaviour
{
    private bool begin;
    private bool end;
    private SpriteRenderer text;
    private Color oldColor;
    private Color newColor;
    bool hasTrigger = false;

    [SerializeField]
    private GameObject jump;

    [SerializeField]
    private GameObject now;

    [SerializeField]
    public GameObject pressR;

    float fadeTime = 1.5f;
    void Start()
    {
        begin = false;
        end = false;
        text = gameObject.GetComponent<SpriteRenderer>();
        oldColor = new Color(text.color.r, text.color.g, text.color.b, 1);
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (begin && end){
            text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            return;
        }
        if (begin){
            text.color = Color.Lerp(text.color, oldColor, fadeTime * Time.deltaTime);
        }
        if (!jump && Input.GetKey(KeyCode.R)){
            end = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            now.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTrigger)
        {
            return;
        }
        
        if (other.CompareTag("Player"))
        {
            hasTrigger = true;
            begin = true;
            jump.SetActive(true);
        }
    }
}
