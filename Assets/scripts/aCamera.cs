using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class aCamera : MonoBehaviour
{
    public GameObject dog;
    Vector3 pi;
    Vector3 OrigAngle;
    float yDeviation;
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        pi = Vector3.one * 360;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gc.paused)
        {
            yDeviation += Input.GetAxis("Mouse Y");
            yDeviation /= 1 + 1 * Time.fixedDeltaTime;
        }

        Vector3 dogPos = dog.transform.position;
        Quaternion dogRot = dog.transform.rotation;
        if (dog.GetComponent<PugController>().jumping)
        {
            //if(Quaternion.Angle(Quaternion.Euler(dogRot.eulerAngles - Vector3.up * 57), transform.rotation) < Quaternion.Angle(Quaternion.Euler(dogRot.eulerAngles + Vector3.up * 60), transform.rotation))
            //{
            //    dogRot.eulerAngles -= Vector3.up * 60;
            //}
            //else
            //{
            //    dogRot.eulerAngles += Vector3.up * 60;
            //}

        }
        if ((dogPos - transform.position).magnitude > 0.3f)
        {
        //    Debug.Log("Hmm");
            transform.position = (transform.position + (dogPos - transform.position) * Time.fixedDeltaTime);

        }
        transform.rotation = Quaternion.Lerp(transform.rotation, dogRot, 1.4f * Time.fixedDeltaTime);
        transform.eulerAngles = new Vector3(OrigAngle.x + (yDeviation / Mathf.Abs(yDeviation)) * Mathf.Pow(Mathf.Abs(yDeviation),0.8f), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
 