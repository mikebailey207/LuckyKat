using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileAndMouseRotater : MonoBehaviour
{
    Vector3 startRot;
    Vector2 lastInputPos;
    Vector3 currentInputPos;

    void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if(Input.GetMouseButton(0))
       {
            
            Vector3 currentInputPos = Input.mousePosition;

            if(lastInputPos == Vector2.zero)
            {
                lastInputPos = currentInputPos;
            }
            float turnAmount = lastInputPos.x - currentInputPos.x;

            lastInputPos = currentInputPos;

            transform.Rotate(Vector3.up * turnAmount / 5);
       }
       /* if(Input.GetMouseButtonUp(0))
        {
            lastInputPos = currentInputPos;
        }*/
    }
}
