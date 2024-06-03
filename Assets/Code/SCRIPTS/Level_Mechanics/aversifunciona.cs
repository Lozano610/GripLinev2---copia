using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class aversifunciona : MonoBehaviour
{
    #region Variables
    public SplineAnimator PlayerSPline;
    public SplineAnimator Rival1Spline;
    public SplineAnimator Rival2Spline;
    public SplineAnimator Rival3Spline;
    public float distanciaJugador;
    public float distanciaRival1;
    public float distanciaRival2;
    public float distanciaRival3;
    public TMP_Text Pos_text;
    public int posicionJugador;
    #endregion
    #region Unity Methods
    void Update()
    {
        distanciaJugador = PlayerSPline.passedTime;
        distanciaRival1 = Rival1Spline.passedTime;
        distanciaRival2 = Rival2Spline.passedTime;
        distanciaRival3 = Rival3Spline.passedTime;
        posicionJugador = DeterminarPosicion();
        Pos_text.text =posicionJugador.ToString() + "/4";
        Debug.Log("Posición del jugador: " + posicionJugador);
    }
    #endregion
    #region Own Methods
    int DeterminarPosicion()
    {
        // Crear un array de distancias y asignar las distancias de los rivales y el jugador
        float[] distancias = { distanciaRival1, distanciaRival2, distanciaRival3, distanciaJugador };
        // Ordenar el array en orden descendente para que la distancia más larga esté en la posición 0
        System.Array.Sort(distancias);
        System.Array.Reverse(distancias);
        // Encontrar la posición del jugador en el array ordenado
        int posicionJugador = System.Array.IndexOf(distancias, distanciaJugador) + 1;
        return posicionJugador;
    }
    #endregion
}