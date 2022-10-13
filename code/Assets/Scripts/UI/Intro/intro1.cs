using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class intro1 : MonoBehaviour
{
    private bool begin;
    private bool end;
    private Text text;
    private Color oldColor;
    private Color newColor;

    [SerializeField]
    private GameObject monster;
    private GameObject player;

    float fadeTime = 1.5f;
    void Start()
    {
        begin = false;
        end = false;
        text = gameObject.GetComponent<Text>();
        oldColor = new Color(0.5566f, 0.5566f, 0.5566f, 1);
        newColor = new Color(text.color.r, text.color.g, text.color.b, 0);

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player){
            return;
        }
        if (begin && end){
            text.color = Color.Lerp(text.color, newColor, fadeTime * Time.deltaTime);
            return;
        }
        if (begin){
            text.color = Color.Lerp(text.color, oldColor, fadeTime * Time.deltaTime);
        }
        if (monster && Vector3.Distance(monster.transform.position, player.transform.position) < 4){
            begin = true;
        }
        if (!monster && Input.GetKey(KeyCode.R)){
            end = true;
        }
    }
}
