using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameObject letterObj;
    Player letter;

    [SerializeField]
    float initialHealth = 20f;

    [SerializeField]
    private GameObject music;

    [SerializeField]
    private GameObject healthObj;

    public float health;

    private Animator anim;

    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform pos;


    private int randomIndex;

    private void Awake(){
        anim = GetComponent<Animator>();
        health = initialHealth;
    }

    // Start is called before the first frame update
    void Start()
    {   
        letterObj = GameObject.FindWithTag("Player");
        letter = letterObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            Destroy(gameObject);
            return;
        }

        // 

    }

    public void AnimatePlayer(){
        if (health < 0.3 * initialHealth){
            // state 1
        }
        else if (health < 0.6 * initialHealth){
            // state 2
        }
        else{
            // state 3
        }  
    }

    IEnumerator SpawnMonsters(){
        while (true){
            yield return new WaitForSeconds(Random.Range(4,  6));
            if (abs(letter.transform.position.x - transform.position.x) > 40f)
            {
                continue;
            }
            randomIndex = Random.Range(0, monsterReference.Length);

            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            spawnedMonster.transform.position = pos.position;
            spawnedMonster.GetComponent<followMonster>().speed = -Random.Range(1, 5);
            spawnedMonster.GetComponent<followMonster>().distance = 10f;
            var scale = Random.Range(0.4f, 1f);
            spawnedMonster.transform.localScale = new Vector3(scale, scale, 1f);        
        } 
    }

    public void appear(){
        anim.SetTrigger("appear");
        // Activate health
        StartCoroutine(SpawnMonsters());
    }

    public void playMusic(){
        healthObj.SetActive(true);
        music.SetActive(true);
    }

    private void OnDestroy() {
        music.SetActive(false);
        healthObj.SetActive(false);
    }

    float abs(float a)
    {
        if (a > 0)
        {
            return a;
        }
        else
        {
            return -a;
        }
    }
}
