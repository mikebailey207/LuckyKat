using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public int difficultyModifierPlatforms;
    public float difficultyModifierDeathSlices;

    [SerializeField]
    Transform startPlatform, endPlatform, helix;
    [SerializeField]

    GameObject platformPrefab;
    Vector3 startRotation;
    
    float distanceToEnd;
    float extraSpace = 0.1f;
    List<GameObject> platformsList;

    public static PlatformSpawner psSingleton;
    int totalPlatforms = 30; // I would tie this to difficulty in a full version of the game, for now it's constant

    void Start()
    {
        if (psSingleton == null) psSingleton = this;
        else if (psSingleton != null) Destroy(gameObject);
        
        distanceToEnd = startPlatform.position.y - (endPlatform.position.y + extraSpace);//calculate distance of course

        startRotation = transform.eulerAngles;
        LoadPlatform();            
    }

    public void LoadPlatform()
    {
       
        float gapBetweenPlatforms = distanceToEnd / totalPlatforms; // Calculate how far to move the spawnpoint down on the Y axis

        float spawnPosY = startPlatform.localPosition.y;// set that Y axis

        for (int i = 0; i < totalPlatforms - 1; i++)// iterate through until all platforms have been instantiated   
        {            
            
            GameObject platform = Instantiate(platformPrefab, transform);// Instantiate platform
     
            platform.transform.position = new Vector3(0, spawnPosY, 0);// Set transform.position

            spawnPosY -= gapBetweenPlatforms; // decrease Y value of next spawned platform;
  
            platform.transform.SetParent(helix);// set the current platform's parent to the helix object
        }
    }
}
