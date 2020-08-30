
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cut1 : MonoBehaviour
{
    public AudioSource type;
    public bool moving, typing, skip;
    public TextMeshProUGUI textbox;
    public UnityEngine.UI.Image blackout;
    float last;
    int step;
    // Start is called before the first frame update
    void Start()
    {
        step = 1;

        StartCoroutine(fadein());
        StartCoroutine(textType("Agent Pablo. Good to see you. Press SPACE to advance my voice."));
    }





    IEnumerator MoveCam(Vector3 dis, float atime)
    {
        moving = true;
        float counter = atime;
        do
        {
            transform.position += dis * Time.deltaTime / atime;
            counter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (counter > 0);
        moving = false;
    }

    IEnumerator textType(string text)
    {
        textbox.text = "";
        typing = true;
        textbox.color = new Color(1, 1, 1, 1);
        for (int i =0; i < text.Length; i++)
        {
            if (skip)
            {
                skip = false;
                textbox.text += text.Substring(i);
                break;
              
            }
            textbox.text += text[i];
            yield return new WaitForSeconds(0.05f);
    
        }


        typing = false;
    }


    IEnumerator fadein()
    {
        blackout.color = new Color(0, 0, 0, 1);
        do
        {
            blackout.color -= new Color(0, 0, 0, Time.deltaTime / 2);
            yield return new WaitForEndOfFrame();

        } while (blackout.color.a > 0);

    }
    IEnumerator FadeOut(string NextScene)
    {
        blackout.color = new Color(0, 0, 0, 0);
        do
        {
            blackout.color += new Color(0, 0, 0, Time.deltaTime / 2);
            yield return new WaitForEndOfFrame();

        } while (blackout.color.a < 1);
        SceneManager.LoadScene(NextScene);
    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxis("Jump") > 0 && last <= 0)
        {
            if(!typing)
            {
                step += 1;
                switch (step)
                {
                    case 1:
                        StartCoroutine(textType("Agent Pablo. Good to see you. Press SPACE to advance my voice."));
                        break;
                    case 2:
                        StartCoroutine(textType("With all the students going back to lectures, we need you on duty at the lecture theatres."));
                        break;
                    case 3:
                        StartCoroutine(textType("I'm sure you haven't forgotten how to walk, but you can do so with the WASD keys"));
                        break;
                    case 4:
                        StartCoroutine(textType("You can detect the people with the virus using your smellovision. Just hold TAB to access it"));
                        break;
                    case 5:
                        StartCoroutine(textType("If you find someone carrying the virus, you can jump and bite it out of them by pressing SPACE"));
                        break;
                    case 6:
                        StartCoroutine(textType("Just make sure to do it before they spread it to everyone else. Good luck, Agent Pablo!"));
                        break;
                    default:
                        StartCoroutine(FadeOut("TLC"));
                        break;


                }
            }
            else
            {
                skip = true;
            }


        }

        last = Input.GetAxis("Jump");

    }
}
