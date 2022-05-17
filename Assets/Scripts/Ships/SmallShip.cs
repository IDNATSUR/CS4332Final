using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShip : MonoBehaviour
{
    public float aggroRange;
    public float speed;
    public GameObject missile;
    public float firePeriod;
    public int health = 1;

    private GameObject player;
    private float fireTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.pause.enabled && !Controller.howTo.enabled && !Controller.gameOver.enabled)
        {
            fireTimer += Time.deltaTime;
            //move forward until player is within __ units, then turn towards them and shoot
            transform.position += transform.forward * speed * Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < aggroRange)
            {
                transform.LookAt(player.transform);

                if (fireTimer > firePeriod)
                {
                    //fire projectile
                    Instantiate(missile, transform.position + transform.forward * 24, transform.rotation);
                    fireTimer = 0;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Missile")
        {
            Hurt();
        }
    }

    void Hurt()
    {
        health--;
        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
