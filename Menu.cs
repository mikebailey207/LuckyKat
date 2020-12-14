using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject game;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartPlay();
        }
    }
    public void StartPlay()
    {
        Debug.Log("Click");
        game.SetActive(true);
        Destroy(gameObject);
    }
}
