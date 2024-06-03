using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Collision : MonoBehaviour
{
    #region Variables
    //Limit maxSpeed
    public float CurveVelocity = 0.4f;
    public SplineAnimator PlayerController;
    public float fuerzaMaxima = 500f;
    public float fuerzaMinima = 250f;
    public float tiempoDeEsperaRespawn = 3f;
    public float Tor_Min = 50;
    public float Tor_Max = 100;
    public Transform player;
    public Transform origenPlayer;
    public Transform origenR1;
    public Transform origenR2;
    public Transform origenR3;
    public bool IsCrashed;
    #endregion
    #region Own Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (PlayerController.speed > CurveVelocity)
            {
                PlayerController.enabled = false;
                Debug.Log("Lo activo");
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
                float fuerzaAleatoria = Random.Range(fuerzaMinima, fuerzaMaxima);
                float RandomTorque = Random.Range(Tor_Min, Tor_Max);
                Debug.Log("Force" + fuerzaAleatoria);
                // Aplicar fuerza adicional en la dirección actual del jugador
                rb.AddForce(other.transform.right * fuerzaAleatoria, ForceMode.Impulse);
                rb.AddTorque(other.transform.right * RandomTorque);
                Debug.Log("Torque" + RandomTorque);
                StartCoroutine(WaitingTime());
               // Invoke("RespawnJugador", tiempoDeEsperaRespawn);
                if (IsCrashed == true)
                {
                    FindFirstObjectByType<Survivol_Level>().LoseGame();
                }
            }
        }
        else if (other.CompareTag("Rival"))
        {
            SlareVelocityIA RivalController = other.GetComponent<SlareVelocityIA>();
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (RivalController.speed > CurveVelocity)
            {
                RivalController.enabled = false;
                Debug.Log("RivalSale");
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
                float fuerzaAleatoria = Random.Range(fuerzaMinima, fuerzaMaxima);
                float RandomTorque = Random.Range(Tor_Min, Tor_Max);
                Debug.Log("ForceRival" + fuerzaAleatoria);
                // Aplicar fuerza adicional en la dirección actual del jugador
                rb.AddForce(other.transform.right * fuerzaAleatoria, ForceMode.Impulse);
                rb.AddTorque(other.transform.right * RandomTorque);
                Debug.Log("TorqueRival" + RandomTorque);
                StartCoroutine(RivalWaitTime(rb,RivalController));
                // Invoke("RespawnJugador", tiempoDeEsperaRespawn);
                
            }
        }
    }
    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(tiempoDeEsperaRespawn);
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        player.transform.position = origenPlayer.position;
        player.transform.rotation = origenPlayer.rotation;
        PlayerController.enabled = true;
    }
    IEnumerator RivalWaitTime(Rigidbody rb, SlareVelocityIA RivalController)
    {
        yield return new WaitForSeconds(tiempoDeEsperaRespawn);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        if (RivalController.RivalNumber == 1)
        {
            RivalController.transform.position = origenR1.position;
            RivalController.transform.rotation = origenR1.rotation;
            RivalController.enabled = true;
        }
        if (RivalController.RivalNumber == 2)
        {
            RivalController.transform.position = origenR2.position;
            RivalController.transform.rotation = origenR2.rotation;
            RivalController.enabled = true;
        }
        if (RivalController.RivalNumber == 3)
        {
            RivalController.transform.position = origenR3.position;
            RivalController.transform.rotation = origenR3.rotation;
            RivalController.enabled = true;
        }

    }
    #endregion
}
