using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalCheckpointTeleporter : CheckpointTeleporter {
    // Cuando el jugador entre en el área de teletransporte, se activa
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && teleportArea != null)
        {
            teleportArea.SetActive(true);
        }
    }

    // Cuando el jugador sale del área de teletransporte, se desactiva
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && teleportArea != null)
        {
            teleportArea.SetActive(false);
        }
    }
}

