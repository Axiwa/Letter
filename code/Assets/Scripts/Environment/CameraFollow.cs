using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Player player;
    private Transform ptr;

    public float Aheadx = 3; //当角色向右移动时，摄像机比任务位置领先
    public float Aheady = 2.5f; //当角色向上移动时，摄像机比任务位置领先
    private float aheady = 3;

    private Vector3 Targetpos; //摄像机的最终目标
    public float smooth; //摄像机平滑移动的值

    private float minY = -8;

    [SerializeField]
    private float height = 5;

    private float targetX;
    private float targetY;

    [SerializeField]
    private bool transition = false;
    [HideInInspector]
    public bool resume = false;


    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        ptr = GameObject.FindWithTag("Player").transform;
        transform.position = new Vector3(0, 0, -10);
        transition = false;
        resume = false;
    }

    void Update () {
        if (!player){
            return;
        }

        if (transform.position.x >= 413.9 && player.linkedState == 0){
            return;
        }

        if (player.movementX > 0){
            targetX = player.transform.position.x + Aheadx;
        }
        else if (player.movementX < 0){
            targetX = player.transform.position.x + Aheadx;
        }
        else{
            targetX = player.transform.position.x + Aheadx;
        }

        if (Targetpos.y < minY){
            Targetpos.y = minY;
            return;
            // TODO: YOU LOST!!!
        }

        if (resume == true){
            Targetpos = new Vector3(targetX, player.transform.position.y + Aheady, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Targetpos, smooth * Time.unscaledDeltaTime);

            if (Vector3.Distance(Targetpos, transform.position) < 0.01){
                resume = false;
            }

            return;            
        }

        if (Input.GetKeyDown(KeyCode.X)){
            resume = true;
            Targetpos = new Vector3(targetX, player.transform.position.y + Aheady, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Targetpos, smooth * Time.unscaledDeltaTime);
            return;
        }

        targetY = transform.position.y;

        Targetpos = new Vector3(targetX, targetY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, Targetpos, smooth * Time.unscaledDeltaTime);
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