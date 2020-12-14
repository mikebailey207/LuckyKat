using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    AudioSource pianoSingleNote;
    private float[] notePitches;

    public static SoundController scSingleton;

    private void Start()
    {
        if (scSingleton == null) scSingleton = this;
        else if (scSingleton != null) Destroy(gameObject);
        notePitches[0] = 1;
        notePitches[1] = 1.1f;
        notePitches[2] = 1.4f;
        notePitches[3] = 1.5f;
    }
    public void PlayNote(float pitch)
    {
        //int pitchChooser = Random.Range(0, notePitches.Length);

        

        pianoSingleNote.Play();

        Debug.Log("PlayNote?");
    }
}
