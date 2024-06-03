using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakingIA : MonoBehaviour
{
    //public float V_minR1 = 0.01f;
    //public float V_minR2;
    //public float V_minR3;
    //public float V_maxR1;
    //public float V_maxR2;
    //public float V_maxR3;
    public FlywightEnemy Enemy_velocity1;
    public FlywightEnemy Enemy_velocity2;
    public FlywightEnemy Enemy_velocity3;
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el collider que entró es el del jugador
        if (other.CompareTag("Rival"))
        {
            // Reduce la velocidad del rival
            SlareVelocityIA Car = other.GetComponent<SlareVelocityIA>();
            switch (Car.Types)
            {
                case SlareVelocityIA.RivalType.Slow:
                    Car.speed = Random.Range(Enemy_velocity1.V_min, Enemy_velocity1.V_max);
                    break;
                case SlareVelocityIA.RivalType.Normal:
                    Car.speed = Random.Range(Enemy_velocity2.V_min, Enemy_velocity2.V_max);
                    break;
                case SlareVelocityIA.RivalType.Crazy:
                    Car.speed = Random.Range(Enemy_velocity3.V_min, Enemy_velocity3.V_max);
                    break;
            }
        }
    }
    // Se llama cuando un collider sale del trigger
    private void OnTriggerExit(Collider other)
    {
        // Verifica si el collider que salió es el del jugador
        if (other.CompareTag("Rival"))
        {
            // Restaura la velocidad normal del rival
            other.GetComponent<SlareVelocityIA>().ResetSpeed();
        }
    }
}
