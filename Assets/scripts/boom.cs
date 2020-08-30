using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class boom : MonoBehaviour
{

    public Image blackout;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        transform.position = new Vector3(20, 8, 91);
        transform.eulerAngles = new Vector3(7,150,0);
        yield return new WaitForSeconds(3);
        transform.position = new Vector3(26.6344814f, -5.30583477f, 80.1184235f);
        transform.eulerAngles = new Vector3(9.04078388f, 142.255264f, -0);
        yield return new WaitForSeconds(3);
        transform.position = new Vector3(-5.01089668f, 10.7657413f, 47.5621605f);
        transform.eulerAngles = new Vector3(28.4640923f, 33.6190071f, 0);
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOut("credits"));
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
