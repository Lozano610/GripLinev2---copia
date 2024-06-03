using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsResistence : MonoBehaviour
{
    public Resistence_Level Checkpoints;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Checkpoints.AñadirTiempo();
        }
    }
}
