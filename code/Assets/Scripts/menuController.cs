using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public void PlayGame(){
        string clickedObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(clickedObj);

        GameManager.instance.CharIndex = GameManager.instance.table[clickedObj];

        SceneManager.LoadScene("GamePlay");
    }
}
