using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip hopSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioSource audioSource;
    // [SerializeField] GameObject bgm;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Game Manager is NULL");

            return _instance;
        }
   }

    private void Awake()
    {
        _instance = this;
    }

    public void PlaySFX(string clip)
    {
        if (clip == "Hop")
        {
            audioSource.clip = hopSFX;
        }
        else if (clip == "Death")
        {
            audioSource.clip = deathSFX;
        }

        audioSource.Play();

    }


}



