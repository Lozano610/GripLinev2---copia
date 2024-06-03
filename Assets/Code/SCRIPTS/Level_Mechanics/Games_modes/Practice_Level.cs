using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using JetBrains.Annotations;

public class Practice_Level : MonoBehaviour
{
    #region Variables
    public TMP_Text timerText;
    public TMP_Text Timebest;
    private float startTime;
    private float bestTime = 9999999;
    public bool raceStarted = false;
    public bool lapCompleted = false;
    public SplineAnimator PlayerController;
    public float countdownDuration;
    public UnityEvent onEventoLanzado;
    public AudioSource Audio_Level;
    #endregion
    #region Unity Methods
    private void Start()
    {
        StartCoroutine(StartCountdown());
    }
    private void Update()
    {
        if (!raceStarted)
        {
            return;
        }
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }
    #endregion
    #region Own Methods
    IEnumerator StartCountdown()
    {
        WaitForSeconds wait = new WaitForSeconds(countdownDuration);
        yield return wait;
        StartRace();
        onEventoLanzado.Invoke();
    }
    public void StartRace()
    {
        raceStarted = true;
        startTime = Time.time;
    }
    public void CompleteLap()
    {
        if (raceStarted && !lapCompleted)
        {
            lapCompleted = true;
            raceStarted = false; // Detener el temporizador
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lapCompleted == false && raceStarted == true)
            {
                float t = Time.time - startTime; 
                if(t < bestTime)
                {
                    bestTime = t;
                    string minutes = ((int)bestTime / 60).ToString("00");
                    string seconds = (bestTime % 60).ToString("00");
                    Timebest.text = minutes + ":" + seconds;
                }
                startTime = Time.time;
            }
        }
    }
    #endregion
}
