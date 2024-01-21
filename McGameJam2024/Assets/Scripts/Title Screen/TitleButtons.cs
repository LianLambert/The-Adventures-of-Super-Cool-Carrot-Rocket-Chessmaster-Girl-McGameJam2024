using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] private GameObject singleplayerButton;
    [SerializeField] private GameObject multiplayerButton;
    private bool singleplayer = true; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            SetButtonColors(new Color(235 / 255.0f, 148 / 255.0f, 34 / 255.0f), new Color(137 / 255.0f, 137 / 255.0f, 137 / 255.0f));
            singleplayer = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            SetButtonColors(new Color(137 / 255.0f, 137 / 255.0f, 137 / 255.0f), new Color(235 / 255.0f, 148 / 255.0f, 34 / 255.0f));
            singleplayer = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (singleplayer)
            {
                SceneManager.LoadScene("Singleplayer");
            }
            else
            {
                SceneManager.LoadScene("Multiplayer");
            }
        }
    }

    void SetButtonColors(Color color1, Color color2)
    {
        SetButtonColor(singleplayerButton, color1);
        SetButtonColor(multiplayerButton, color2);
    }

    void SetButtonColor(GameObject buttonObject, Color color)
    {
        Image buttonImage = buttonObject.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }
}
