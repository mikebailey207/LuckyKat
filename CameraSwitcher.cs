using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    Camera cam, camFP;
   // bool isFirstPerson;
    void Update()
    {
      //  SwitchCam();
    }
    public void SwitchCam()
    {
     //   if (Input.GetKeyDown(KeyCode.Space))
     //   {
            if (cam.enabled)
            {
                camFP.enabled = true;
                cam.enabled = false;
            }
           else if (!cam.enabled)
            {
                camFP.enabled = false;
                cam.enabled = true;
            }
     //   }
    }
}

