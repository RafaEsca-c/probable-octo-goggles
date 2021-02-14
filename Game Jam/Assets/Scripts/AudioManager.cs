using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip fire1, fire2;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        fire1 = Resources.Load<AudioClip>("gun");
        fire2 = Resources.Load<AudioClip>("pistol_no_distor");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "fire1":
                audioSrc.PlayOneShot(fire1);
                break;
            case "fire2":
                audioSrc.PlayOneShot(fire2);
                break;
        }
    }
}
