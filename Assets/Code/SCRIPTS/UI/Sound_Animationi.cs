using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Animationi : MonoBehaviour
{
    public AudioSource Sound_Light1;
    public AudioSource Sound_Light2;
    public void First_Sound()
    {
        Sound_Light1.Play();
    }
    public void Second_Sound()
    {
        Sound_Light2.Play();
    }
}
