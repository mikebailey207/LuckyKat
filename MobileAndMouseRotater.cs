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

    private void Rotate() // Rotate game object (helix) with touch input (and mouse)
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

            transform.Rotate(Vector3.up * turnAmount / 5); // this seemed to work as a figure, though I may add sensitivity to the options screen in a full version
       }
    }
}
