using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Canvas mainMenu;
    private Canvas howTo;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenu").GetComponent<Canvas>();
        howTo = GameObject.Find("HowTo").GetComponent<Canvas>();
        howTo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn(string btn)
    {
        switch (btn)
        {
            case "play":
                //load game scene
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                break;
            case "howto":
                //disable main menu canvas, enable how-to canvas
                mainMenu.enabled = false;
                howTo.enabled = true;
                break;
            case "return":
                //return to main menu from how-to
                howTo.enabled = false;
                mainMenu.enabled = true;
                break;
            case "exit":
                //quit the application
                Application.Quit();
                break;
        }
    }
}
