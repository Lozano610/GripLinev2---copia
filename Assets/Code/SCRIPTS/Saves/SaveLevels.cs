using UnityEngine;

public class SaveLevels : MonoBehaviour
{
    //public MenuManager mimanager;
    public GameObject Boton1;
    public GameObject Boton2;
    public GameObject Boton3;
    public GameObject Boton4;
   
    // Start is called before the first frame update
    void Update()
    {
        
           if((MenuManager.Singleton.circuit_index - 1) > 0){

        if (MenuManager.Singleton.PassLevel[MenuManager.Singleton.circuit_index - 1] > 0 )
        {
            Boton1.SetActive(true);
            Boton2.SetActive(false);
            Boton3.SetActive(false);
            Boton4.SetActive(false);
            }
            else{
                Boton1.SetActive(false);
                Boton2.SetActive(false);
                Boton3.SetActive(false);
                Boton4.SetActive(false);
            }

        }

        if (MenuManager.Singleton.PassLevel[MenuManager.Singleton.circuit_index + 0] > 0 )
        {
            Boton1.SetActive(true);
            Boton2.SetActive(true);
            Boton3.SetActive(false);
            Boton4.SetActive(false);
        }
        if (MenuManager.Singleton.PassLevel[MenuManager.Singleton.circuit_index + 1] > 0)
        {
            Boton1.SetActive(true);
            Boton2.SetActive(true);
            Boton3.SetActive(true);
            Boton4.SetActive(false);
        }
        if (MenuManager.Singleton.PassLevel[MenuManager.Singleton.circuit_index + 2] > 0)
        {
            Boton1.SetActive(true);
            Boton2.SetActive(true);
            Boton3.SetActive(true);
            Boton4.SetActive(true);
        }
        //if (mimanager.PassLevel[mimanager.circuit_index + 3] > 0)
        //{
        //    Boton1.SetActive(true);
        //    Boton2.SetActive(true);
        //    Boton3.SetActive(true);
        //    Boton4.SetActive(true);
        //}


        if (MenuManager.Singleton.circuit_index == 0 && MenuManager.Singleton.PassLevel[0] == 0)
        {
            Boton1.SetActive(true);
            Boton2.SetActive(false);
            Boton3.SetActive(false);
            Boton4.SetActive(false);
        }
    }

    // Update is called once per frame

}