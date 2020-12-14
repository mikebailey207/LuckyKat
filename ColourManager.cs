using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourManager : MonoBehaviour
{
    public Color helixColour, ballColour, deathColour;
    
    public static ColourManager cmSingleton;
   
    
    private void Start()
    {
        GenerateColour();
        if (cmSingleton == null) cmSingleton = this;
        else if (cmSingleton != null) Destroy(gameObject);
       
    }
    public void GenerateColour()
    {
         helixColour = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //helixColour = Color.HSVToRGB(21, 97, 109);
         ballColour = Random.ColorHSV(1f, 1f, 1f, 1f, 0.5f, 1f);
      //  ballColour = Color.HSVToRGB(0, 21, 36);
         deathColour = Random.ColorHSV(0.5f, 1f, 1f, 1f, 0.5f, 1f);
      //  deathColour = Color.HSVToRGB(255, 125, 0);
    }
}
