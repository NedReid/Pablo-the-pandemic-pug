using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class humannav : MonoBehaviour
{
    public seat[] seats;
    public seat chosenSeat;
    public bool foundloc;
    public bool HasVirus;
    public List<GameObject> collisions;
    public ParticleSystem ps,exp;
    List<float> collisionT;
    public Material human2;

    bool paused;
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        exp.gameObject.SetActive(false);
        gc = FindObjectOfType<GameController>();
        seats = GameObject.FindObjectsOfType<seat>();
        GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(1f, 4);
        collisions = new List<GameObject>();
        collisionT = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {

        paused = gc.paused;
        if (!paused)
        {
            if (HasVirus)
            {
                transform.Find("person").Find("Cube").gameObject.GetComponent<MeshRenderer>().material = human2;
                transform.Find("person").Find("Icosphere").gameObject.GetComponent<MeshRenderer>().material = human2;

                ps.gameObject.SetActive(true);
            }
            else
            {
                ps.gameObject.SetActive(false);
            }
            if (!foundloc)
            {
                int index = UnityEngine.Random.Range(0, seats.Length);
                if (!seats[index].claimed && !(seats[index] == chosenSeat))
                {
                    foundloc = true;
                    chosenSeat = seats[index];

                    seats[index].claimed = true;
                    GetComponent<NavMeshAgent>().SetDestination(chosenSeat.gameObject.transform.position);
                    GetComponent<NavMeshAgent>().isStopped = false;
                }
            }
            // Debug.Log(GetComponent<NavMeshAgent>().remainingDistance);
            if ((transform.position - chosenSeat.gameObject.transform.position).magnitude < 3 && chosenSeat.claimed)
            {
                chosenSeat.claimed = false;
                GetComponent<NavMeshAgent>().isStopped = true;
                Debug.Log("Triggered 1");
                StartCoroutine("WaitAgain");

            }
            else if(foundloc && chosenSeat.claimed)
            {
                GetComponent<NavMeshAgent>().isStopped = false;

            }
        }
        else
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }

    }

    IEnumerator WaitAgain()
    {
        Debug.Log("Triggered 2");
        yield return new WaitForSeconds(UnityEngine.Random.Range(.5f, 3f));
        foundloc = false;

    }
    private IEnumerator OnCollisionEnter(Collision other)
    {

            GameObject OtherOb = other.gameObject;
            if (OtherOb.name == "pug")
            {
                if (OtherOb.GetComponent<PugController>().jumping == true)
                {
                    exp.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1);
                    Destroy(gameObject);

                }
            }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(!HasVirus && other.transform.parent != null)
        {

            GameObject OtherOb = other.transform.parent.gameObject;
            if (OtherOb.TryGetComponent(out humannav nav) && OtherOb != gameObject)
            {
                if (nav.HasVirus == true)
                {
                    Debug.Log("ADDING");
                    collisions.Add(OtherOb);
                    collisionT.Add(Time.deltaTime);
                }

            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (!HasVirus && other.transform.parent != null && !paused)
        {

            GameObject OtherOb = other.transform.parent.gameObject;


                if (collisions.Contains(OtherOb))
                {
                    collisionT[collisions.IndexOf(OtherOb)] += Time.deltaTime;
                    if (collisionT[collisions.IndexOf(OtherOb)] > 3)
                    {
                        HasVirus = true;

                        Debug.Log("I HAVE VIRUS");
                        
                    }
                }

            
  
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (!HasVirus && other.transform.parent != null)
        {
            GameObject OtherOb = other.transform.parent.gameObject;

            if (OtherOb.TryGetComponent(out humannav nav) && collisions.Contains(OtherOb))
            {
                collisionT.RemoveAt(collisions.IndexOf(OtherOb));
                collisions.Remove(OtherOb);

            }

        }

    }

}
