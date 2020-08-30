using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PugController : MonoBehaviour
{
    public Animator ani;
    public float sensitivity;
//    float prevMouse;
    public float speed,sspeed,jump;
    public bool jumping;
    float state;
    Rigidbody rb;
    Vector3 locRot;
    bool paused;
    GameController gc;


    Vector3 startP;
    // Start is called before the first frame update
    void Start()
    {

        startP = transform.position;
        gc = FindObjectOfType<GameController>();
    //    prevMouse = (Input.mousePosition.x / Screen.width);
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "floor")
        {
            jumping = false;
        }
        else if (other.gameObject.tag == "bound")
        {
            rb.velocity = Vector3.zero;
            transform.position = startP;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(gc.final2)
        {
            if(gc.evac == other.gameObject)
            {
                StartCoroutine(gc.FadeOut("finale"));
            }
        }
    }
    // Update is called once per frames
    void FixedUpdate()
    {
        paused = gc.paused;
        if (!paused)
        {
            ani.enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
            float hoz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            //    Debug.Log(prevMouse);

            if (!jumping)
            {
                transform.eulerAngles += Vector3.up * Input.GetAxis("Mouse X") * sensitivity;
         //       prevMouse = (Input.mousePosition.x / Screen.width);
                rb.velocity = new Vector3((transform.forward.x * speed * vert + transform.right.x * sspeed * hoz), rb.velocity.y, (transform.forward.z * speed * vert + transform.right.z * sspeed * hoz));

                state = Mathf.Abs(vert) + Mathf.Abs(hoz) - 50 * Input.GetAxis("Jump");
                locRot = Vector3.up * Mathf.Atan2(hoz, vert) * (180 / Mathf.PI) + 90 * Vector3.up;
                ani.gameObject.transform.localRotation = Quaternion.Lerp(ani.gameObject.transform.localRotation, Quaternion.Euler(locRot), 10 * Time.fixedDeltaTime);
            }

            if (Mathf.Abs(state) < 0.2f)
            {
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle") == false)
                {
                    ani.SetTrigger("idle");
                }
            }
            else if (state >= 0.2f)
            {
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("run") == false)
                {
                    ani.SetTrigger("run");
                }

            }
            else
            {
                if (ani.GetCurrentAnimatorStateInfo(0).IsName("leap") == false && jumping == false && Input.GetAxis("Fire1") <= 0)
                {

                    ani.SetTrigger("leap");

                 //   transform.eulerAngles = (ani.gameObject.transform.eulerAngles - 90 * Vector3.up);
                 //   ani.gameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
                       rb.velocity = (((-1.2f * ani.gameObject.transform.right) + Vector3.up) * jump);
                    jumping = true;
                }
                //MAKE MORE CONSISTANT!
            }
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            ani.enabled = false;
        }

    }
    
}
