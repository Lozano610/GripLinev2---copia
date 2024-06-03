using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed_Sound : MonoBehaviour
{
    public AudioSource SpeedSound;
    public SplineAnimator speed_Player;
    public Player Max_SpeedPlayer;
    public Slider volumeSlider;
    void Update()
    {
        float volumen = speed_Player.speed / Max_SpeedPlayer.Max_SpeedPlayer;
        SpeedSound.volume = volumen;
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
