using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bonusCount : MonoBehaviour
{
    private Player letter;
    private GameObject temp;
    private Text text;

    void Awake(){

    }

    void Start(){
        temp = GameObject.FindWithTag("Player");
        if (temp.CompareTag("Player")){
            Debug.Log("?????????");
        }
        letter = GameObject.FindWithTag("Player").GetComponent<Player>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textChange();
    }

    public void textChange(){
        text.text = letter.bonus.ToString();
    }
}
