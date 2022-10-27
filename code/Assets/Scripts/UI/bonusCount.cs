using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bonusCount : MonoBehaviour
{
    [SerializeField]
    private GameObject letter;
    private GameObject temp;
    private Slider text;

    void Awake(){

    }

    void Start(){
        if (!letter)
            letter = GameObject.FindWithTag("Boss");
        text = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        textChange();
    }

    public void textChange(){
        if (text != null && letter != null)
            text.value = letter.GetComponent<Boss>().health;
        // Debug.Log(text.value);
    }
}
