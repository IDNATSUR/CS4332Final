using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject small;
    public GameObject mother;
    public GameObject asteroid;
    public GameObject satellite;
    public float mapRange;
    public float smallSpawnRange;
    public int obstacleNum;
    public float spawnPeriod;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //spawn one mothership in random area
        Instantiate(mother, new Vector3(Random.Range(-1 * mapRange, mapRange), Random.Range(-1 * mapRange, mapRange) * .5f, Random.Range(-1 * mapRange, mapRange)), Quaternion.identity);
        //spawn multiple asteroids and satellites
        for(int i = 0; i < obstacleNum; i++)
        {
            Instantiate(asteroid, new Vector3(Random.Range(-1 * mapRange, mapRange), Random.Range(-1 * mapRange, mapRange) * .5f, Random.Range(-1 * mapRange, mapRange)), Random.rotation);
            Instantiate(satellite, new Vector3(Random.Range(-1 * mapRange, mapRange), Random.Range(-1 * mapRange, mapRange) * .5f, Random.Range(-1 * mapRange, mapRange)), Random.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.pause.enabled && !Controller.howTo.enabled && !Controller.gameOver.enabled)
        {
            timer += Time.deltaTime;
            //spawn small ships after a random increment of time facing a random direction in a random location close to the player
            if(timer > spawnPeriod)
            {
                Instantiate(small, new Vector3(Random.Range(player.transform.position.x - smallSpawnRange, player.transform.position.x + smallSpawnRange), 
                    Random.Range(player.transform.position.y - smallSpawnRange, player.transform.position.y + smallSpawnRange) * .1f,
                    Random.Range(player.transform.position.z - smallSpawnRange, player.transform.position.z + smallSpawnRange)), 
                    Random.rotation);
                timer = 0f;
            }
        }
        
    }
}
