using System.Collections;
using UnityEngine;

public class DeactivateParticleAndWin : MonoBehaviour
{
    // Referencia al sistema de partículas que se desactiva
    public GameObject particleSystem;
    // Referencia al nuevo AudioSource que reproducirá el sonido
    public AudioSource audioSource;
    // AudioClip para el nuevo sonido
    public AudioClip audioClip;
    // Posición a la que se teletransportará el GameObject
    public Vector3 targetPosition;
    public GameObject portal;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra al collider es el jugador
        if (other.CompareTag("Player"))
        {
            // Desactivar el sistema de partículas
            if (particleSystem != null)
            {
                particleSystem.SetActive(false);
            }

            // Reproducir el nuevo audio al 50% de volumen
            GameObject audioSourceGameObject = audioSource.gameObject;
            audioSourceGameObject.SetActive(true);
            if (audioSource != null && audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.volume = 0.5f; // Ajusta el volumen al 50%
                audioSource.Play();
                //subrutina para activar un gameObject en cuanto se acabe el audio
                StartCoroutine(ActivateGameObject(audioSourceGameObject));
            }
            audioSourceGameObject.transform.position = targetPosition;
        }
    }

    IEnumerator ActivateGameObject(GameObject audioSourceGameObject)
    {
        yield return new WaitForSeconds(audioClip.length);
        portal.SetActive(true);
        audioSource.gameObject.SetActive(false);
    }
}
