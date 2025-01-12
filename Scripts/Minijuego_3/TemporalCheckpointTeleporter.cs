using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que extiende CheckpointTeleporter para gestionar un teletransporte temporal.
/// La zona de teletransporte se activa al entrar en el área y se desactiva al salir.
public class TemporalCheckpointTeleporter: CheckpointTeleporter {
  /// Activa la zona de teletransporte cuando el jugador entra en el área del checkpoint.
  /// <param name="other"> El collider que entra en el área del trigger. </param>
  private void OnTriggerEnter(Collider other) {
    /// Verifica si el objeto que entra tiene el tag "Player" y si teleportArea está configurado
    if (other.CompareTag("Player") && teleportArea != null) {
      teleportArea.SetActive(true); /// Activa la zona de teletransporte
    }
  }

  /// Desactiva la zona de teletransporte cuando el jugador sale del área del checkpoint.
  /// <param name="other"> El collider que sale del área del trigger. </param>
  private void OnTriggerExit(Collider other) {
    /// Verifica si el objeto que sale tiene el tag "Player" y si teleportArea está configurado
    if (other.CompareTag("Player") && teleportArea != null) {
      teleportArea.SetActive(false); /// Desactiva la zona de teletransporte
    }
  }
}
