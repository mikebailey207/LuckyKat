using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public int difficultyModifierPlatforms;

    [SerializeField]
    Transform startPlatform, endPlatform, helix;
    [SerializeField]

    GameObject platformPrefab;
    Vector3 startRotation;
    
    float distanceToEnd;
    float extraSpace = 0.1f;
    List<GameObject> platformsList;

    public static PlatformSpawner psSingleton;
    int totalPlatforms = 30;

    void Start()
    {
        if (psSingleton == null) psSingleton = this;
        else if (psSingleton != null) Destroy(gameObject);
        //calculate distance of course
        distanceToEnd = startPlatform.position.y - (endPlatform.position.y + extraSpace);

        startRotation = transform.eulerAngles;
        LoadPlatform();            
    }

    public void LoadPlatform()
    {
        float gapBetweenPlatforms = distanceToEnd / totalPlatforms;
        float spawnPosY = startPlatform.localPosition.y;
    
        for (int i = 0; i < totalPlatforms - 1; i++)
        {            
            GameObject platform = Instantiate(platformPrefab, transform);
           
            platform.transform.position = new Vector3(0, spawnPosY, 0);

            spawnPosY -= gapBetweenPlatforms;
            platform.transform.SetParent(helix);
        }
    }
}
