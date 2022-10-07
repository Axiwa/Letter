using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bonusCount : MonoBehaviour
{
    private GameObject letter;
    private GameObject temp;
    private Text text;

    void Awake(){

    }

    void Start(){
        letter = GameObject.FindWithTag("Player");
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textChange();
    }

    public void textChange(){
        if (text != null && letter != null)
            text.text = letter.GetComponent<Player>().bonus.ToString();
    }
}
