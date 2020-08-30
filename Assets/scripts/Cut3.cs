
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cut3 : MonoBehaviour
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
        StartCoroutine(textType("We made it this far thanks to your efforts, agent Pablo, but I'm afraid it's the end of the road."));
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
                        StartCoroutine(textType("We got notice that Doxbridge's worst nightclub, Qlute, is opening back up."));
                        break;
                    case 2:
                        StartCoroutine(textType("That place is a hellhole. An epicentre of diseases within itself."));
                        break;
                    case 3:
                        StartCoroutine(textType("There's no way we can stop the spread. It's too late.... Unless?"));
                        break;
                    case 4:
                        StartCoroutine(textType("Agent. Go into the club. I don't know if this is going to work, but we might be able to curve the spread early."));
                        break;
                    case 5:
                        StartCoroutine(textType("And if this doesn't work, well, we may have to resort to... special measures."));
                        break;
                    default:
                        StartCoroutine(FadeOut("qlute"));
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
