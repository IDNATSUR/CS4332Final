using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private CharacterController controller;

    //use bool pause.enabled to prevent input with other scripts
    //disable movement and input updates while pause is active
    public static Canvas pause;
    private Canvas howTo;
    private Canvas ui;
    private Canvas gameOver;

    public float cameraScaling = .5f;

    //player base stats
    //public static int score = 0;
    public float playerSpeed = 5f;
    public float rotationSpeed = .5f;
    public GameObject missile;
    public float fireTimer = 1f;
    public static int health = 1;
    private int boost;

    //keep track for powerups
    public static int speedUp = 0;
    public static int fireRateUp = 0;
    public float speedMult = .1f;
    public float fireRateMult = .9f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();

        pause = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        howTo = GameObject.Find("HowToCanvas").GetComponent<Canvas>();
        ui = GameObject.Find("UICanvas").GetComponent<Canvas>();
        gameOver = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        pause.enabled = false;
        howTo.enabled = false;
        ui.enabled = true;
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pause logic
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pause.enabled)
            {
                //stop the game and show pause menu
                ui.enabled = false;
                howTo.enabled = false;
                gameOver.enabled = false;
                pause.enabled = true;
            }
            else
            {
                //resume the game and take down the pause menu
                pause.enabled = false;
                ui.enabled = true;
            }
        }

        //player movement
        if (!pause.enabled)
        {
            //movement
            if (Input.GetButton("Fire3"))
            {
                boost = 2;
            }
            else
            {
                boost = 1;
            }
            //speedUp powerups boost player speed by 10% additively for each one you collect
            transform.Rotate(0f, Input.GetAxis("Horizontal") * rotationSpeed, 0f, Space.World);
            Vector3 move = new Vector3(0f, Input.GetAxis("Vertical") * -.5f, 0f);
            controller.Move(Time.deltaTime * (transform.forward + move) * (playerSpeed + playerSpeed * speedMult * speedUp) * boost);
            //add cosmetic rotations to make flight feel more natural if there is time

            //shooting
            //fire rate powerups boost fire rate by 10% multiplicatively for each one you collect
            timer += Time.deltaTime;
            if (Input.GetButtonDown("Jump") && (timer > (fireTimer * Mathf.Pow(fireRateMult, fireRateUp))))
            {
                //spawn projectile prefab
                //Debug.Log("Pew!"); //placeholder for actually firing
                Instantiate(missile, transform.position, transform.rotation);
                //reset timer
                timer = 0f;
            }
        }
        
        //update camera position based on player position
    }

    //Button Scripts
    public void Btn(string btn)
    {
        switch (btn)
        {
            case "pause":
                ui.enabled = false;
                pause.enabled = true;
                break;
            case "resume":
                //resume the game and take down the pause menu
                pause.enabled = false;
                ui.enabled = true;
                break;
            case "howto":
                //disable pause canvas, enable how-to canvas
                pause.enabled = false;
                howTo.enabled = true;
                break;
            case "return":
                //return to pause from how-to
                howTo.enabled = false;
                pause.enabled = true;
                break;
            case "restart":
                //reload the game scene
                pause.enabled = false;
                ui.enabled = true;
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                break;
            case "main":
                pause.enabled = false;
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            case "exit":
                //quit the application
                Application.Quit();
                break;
        }
    }

    public void OnDeath()
    {
        //activate death animation?
        //stop all movement
        ui.enabled = false;
        gameOver.enabled = true;
    }

    public void OnKill()
    {
        //update score
    }

    public void Collect()
    {
        //update health, speedUp or fireRateUp depending on which powerup was collected
    }

    public void Hurt()
    {
        health--;
        if(health == 0)
        {
            ui.enabled = false;
            gameOver.enabled = true;
        }
    }
}
