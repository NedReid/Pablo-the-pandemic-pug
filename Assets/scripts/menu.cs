using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public Button butt;
    public Image blackout;
    // Start is called before the first frame update
    void Start()
    {
        butt.onClick.AddListener(delegate  { StartCoroutine(FadeOut("Opening")); });
    }
    IEnumerator FadeOut(string NextScene)
    {
        blackout.color = new Color(0, 0, 0, 0);
        do
        {
            blackout.color += new Color(0, 0, 0, Time.deltaTime / 2);
            yield return new WaitForEndOfFrame();

        } while (blackout.color.a < 1);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(NextScene);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
