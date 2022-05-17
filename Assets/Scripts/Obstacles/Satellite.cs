﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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