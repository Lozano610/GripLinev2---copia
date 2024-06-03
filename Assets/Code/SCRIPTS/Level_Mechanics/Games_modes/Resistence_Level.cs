using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Resistence_Level : MonoBehaviour
{
    #region Variables
    public TMP_Text Timer;
    public TMP_Text Kilometers;
    public TMP_Text CoinsFinish;
    public GameObject FinishScreen;
    public GameObject HUD_Screen;
    public SplineAnimator PlayerSpeed;
    public float CountdownDuration;
    public UnityEvent onEventoLanzado;
    public AudioSource Music;
    public Image[] stars;
    public bool raceStarted = false;
    public bool lapCompleted = false;
    public float start_time = 60f;
    public int LapCounts;
    public float basetime = 25f;
    public float CircuitKilometers;
    public int nivelActualIndex;
    public float KilometersOneStar;
    public float KilometersTwoStar;
    public float KilometersThreeStar;
    //public ManagerSavingObjects managerSavingObjects;
    #endregion
    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdown());
        FinishScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!raceStarted)
        {
            return;
        }
        //float t = Time.time - start_time;
        if(start_time > 0)
        {
            start_time -= Time.deltaTime;
        }
        else if(lapCompleted == false)
        {
            StopRace();
            lapCompleted = true;
        }
        string minutes = ((int)start_time / 60).ToString("00");
        string seconds = (start_time % 60).ToString("00");
        Timer.text = minutes + ":" + seconds;

        Kilometers.text = (PlayerSpeed.passedTime * CircuitKilometers).ToString("F2");
    }
    #endregion
    #region OwnMethods
    IEnumerator StartCountdown()
    {
        WaitForSeconds wait = new WaitForSeconds(CountdownDuration);
        yield return wait;
        onEventoLanzado.Invoke();
        StartRace();
    }
    public void StartRace()
    {
        raceStarted = true;
    }
    public void StopRace()
    {
            raceStarted = false;
            PlayerSpeed.PlayerEnabled = false;
            FinishScreen.SetActive(true);
            HUD_Screen.SetActive(false);
            ManagerSavingObjects.Singleton.Guardar();
        if (MenuManager.Singleton.SceneComplete < nivelActualIndex)
        {
            MenuManager.Singleton.SceneComplete = nivelActualIndex;
        }
    }
    public void AñadirTiempo()
    {
        if(LapCounts == 0)
        {
            start_time += basetime;
        }
        else 
        {
            float time = basetime * Mathf.Pow(0.8f, LapCounts); 
            start_time += time;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LapCounts++;
        }
    }
    void AsignarEstrellas()
    {
        float Meters = float.Parse(Kilometers.text);
        int estrellas = 0;
        if (Meters <= KilometersOneStar)
        {
            estrellas = 3;
            Coins.TotalCoins += 1000;
            CoinsFinish.text = "1000GL";
        }
        if (Meters <= KilometersTwoStar)
        {
            estrellas = 2;
            Coins.TotalCoins += 500;
            CoinsFinish.text = "500GL";
        }
        if (Meters <= KilometersThreeStar)
        {
            estrellas = 1;
            Coins.TotalCoins += 200;
            CoinsFinish.text = "200GL";
        }
        for (int i = 0; i < estrellas; i++)
        {
            stars[i].enabled = true; // Activa la estrella
        }
    }
    #endregion
}
