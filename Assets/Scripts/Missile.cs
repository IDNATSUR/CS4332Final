using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float lifespan;
    public float speed;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.pause.enabled && !Controller.howTo.enabled && !Controller.gameOver.enabled)
        {         
            timer += Time.deltaTime;
            transform.position += speed * transform.forward * Time.deltaTime;
            if(timer > lifespan)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Missile" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
        }
    }
}
