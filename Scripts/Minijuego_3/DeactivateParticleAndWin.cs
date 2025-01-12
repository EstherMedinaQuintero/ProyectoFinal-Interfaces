using System.Collections;
using UnityEngine;

/// Clase que gestiona la desactivación de un sistema de partículas, la reproducción de un sonido
/// y el teletransporte de un objeto cuando el jugador entra en el área del trigger.
public class DeactivateParticleAndWin: MonoBehaviour {
  /// Referencia al sistema de partículas que será desactivado.
  public GameObject particleSystem;

  /// Referencia al componente AudioSource que reproducirá el sonido.
  public AudioSource audioSource;

  /// Clip de audio que será reproducido al activar el evento.
  public AudioClip audioClip;

  /// Posición a la que será teletransportado este GameObject.
  public Vector3 targetPosition;

  /// Método llamado cuando otro objeto entra en el área del trigger.
  /// <param name="other"> El collider del objeto que ha entrado en el trigger. </param>
  private void OnTriggerEnter(Collider other) {
    /// Verifica si el objeto que entra al trigger tiene el tag "Player".
    if (other.CompareTag("Player")) {
      /// Desactiva el sistema de partículas si está asignado.
      if (particleSystem != null) {
        particleSystem.SetActive(false);
      }

      /// Reproduce el audio con el volumen ajustado al 50%.
      if (audioSource != null && audioClip != null) {
        audioSource.clip = audioClip;
        audioSource.volume = 0.5f; /// Ajusta el volumen al 50%.
        audioSource.Play();
      }

      /// Teletransporta este GameObject a la posición especificada.
      transform.position = targetPosition;
    }
  }
}
