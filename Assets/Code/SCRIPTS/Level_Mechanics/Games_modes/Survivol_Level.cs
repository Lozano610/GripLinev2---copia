using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using JetBrains.Annotations;
using GooglePlayGames;
using static UnityEngine.Timeline.TimelineAsset;
//using static System.Net.Mime.MediaTypeNames;

public class Survivol_Level : MonoBehaviour
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
    public GameObject Lose_Scene;
    public float loseWaiting = 1f;
    public int nivelActualIndex;
    public float tiempogold;
    public float tiemposilver;
    public float tiempobronze;
    public SQLITE_conxx sqlite;
    //public ManagerSavingObjects managerSavingObjects;
    private MenuManager manager;
    #endregion
    #region UnityMethods
    private void Start()
    {
        StartCoroutine(StartCountdown());
        Finish_Screen.SetActive(false);
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
    #region OwnMethods
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
    public void LoseGame()
    {
        StartCoroutine (WaitingLose());
    }
    IEnumerator WaitingLose()
    {
        yield return new WaitForSeconds(loseWaiting);
        Lose_Scene.SetActive(true);
        Time.timeScale = 0;
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
                if (manager.SceneComplete < nivelActualIndex)
                {
                    manager.SceneComplete = nivelActualIndex;
                }
            }
            Debug.Log("MONEDAS" + Coins.TotalCoins);
            //managerSavingObjects.save_coins = Coins.TotalCoins;
            
            //managerSavingObjects.save_timertext = timerText.text;
            ManagerSavingObjects.Singleton.Guardar();

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
            Coins.TotalCoins += 1000;
            CoinsFinish.text = "1000GL";
            
        }
        if (tiemposilver <= 45 && tiemposilver >40)
        {
            estrellas = 2;
            Coins.TotalCoins += 500;
            CoinsFinish.text = "500GL";
           
        }
        if (tiempobronze <= 50 && tiempobronze >45)
        {
            estrellas = 1;
            Coins.TotalCoins += 200;
            CoinsFinish.text = "200GL";
        }
        for (int i = 0; i < estrellas; i++)
        {
            stars[i].enabled = true; // Activa la estrella
        }
        //NivelNascar
        if(nivelActualIndex == 3) {
            PlayGamesPlatform.Instance.ReportScore((long)tiempobronze * 1000, GPGSIds.leaderboard_RankingNascar, (bool success) => {
                // Handle success or failure
            });
            sqlite.Inserta("RankingNascar", Time.time - startTime);
            sqlite.Selecccionar("RankingNascar");


        }
        //NivelBrittish
        if (nivelActualIndex == 7)
        {
            PlayGamesPlatform.Instance.ReportScore((long)tiempobronze * 1000, GPGSIds.leaderboard_RankingBrittish, (bool success) => {
            // Handle success or failure
        });
            sqlite.Inserta("RankingBreifford", Time.time - startTime);
            sqlite.Selecccionar("RankingBreifford");

        }
        //NivelSenach
        if (nivelActualIndex == 11)
        {
            PlayGamesPlatform.Instance.ReportScore((long)tiempobronze * 1000, GPGSIds.leaderboard_RankingSenach, (bool success) => {
                // Handle success or failure
            });
            sqlite.Inserta("RankingSenach", Time.time - startTime);
            sqlite.Selecccionar("RankingSenach");

        }
    }
    public void MostrarLeaderboard()
    {
        if (nivelActualIndex == 3)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_RankingNascar);
        }
        //NivelBrittish
        if (nivelActualIndex == 7)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_RankingBrittish);
        }
        //NivelSenach
        if (nivelActualIndex == 11)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_RankingSenach);
        }
        
    }
    #endregion
}