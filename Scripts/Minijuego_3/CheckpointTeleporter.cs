using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckpointTeleporter : MonoBehaviour
{
    // Referencia al área de teletransporte asociada a este checkpoint
    public GameObject teleportArea;

    private void Start()
    {
        // Desactivar la zona de teletransporte al inicio
        if (teleportArea != null)
        {
            teleportArea.SetActive(false);
        }
    }

    // Cuando el jugador entre en el collider del checkpoint
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra al collider tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Activar la zona de teletransporte (suponiendo que el área tiene un componente XR Teleportation Area)
            if (teleportArea != null)
            {
                teleportArea.SetActive(true);  // Activamos la zona de teletransporte
            }
        }
    }

}

