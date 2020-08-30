using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCut : MonoBehaviour
{
    public AudioSource type;
    public bool moving, typing;
    public TextMeshProUGUI textbox;
    public UnityEngine.UI.Image Logo, blackout;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        Logo.color = new Color(1,1,1,0);
        StartCoroutine(fadein());
        yield return new WaitForSeconds(0.9f);

        StartCoroutine(textType("The year is " + (DateTime.Now.Year + 10).ToString() + ". \n \nA newfound virus is spreading, and fast."));
        StartCoroutine(MoveCam(new Vector3(0,-55,0), 21));
        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => typing == true);
        StartCoroutine(textType("With the pandemic still under the public eye, and no vaccine in sight, only the brightest minds at Doxbridge University can save us."));
        yield return new WaitWhile(() => (typing || moving) == true);
        yield return new WaitForSeconds(1);
        StartCoroutine(textType("Former warden and self-described genius, Dr. Borebridge, established the P.U.P.S program. \n\n(Pandemic Use of Peripheral Scent)."));
        StartCoroutine(MoveCam(new Vector3(0, -71, 0), 30));
        yield return new WaitWhile(() => typing == true);
        StartCoroutine(textType("Specially trained dogs are deployed to sniff out and eradicate any virus posing danger to the human race."));
        yield return new WaitWhile(() => typing == true);
        StartCoroutine(textType("And one very good boy is going to put this to the test."));
        yield return new WaitWhile(() => (typing || moving) == true);
        yield return new WaitForSeconds(0.5f);
        do
        {
            Logo.color += new Color(0, 0, 0, Time.deltaTime);
            yield return new WaitForEndOfFrame();

        } while (Logo.color.a < 1);
        yield return new WaitForSeconds(4);
        StartCoroutine(FadeOut("Cut1"));
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
        textbox.color = new Color(1,1,1, 1);
        foreach (char i in text)
        {
            textbox.text += i;
            yield return new WaitForSeconds(0.09f);
            
        }

        yield return new WaitForSeconds(1f);

        do
        {
            textbox.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return new WaitForEndOfFrame();

        } while (textbox.color.a > 0);
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
        
    }
}
