using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class smellovision : MonoBehaviour
{
    public Volume smellOverride;
    public Camera SmellCam;
    public Camera cam;
    public PugController dog;
 //   public bool smell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") > 0 && !dog.jumping)
        {
            if(smellOverride.weight < 1)
            {
                smellOverride.weight += 2 * Time.deltaTime;

            }
            if (!cam.GetUniversalAdditionalCameraData().cameraStack.Contains(SmellCam))
            {
                cam.GetUniversalAdditionalCameraData().cameraStack.Add(SmellCam);
            }
            else if (SmellCam.farClipPlane < 70)
            {
                SmellCam.farClipPlane += 20 * Time.deltaTime;
            }

        }
        else
        {
            if (smellOverride.weight > 0)
            {
                smellOverride.weight -= 2 * Time.deltaTime;
                
            }

            if(SmellCam.farClipPlane > 0.3f)
            {
                SmellCam.farClipPlane -= 40 * Time.deltaTime;
            }
            else if (cam.GetUniversalAdditionalCameraData().cameraStack.Contains(SmellCam))
            {
                cam.GetUniversalAdditionalCameraData().cameraStack.Remove(SmellCam);
            }
        }
    }
}
