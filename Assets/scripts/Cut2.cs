
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cut2 : MonoBehaviour
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
        StartCoroutine(textType("Pretty good work, Agent Pablo. I think you deserve a good snack and a good rest."));
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
                        StartCoroutine(textType("Pretty good work, Agent Pablo. I think you deserve a good snack and a good rest."));
                        break;
                    case 2:
                        StartCoroutine(textType("OH NO! This isn't good Agent. This is not good at all."));
                        break;
                    case 3:
                        StartCoroutine(textType("We just got word that Hexagon College is moving back into their accommodation."));
                        break;
                    case 4:
                        StartCoroutine(textType("I'm sure you know the 'rumours' about the Hexagon College students. Yes, they're true."));
                        break;
                    case 5:
                        StartCoroutine(textType("If you don't intervene, they could quickly cause a microcosm. Get going now! (please)"));
                        break;
                    default:
                        StartCoroutine(FadeOut("hex"));
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
