using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class MenuManager : MonoBehaviour
{
    #region Variables
    public GameObject MenuCamera;
    public GameObject[] cars;
    public GameObject[] paintcars;
    public Button next, buynext;
    public Button prev, prevnext;
    public int car_index;
    public int Selectedcar_index;
    public int circuit_index;
    int gamemode_index;
    public string[] sceneName;
    public int SceneComplete;
    public List<int> CarsList;
    public List<int> CarsComprados;
    public TMP_Text buy_text;
    public GameObject candado;
    //public ManagerSavingObjects loadSavingObjects;
    //public int debugsceneindex = 999;
    //esto son las estrellas de cada nivel
    public int[] PassLevel;
    //private static MenuManager instance;
    #endregion
    #region Events
    public static MenuManager Singleton
    {
        //El get nos sirve para obtener la información del Singleton
        get
        {
            //Comprobamos primero que la instancia esté vacía
            if (instance == null)
            {
                //Rellenamos la referencia del Singleton
                instance = FindAnyObjectByType(typeof(MenuManager)) as MenuManager;
            }
            //Nos devuelve la información de la instancia
            return instance;
        }
        set { 
            instance = value; 
        }
    }
    private static MenuManager instance;
    #endregion
    #region UnityMethods
    private void Start()
    {
        ManagerSavingObjects.Singleton.Cargar();
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            paintcars[i].SetActive(false);
            cars[car_index].SetActive(true);
            paintcars[car_index].SetActive(true);
        }
    }
    private void Update()
    {
        //loadSavingObjects.coins.CoinsNumber.text = Coins.TotalCoins.ToString();
            Debug.Log(car_index);
        if(car_index >= 1)
        {
            next.interactable = false;
            buynext.interactable=false;
        }
        else
        {
            next.interactable = true;
            buynext.interactable=true;
        }
        if(car_index <= 0)
        {
            prev.interactable = false;
            prevnext.interactable=false;
        }
        else
        {
            prev.interactable= true;
            prevnext.interactable= true;
        }
        //if(debugsceneindex != 999)
        //{
        //    SceneComplete = debugsceneindex;
        //}
    }
    #endregion
    #region OwnMethods
    public void Next()
    {
        car_index++;
        //PlayerPrefs.SetInt("carIndex", car_index);
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            paintcars[i].SetActive(false);
            cars[car_index].SetActive(true);
            paintcars[car_index].SetActive(true);
        }
        Compradecoches();
    }
    public void Prev()
    {
        car_index--;
        //PlayerPrefs.SetInt("carIndex", car_index);
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
            paintcars[i].SetActive(false);
            cars[car_index].SetActive(true);
            paintcars[car_index].SetActive(true);
        }
        Compradecoches();
    }
    public void CircuitIndexButton(int CI)
    {
        circuit_index = CI;
    }
    public void GameModeIndexButton (int GMI)
    {
        gamemode_index = GMI;
    }
    void Compradecoches()
    {
        if (car_index != 0)
        {
            if (CarsComprados.Contains(car_index) == false)
            {
                buy_text.text = CarsList[car_index - 1] + "GL";
                candado.SetActive(true);
            }
            else
            {
                candado.SetActive(false);
                buy_text.text = "Equipar";
            }
        }
        else
        {
            candado.SetActive(false);
            buy_text.text = "Equipar";
        }
    }
    public void UnlockCars()
    {
        if(car_index == 0) 
        {
            Selectedcar_index = car_index;
            ManagerSavingObjects.Singleton.Guardar();
            return;
        }
        if (CarsComprados.Contains(car_index) == false)
        {
            if (Coins.TotalCoins >= CarsList[car_index - 1])
            {
                CarsComprados.Add(car_index);
                Compradecoches();
                //Debug.Log("ESTA COMPRAO");
                Coins.TotalCoins -= CarsList[car_index - 1];
            }
        }
        else
        {
            Selectedcar_index = car_index;
            ManagerSavingObjects.Singleton.Guardar();
        }
        //Debug.Log(Coins.TotalCoins);
    }
    public void SceneLoad()
    {
        //loadSavingObjects.Guardar();
        Application.LoadLevel(sceneName[circuit_index + gamemode_index]);

    }
    #endregion
}
