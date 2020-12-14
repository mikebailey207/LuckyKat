using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    [SerializeField]
    AudioSource music;
    
    public void ToggleMusic() // turn music on and off with button
    {
        if (!music.enabled) music.enabled = true;
        else if (music.enabled) music.enabled = false;

    }
}
