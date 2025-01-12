using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase para gestionar los checkpoints del juego.
/// Guarda la posición del último checkpoint y permite teletransportar al jugador a esa posición en caso de caer al agua.
public class CheckPointManager: MonoBehaviour {
  /// Posición del último checkpoint alcanzado.
  /// Es estática para mantener la misma posición a través de distintas instancias de esta clase.
  private static Vector3 lastCheckpointPosition;

  /// Referencia al objeto jugador que será teletransportado.
  public GameObject player;

  /// Tag para identificar el agua.
  public string waterTag = "Water";

  /// Corrutina para desactivar el collider del checkpoint después de un breve retraso.
  /// Esto evita que el checkpoint sea activado repetidamente.
  /// <param name="collider"> El collider del checkpoint a desactivar. </param>
  /// <returns> Un enumerador para la corrutina. </returns>
  private IEnumerator DisableCollider(Collider collider) {
    yield return new WaitForSeconds(0.5f);
    if (collider != null) {
      collider.enabled = false;
    }
  }

  /// Método llamado cuando este objeto entra en contacto con un trigger.
  /// Actualiza la posición del último checkpoint si se contacta con un objeto con el tag "Checkpoint".
  /// <param name="other"> El collider con el que se ha detectado la colisión. </param>
  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Checkpoint")) {
      /// Actualiza la posición del último checkpoint alcanzado.
      lastCheckpointPosition = transform.position;

      /// Desactiva el collider del checkpoint después de un retraso.
      StartCoroutine(DisableCollider(other));
    }
  }

  /// Método llamado cuando este objeto colisiona con otro.
  /// Si el jugador colisiona con un objeto con el tag "Water", se teletransporta al último checkpoint.
  /// <param name="collision"> Información sobre la colisión detectada. </param>
  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag(waterTag)) {
      TeleportToCheckpoint();
    }
  }

  /// Teletransporta al jugador a la posición del último checkpoint alcanzado.
  private void TeleportToCheckpoint() {
    player.transform.position = lastCheckpointPosition;
  }
}
