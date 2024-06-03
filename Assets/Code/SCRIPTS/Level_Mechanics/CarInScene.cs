using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInScene : MonoBehaviour
{
    public GameObject[] cars;
    //public ManagerSavingObjects savingindex;
    // Start is called before the first frame update
    void Start()
    {
        //savingindex = GetComponent<ManagerSavingObjects>();
        ManagerSavingObjects.Singleton.Cargar();
        cars[ManagerSavingObjects.Singleton.save_indexcar].SetActive(true);
        //Debug.Log(PlayerPrefs.GetInt("carIndex"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
