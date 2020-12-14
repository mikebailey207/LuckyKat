using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> slices;
    private int sliceChooser;
    private int removedSliceAmount;
    
    // difficultyModifierDeathSlices;
    
    void Awake()
    {
        GeneratePlatform();
    }

    void GeneratePlatform()
    {     
        // remove certain amount of slices based on difficulty/stage of the game    
        removedSliceAmount = Random.Range(PlatformSpawner.psSingleton.difficultyModifierPlatforms, slices.Count);
      
        for (int i = 0; i < removedSliceAmount; i++)
        {           
            sliceChooser = Random.Range(0, slices.Count);
            slices[sliceChooser].SetActive(false);
            slices.Remove(slices[sliceChooser]);
            //make a random amount of these slices into death slices
           
            int deathSliceAmount = Random.Range(0, slices.Count);    
            for (int j = 0; j < deathSliceAmount; j++)
            {      
                slices[deathSliceAmount].GetComponent<Renderer>().material.color = Color.blue;
            }
        }     
    }
}
