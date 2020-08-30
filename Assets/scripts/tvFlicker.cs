using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvFlicker : MonoBehaviour
{
    public Material b1, b2, b3;

    Vector3 origPos;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = origPos + Random.Range(-.01f, .01f) * transform.right + Random.Range(-.01f, .01f) * transform.up;
        int display = Random.Range(0, 100);
        if (display == 0)
        {
            GetComponent<MeshRenderer>().material = b2;
        }
        else if (display == 1)
        {
            GetComponent<MeshRenderer>().material = b3;
        }
        else
        {
            GetComponent<MeshRenderer>().material = b1;
        }


    }
}
