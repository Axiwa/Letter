using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class stair0_2 : Stair
{
    bool begin = false;
    bool toright = true;

    [SerializeField]
    private float minX = 0;
    [SerializeField]
    private float maxX = 15;
    [SerializeField]
    float speed = 4.5f;

    Transform parent;



    void Awake()
    {
        lineInfo.moveInfo += Move;
        minX = transform.position.x;
        toright = true;
        begin = true;
        if (minX > maxX)
        {
            (minX, maxX) = (maxX, minX);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!begin)
            return;

        if (toright)
        {
            transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
            if (transform.position.x >= maxX - 0.1)
            {
                toright = false;
            }
        }
        else
        {
            transform.position += new Vector3(-speed, 0f, 0f) * Time.deltaTime;
            if (transform.position.x <= minX + 0.1)
            {
                toright = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player") && other.transform.position.y - transform.position.y > 0.4f) || (other.gameObject.CompareTag("Girl") && other.transform.position.y - transform.position.y > 0.2f))
        {
            parent = null;
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Girl"))
        {
            other.gameObject.transform.parent = parent;
        }
    }

    public override void Move()
    {
        begin = true;
    }


}
