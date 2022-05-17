using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    private GameObject player;
    public float aggroRange;
    public GameObject recon;
    public GameObject missile;
    public float spawnPeriod;
    public float firePeriod;
    public static int health = 5;

    private float spawnTimer = 0;
    private float fireTimer = 0;

    public AudioSource audioSrc;
    public AudioClip explode;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.pause.enabled && !Controller.howTo.enabled && !Controller.gameOver.enabled)
        {
            transform.LookAt(player.transform);
            spawnTimer += Time.deltaTime;
            //spawn recon ships going towards the player after a random increment of time
            if(spawnTimer > spawnPeriod)
            {
                Instantiate(recon, transform.position + transform.up * -50, transform.rotation);
                spawnTimer = 0f;
            }
            //if player is within __ units, shoot directly at them
            fireTimer += Time.deltaTime;
            if(Vector3.Distance(player.transform.position, transform.position) < aggroRange && fireTimer > firePeriod)
            {
                //fire projectile
                Instantiate(missile, transform.position + transform.forward * 40, transform.rotation);
                fireTimer = 0f;
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
            audioSrc.PlayOneShot(explode, 0.5F);
            Controller.ui.enabled = false;
            Controller.gameWon.enabled = true;
            Destroy(gameObject);
        }
    }
}
