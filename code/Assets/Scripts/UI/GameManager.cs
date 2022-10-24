using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<string, int> table=new Dictionary<string, int>(){
        {"Button", 0},
    };
    [SerializeField]
    private GameObject[] characters;

    private void Awake() {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if (scene.name == "4"){
            var littlegirl = Instantiate(characters[1]);
            var player = Instantiate(characters[0]);
            littlegirl.GetComponent<girl>().letter = player;
            player.GetComponent<Player>().girl = littlegirl;
            player.transform.position = new Vector3(-6f, -3f, 0);
            littlegirl.transform.position = new Vector3(-7f, -3f, 0);
        }
    }
}
