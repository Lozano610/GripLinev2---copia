using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Braking : MonoBehaviour
{
    public float velocidadMinimaActivacion = 5f;
    public GameObject efectoVisual;
    public SplineAnimator PlayerController;
    public GameObject Advertising;
    private void Start()
    {
        Advertising.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           if( PlayerController.speed > velocidadMinimaActivacion)
            {
                efectoVisual.SetActive(true);
                Advertising.SetActive(true);
                Debug.Log("He entrado");
            }
            else
            {
                efectoVisual.SetActive(false);
                Advertising.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Desactivar el efecto visual cuando el jugador sale del trigger
        if (other.CompareTag("Player"))
        {
            efectoVisual.SetActive(false);
            Advertising.SetActive (false);
        }
    }
}
