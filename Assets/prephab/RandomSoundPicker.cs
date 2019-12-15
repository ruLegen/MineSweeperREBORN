using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPicker : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] audio = new AudioClip[5];
    AudioSource source;
    private void Awake()
    {
        int index = Random.Range(0, audio.Length);
        try
        {
            source = gameObject.GetComponent<AudioSource>();

            source.clip = audio[index];
            source.Play();

        }
        catch (AndroidJavaException e)
        {
        }
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
