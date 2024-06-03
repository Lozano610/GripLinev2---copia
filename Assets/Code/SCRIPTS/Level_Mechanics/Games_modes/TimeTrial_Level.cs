using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using JetBrains.Annotations;

public class TimeTrial_Level : MonoBehaviour
{
    #region Variables
    public TMP_Text timerText;
    public TMP_Text CoinsFinish;
    private float startTime;
    public bool raceStarted = false;
    public bool lapCompleted = false;
    public SplineAnimator PlayerController;
    public float countdownDuration;
    public UnityEvent onEventoLanzado;
    public AudioSource Audio_Level;
    public GameObject Finish_Screen;
    public GameObject HUD_Screen;
    public Image[] stars;
    public float tiempogold;
    public float tiemposilver;
    public float tiempobronze;
    public int nivelActualIndex;
    //public ManagerSavingObjects managerSavingObjects;
    #endregion
    #region Unity Methods
    private void Start()
    {
        StartCoroutine(StartCountdown());
        Finish_Screen.SetActive(false);
        //startTime = Time.time;
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
                lapCompleted = true;
                raceStarted = false;
                PlayerController.PlayerEnabled = false;
                //MostrarFinCarrera(true, int);
                Finish_Screen.SetActive(true);
                //AsignarEstrellas(posicion.posicionJugador);
                HUD_Screen.SetActive(false);
                AsignarEstrellas();
                Debug.Log("Has acabado la contrarreloj");
                if (MenuManager.Singleton.SceneComplete < nivelActualIndex)
                {
                    MenuManager.Singleton.SceneComplete = nivelActualIndex;
                }
                ManagerSavingObjects.Singleton.Guardar();
            }

        }
    }
    void AsignarEstrellas()
    {
        tiempogold = Time.time - startTime;
        tiemposilver = Time.time - startTime;
        tiempobronze = Time.time - startTime;
        int estrellas = 0;
        if (tiempogold <= 40)
        {
            estrellas = 3;
            Debug.Log("PASO 1000 MONEDAS");
            Coins.TotalCoins += 1000;
            CoinsFinish.text = "1000GL";
        }
        if (tiemposilver <= 45 && tiemposilver > 40)
        {
            estrellas = 2;
            Coins.TotalCoins += 500;
            CoinsFinish.text = "500GL";
        }
        if (tiempobronze <= 50 && tiempobronze > 45)
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
