using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private GameObject letter;

    private int randomIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (!letter){
            letter = GameObject.FindWithTag("Player");
        }
        StartCoroutine(SpawnMonsters());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnMonsters(){
        while (true){
            yield return new WaitForSeconds(Random.Range(1, 5));
            letter = GameObject.FindWithTag("Player");
            Debug.Log(abs(letter.transform.position.x - transform.position.x));
            if (spawnedMonster == null && Vector3.Distance(letter.transform.position, transform.position) < 5f & abs(letter.transform.position.y - transform.position.y) < 1f){
                randomIndex = Random.Range(0, monsterReference.Length);

                spawnedMonster = Instantiate(monsterReference[0]);

                spawnedMonster.transform.position = transform.position;
                spawnedMonster.GetComponent<followMonster>().speed = Random.Range(1, 2);
            }
        }
    }    

    float abs(float a){
        if (a > 0){
            return a;
        }
        else{
            return -a;
        }
    }
}
