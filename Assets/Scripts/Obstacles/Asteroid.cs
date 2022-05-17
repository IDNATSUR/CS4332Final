using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float maxRotation;
    public GameObject powerUp;

    private Vector3 rotate;

    // Start is called before the first frame update
    void Start()
    {
        //create random vector for asteroid to spin on
        rotate = new Vector3(Random.Range(-1 * maxRotation, maxRotation), Random.Range(-1 * maxRotation, maxRotation), Random.Range(-1 * maxRotation, maxRotation));
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.pause.enabled && !Controller.howTo.enabled && !Controller.gameOver.enabled)
        {
            //spin
            transform.Rotate(rotate, Space.Self);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Missile")
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
