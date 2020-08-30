using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cred : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI b1,b2;

    IEnumerator Start()
    {
        b1.text = "Programming, Models/Animations, Design and music by";
        b2.text = "NED REID";
        yield return new WaitForSeconds(5);
        b1.text = "Made in 4 days for";
        b2.text = "DURJAM 2020";
        yield return new WaitForSeconds(4);
        b1.text = "Assets and sources used:";
        b2.text = "Architectural textures by Nobiax / Yughues\nToon Shader made by SnutiHQ\nGame Models made using Blender\nGame cobbled together using Unity\n\n Full credits in Readme";
        yield return new WaitForSeconds(8);
        b1.text = "Special thanks to:";
        b2.text = "The Durjam team and sponsors, for making this possible.\n\nHarry, for playtesting";
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
