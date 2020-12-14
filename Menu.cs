using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject game;

    public void StartPlay()
    {
        game.SetActive(true); // I have the entire playable game collapsed in the hierachy to load from here
        Destroy(gameObject);
    }
}
