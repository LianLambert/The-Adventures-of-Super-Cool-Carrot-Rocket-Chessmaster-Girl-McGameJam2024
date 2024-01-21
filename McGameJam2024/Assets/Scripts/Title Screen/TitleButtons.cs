using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] private Transform Girl;
    [SerializeField] private Transform Boy;
    [SerializeField] private CanvasGroup fade;
    [SerializeField] private GameObject singleplayerButton;
    [SerializeField] private GameObject multiplayerButton;
    private bool anim_finished = false;
    private bool singleplayer = true; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = new Vector3(0, 0, 0);
        if (!anim_finished)
        {
            Girl.position = Vector3.MoveTowards(Girl.position, origin, 2f * Time.deltaTime);

            if(Girl.position == origin)
            {
                Boy.position = Vector3.MoveTowards(Boy.position, origin, 2f * Time.deltaTime);
            }

            if(Boy.position == origin)
            {
                if(fade.alpha <= 0.65f)
                {
                    fade.alpha += 0.002f;
                }
            }
        }





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

    IEnumerator TitleAnimation()
    {
        yield return new WaitForSeconds(1f);

        Vector3 origin = new Vector3(0, 0, 0);
        while(Girl.position != origin)
        {
            Girl.position = Vector3.MoveTowards(Girl.position, new Vector3(0, 0, 0), 1.5f * Time.deltaTime);
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
