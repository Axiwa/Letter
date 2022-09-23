using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    // Start is called before the first frame update
    public Warrior(int health, int power, string name){
        Health = health;
        Power = power;
        Name = name;
    }
    public override void Attack(){
        Debug.Log(Name + " is throwing AXE!\n");
    }
}
