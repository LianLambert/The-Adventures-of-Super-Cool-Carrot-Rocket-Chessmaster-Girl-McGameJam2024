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
    [SerializeField] private CanvasGroup MainTitle;
    [SerializeField] private CanvasGroup AndKenTitle;
    [SerializeField] private CanvasGroup PressStart;
    [SerializeField] private CanvasGroup fade2;

    [SerializeField] private GameObject singleplayerButton;
    [SerializeField] private GameObject multiplayerButton;
    private bool anim_finished = false;
    private bool allcond = false;
    private bool singleplayer = true;
    private bool ken = false;

    // Audio
    [SerializeField] AudioClip kenSound;
    [SerializeField] AudioClip textSound;

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

            if(Girl.position == origin && !ken)
            {
                StartCoroutine(Pop(origin));
                ken = true;
            }

            if(Boy.position == origin)
            {
                if(fade.alpha <= 0.80f)
                {
                    fade.alpha += 0.002f;
                }
            }

            if(fade.alpha > 0.65f)
            {
                if(MainTitle.alpha < 1)
                {
                    MainTitle.alpha += 0.01f;
                }
            }

            if(MainTitle.alpha >= 1)
            {
                StartCoroutine(Pop2());
            }

            if(AndKenTitle.alpha == 1)
            {
                if(PressStart.alpha < 1)
                {
                    PressStart.alpha += 0.01f;
                }
                if(PressStart.alpha == 1 && Input.GetKeyDown(KeyCode.Space))
                {
                    allcond = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Girl.position = origin;
                Boy.position = origin;
                fade.alpha = 0.66f;
                MainTitle.alpha = 1f;
                AndKenTitle.alpha = 1f;
                PressStart.alpha = 1f;


            }


            if (allcond) {
                if (PressStart.alpha > 0)
                {
                    PressStart.alpha -= 0.05f;
                }
                if (fade2.alpha <= 0.80f)
                {
                    fade2.alpha += 0.02f;
                }

                if (fade2.alpha > 0.80f)
                {
                    singleplayerButton.SetActive(true);
                    multiplayerButton.SetActive(true);
                    anim_finished = true;
                }
            }
        }



        if (anim_finished)
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

    }

    IEnumerator Pop(Vector3 origin)
    {
        yield return new WaitForSeconds(0.5f);
        Boy.position = origin;
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(kenSound);

    }

    IEnumerator Pop2()
    {
        yield return new WaitForSeconds(1f);

        AndKenTitle.alpha = 1;

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
