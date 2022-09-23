using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next : MonoBehaviour
{
    float speed = 5.0f;
    double mana = 15.5;

    int health = 100;

    int power = 20;

    string name = "Axiwa";

    bool isDead = false;

    char oneChar = 'a';

    Player warrior;
    Player axiwa;


    private void Awake(){
        warrior = new Warrior(health, power, "Warrior");
        axiwa = new Player(health, power, name);
    }

    private void onEnable(){

    }
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("ExecuteSomething");
        Transform myTransform = GetComponent<Transform>();
        myTransform.position = new Vector3(10, 20, 30);
    }

    IEnumerator ExecuteSomething(){
        yield return new WaitForSeconds(5f);
        axiwa.Info();
        axiwa.Attack();
        axiwa.Health = 120;
        axiwa.Info();
        warrior.Info();
        warrior.Attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
