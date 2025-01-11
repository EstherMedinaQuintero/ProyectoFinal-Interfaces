using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
  private static Vector3 lastCheckpointPosition;  // Posición del último checkpoint.
  public GameObject player;  // Referencia al jugador.
  public string waterTag = "Water";  // Tag para identificar el agua.
    
  private IEnumerator DisableCollider(Collider collider) {
    yield return new WaitForSeconds(0.5f);
    if (collider != null) {
      collider.enabled = false;
    }
  }
  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Checkpoint")) {
      lastCheckpointPosition = transform.position;
      StartCoroutine(DisableCollider(other));
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag(waterTag)) {
      TeleportToCheckpoint();
    }
  }

    // Método para teletransportar al jugador al último checkpoint
  private void TeleportToCheckpoint() {
    player.transform.position = lastCheckpointPosition;
  }

}
