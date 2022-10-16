using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters(){
        while (true){
            yield return new WaitForSeconds(Random.Range(5, 6));
            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = 1;

            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            // LEFT
            if (randomSide == 0){
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<Monster>().speed = Random.Range(4, 10);
                spawnedMonster.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            }
            else{
                spawnedMonster.transform.position = rightPos.position;
                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(3, 6);
                spawnedMonster.transform.localScale = new Vector3(-0.6f, 0.6f, 1f);
            }            
        } // while
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
