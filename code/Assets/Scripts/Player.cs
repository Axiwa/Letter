using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    private int health = 100;

    public int Health{
        get{
            return health;
        }
        set{
            health = value;
        }
    }

    int power = 50;

    public int Power{
        get{
            return power;
        }
        set{
            power = value;
        }
    }    
    string name = "Warrior";

    public string Name{
        get{
            return name;
        }
        set{
            name = value;
        }
    }
    public Player(){ }
    public Player(int health, int power, string name){
        Health = health;
        Power = power;
        Name = name;
    }

    public virtual void Attack(){
        Debug.Log(Name + " is attacking you!\n");
    }

    public void Info(){
        Debug.Log("Health is: " + Health);
        Debug.Log("Power is: " + power);
        Debug.Log("Name is: " + name);        
    }
}
