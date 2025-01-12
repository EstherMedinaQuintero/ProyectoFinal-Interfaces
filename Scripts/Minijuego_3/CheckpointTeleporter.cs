using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// Clase que gestiona un checkpoint con funcionalidad de teletransporte.
/// Activa una zona de teletransporte al alcanzar el checkpoint.
public class CheckpointTeleporter: MonoBehaviour {
  /// Referencia al área de teletransporte asociada a este checkpoint.
  public GameObject teleportArea;

  /// Inicializa el estado del checkpoint al inicio.
  /// La zona de teletransporte está desactivada por defecto.
  private void Start() {
    /// Desactiva la zona de teletransporte al inicio
    if (teleportArea != null) {
      teleportArea.SetActive(false);
    }
  }

  /// Método llamado cuando un objeto entra en el collider del checkpoint.
  /// Si el objeto tiene el tag "Player", activa la zona de teletransporte asociada.
  /// <param name="other"> El collider del objeto que entró. </param>
  private void OnTriggerEnter(Collider other) {
    /// Verifica si el objeto que entra tiene el tag "Player"
    if (other.CompareTag("Player")) {
      /// Activa la zona de teletransporte si está configurada
      if (teleportArea != null) {
        teleportArea.SetActive(true); // Activa la zona de teletransporte
      }
    }
  }
}
