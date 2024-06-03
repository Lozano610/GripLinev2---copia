using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using JetBrains.Annotations;

public class Vs_IA_Level : MonoBehaviour
{
    #region Variables
    public TMP_Text Vueltas;
    public TMP_Text CoinsFinish;
    public int VueltasTotales = 3;
    public int VueltasCompletadas = 0;
    public SplineAnimator PlayerController;
    public float countdownDuration;
    public UnityEvent onEventoLanzado;
    public AudioSource Audio_Level;
    public GameObject Finish_Screen;
    public GameObject HUD_Screen;
    public Image[] stars;
    public aversifunciona posicion;
    public int nivelActualIndex;
    //public ManagerSavingObjects managerSavingObjects;
    #endregion
    #region Unity Methods
    private void Start()
    {
        StartCoroutine(StartCountdown());
        Finish_Screen.SetActive(false);
    }
    #endregion
    #region Own Methods
    IEnumerator StartCountdown()
    {
        WaitForSeconds wait = new WaitForSeconds(countdownDuration);
        yield return wait;
        onEventoLanzado.Invoke();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VueltasCompletadas++;
            if(VueltasCompletadas >= VueltasTotales)
            {
                Vueltas.text = "Vueltas 3/3";
                PlayerController.PlayerEnabled = false;
                //MostrarFinCarrera(true, int);
                Finish_Screen.SetActive(true);
                AsignarEstrellas(posicion.posicionJugador);
                HUD_Screen.SetActive(false);
                Debug.Log("Has acabado la carrera");
                //He conseguido superar el nivel
                if (MenuManager.Singleton.SceneComplete < nivelActualIndex)
                {
                    MenuManager.Singleton.SceneComplete = nivelActualIndex;
                }
                ManagerSavingObjects.Singleton.Guardar();

            }
            else
            {
                Vueltas.text = "Vuelta " + VueltasCompletadas + " /3";
            }
        }
    }
    void AsignarEstrellas(int posicion)
    {

        switch (posicion)
        {

            case 1: posicion = 3;
                
                if (posicion == 3)
                {
                    Debug.Log("PASO 1000 MONEDAS1");
                    Coins.TotalCoins += 1000;
                    CoinsFinish.text = "1000GL";
                }
                break;
            case 3: posicion = 1;
                if (posicion == 1)
                {
                    Debug.Log("PASO 200 MONEDAS");
                    Coins.TotalCoins += 200;
                    CoinsFinish.text = "200GL";
                }
                break;
        }
        if (posicion == 2)
        {
            Debug.Log("PASO 500 MONEDAS");
            Coins.TotalCoins += 500;
            CoinsFinish.text = "500GL";
        }
        if (posicion < 4)
        { 
            for (int i = 0; i < 3; i++)
            {
                
                if (i < posicion)
                {
                    stars[i].enabled = true; // Activa la estrella
                }
            }
        }  
    }
    #endregion
}
