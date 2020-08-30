using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    humannav[] allHumans;
    public bool started, finished;
    public int OPeople, deaths, currentV, max;
    public TextMeshProUGUI Counter;
    public GameObject results, pause,retry,next, over, ftext, evac;
    public TextMeshProUGUI maxD, Attacks, FinalTime, Ranking, RankingName;
    public DateTime start;
    public Image blackout;
    public bool paused, final, final2;
    int losestate;
    // Start is called before the first frame update
    void Start()
    {
        losestate = 14;
        pause.SetActive(false);
        started = true;
        results.SetActive(false);
        start = DateTime.Now;
        allHumans = FindObjectsOfType<humannav>();
        allHumans[UnityEngine.Random.Range(0, allHumans.Length)].HasVirus = true;
        allHumans[UnityEngine.Random.Range(0, allHumans.Length)].HasVirus = true;
        InvokeRepeating("checkStatus", 1f, 1f);
        OPeople = allHumans.Length;
        next.SetActive(false);
        retry.SetActive(false);
        over.SetActive(false);
        if(final)
        {
            ftext.SetActive(false);
            evac.SetActive(false);
            losestate = 17;
            if(PlayerPrefs.GetInt("failed") == 1)
            {
                PlayerPrefs.SetInt("failed", 0);
                final2 = true;
            }
        }
    }


    void checkStatus()
    {
        if(started && !finished)
        {
            allHumans = FindObjectsOfType<humannav>();
            deaths = OPeople - allHumans.Length;

            currentV = 0;
            foreach (humannav human in allHumans)
            {
                if (human.HasVirus)
                {
                    currentV += 1;
                }
            }
            max = Mathf.Max(currentV, max);
            Counter.text = currentV.ToString();

            if (currentV == 0)
            {
                paused = true;
                results.SetActive(true);
                maxD.text = max.ToString();
                Attacks.text = deaths.ToString();
                FinalTime.text = Convert.ToInt32((DateTime.Now - start).TotalSeconds).ToString() + "s";
                next.SetActive(true);
                retry.SetActive(true);
                float rankNum = deaths + max + Convert.ToSingle((DateTime.Now - start).TotalSeconds / 10);
                if (rankNum < 10)
                {
                    Ranking.text = "S";
                    RankingName.text = "(The Best Boy)";
                }
                else if (rankNum < 15)
                {
                    Ranking.text = "A";
                    RankingName.text = "(Extremely good Boy)";
                }
                else if (rankNum < 20)
                {
                    Ranking.text = "B";
                    RankingName.text = "(Good Boy)";
                }
                else if (rankNum < 30)
                {
                    Ranking.text = "C";
                    RankingName.text = "(Adequately good boy)";
                }
                else if (rankNum < 40)
                {
                    Ranking.text = "D";
                    RankingName.text = "(Not very good boy)";
                }
                else if (rankNum < 50)
                {
                    Ranking.text = "E";
                    RankingName.text = "(Bad Boy)";
                }
                else if (rankNum >= 60)
                {
                    Ranking.text = "F";
                    RankingName.text = "(Did you play dead?)";
                }
                finished = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                retry.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FadeOut(SceneManager.GetActiveScene().name)); });
                switch (SceneManager.GetActiveScene().name)                    
                {
                    case "TLC":
                        next.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FadeOut("Cut2")); });
                        break;
                    case "hex":
                        next.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FadeOut("Cut3")); });
                        break;
                    case "qlute":
                        next.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FadeOut("credits")); });
                        break;
                }
   
            }
            else if (currentV > losestate && !finished && !final2)
            {
                finished = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                paused = true;
                over.SetActive(true);
                retry.SetActive(true);
                retry.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FadeOut(SceneManager.GetActiveScene().name)); });
                if(final)
                {
                    PlayerPrefs.SetInt("failed", 1);
                }
            }
            else if (currentV > losestate && !finished && final2)
            {
                losestate = 999;
                ftext.SetActive(true);
                ftext.transform.Find("countdown").gameObject.SetActive(false);
                // ftext.transform.Find("ftext").gameObject.SetActive(true);
                StartCoroutine(textType("This isn't working Pablo. Evacuate and prepare for Operation Delta. Smell the evcuation point with TAB", ftext.transform.Find("ftext").Find("text").gameObject.GetComponent<TextMeshProUGUI>()));


              //  over.SetActive(true);
             //   retry.SetActive(true);
             //   retry.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(FadeOut(SceneManager.GetActiveScene().name)); });
            }
            
        }
        if(final2)
        {
            if(ftext.transform.Find("countdown").gameObject.activeSelf)
            {
                ftext.transform.Find("countdown").gameObject.GetComponent<TextMeshProUGUI>().text = (Int32.Parse(ftext.transform.Find("countdown").gameObject.GetComponent<TextMeshProUGUI>().text) - 1).ToString();
            }
        }
        

    }
    public IEnumerator FadeOut(string NextScene)
    {
        blackout.color = new Color(0, 0, 0, 0);
        do
        {
            blackout.color += new Color(0, 0, 0, Time.deltaTime / 2);
            yield return new WaitForEndOfFrame();

        } while (blackout.color.a < 1);

        SceneManager.LoadScene(NextScene);
        
    }


    IEnumerator textType(string text, TextMeshProUGUI textbox)
    {
        textbox.text = "";

        textbox.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < text.Length; i++)
        {
            textbox.text += text[i];
            yield return new WaitForSeconds(0.05f);

        }
        evac.SetActive(true);
        ftext.transform.Find("countdown").gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        ftext.transform.Find("ftext").gameObject.SetActive(false);






    }


    // Update is called once per frame
    void Update()
    {
        if(paused)
        {
            start = start.AddSeconds(Time.deltaTime);
        }
        if ((finished || paused) && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(FadeOut(SceneManager.GetActiveScene().name));
        }
        if (!finished && Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(!pause.activeSelf);
            paused = !paused;
            
        }
    }
}
