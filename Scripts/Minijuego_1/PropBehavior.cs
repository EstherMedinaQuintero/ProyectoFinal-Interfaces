using System.Collections;
using UnityEngine;

/// Comportamiento de un "prop" que interactúa con el sistema de daño y ofrece pistas visuales y acústicas.
/// Implementa la interfaz ITakeDamage para recibir daño.
public class PropBehavior: MonoBehaviour, ITakeDamage {
  /// Número de golpes restantes antes de que el prop sea destruido.
  private int hitsRemaining = 3;

  /// Referencia al componente Renderer para cambiar el color del prop.
  private Renderer propRenderer;

  /// Color original del material del prop.
  private Color originalColor;

  /// Referencia al GameManager para registrar el estado del prop.
  private GameManager gameManager;

  /// Indica si el prop ya ha sido destruido.
  private bool isDestroyed = false;

  /// Alterna entre mostrar pistas visuales y acústicas.
  private bool giveVisualHint = true;

  /// Fuente de audio para reproducir las pistas acústicas.
  [SerializeField] private AudioSource audioSource;

  /// Sonido que se reproduce como pista acústica.
  [SerializeField] private AudioClip hintSound;

  /// Inicializa las referencias necesarias al componente Renderer y GameManager.
  void Awake() {
    propRenderer = GetComponent<Renderer>();
    originalColor = propRenderer.material.color;

    /// Busca el GameManager en la escena
    gameManager = FindObjectOfType<GameManager>();
    if (gameManager == null) {
      Debug.LogError("No se encontró el GameManager en la escena.");
    }
  }

  /// Inicia la corrutina para proporcionar pistas al jugador.
  void Start() {
    StartCoroutine(HintCoroutine());
  }

  /// Asigna una fuente de audio al prop.
  /// <param name="source"> El AudioSource a asignar. </param>
  public void SetAudioSource(AudioSource source) {
    audioSource = source;
  }

  /// Asigna un clip de sonido para las pistas acústicas.
  /// <param name="sound"> El AudioClip a asignar. </param>
  public void SetHintSound(AudioClip sound) {
    hintSound = sound;
  }

  /// Método de la interfaz ITakeDamage para manejar el daño recibido por el prop.
  /// <param name="weapon"> El arma que causó el daño. </param>
  /// <param name="projectile"> El proyectil que impactó. </param>
  /// <param name="contactPoint"> El punto de impacto. </param>
  public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint) {
    hitsRemaining--;

    /// Muestra un efecto visual al recibir daño.
    StartCoroutine(ShowHitEffect());

    /// Comprueba si el prop debe ser destruido.
    if (hitsRemaining <= 0) {
      isDestroyed = true;
      StopAllCoroutines(); // Detiene las pistas si el prop es destruido.
      Destroy(gameObject);

      /// Notifica al GameManager que el prop ha sido destruido.
      if (gameManager != null) {
        gameManager.RegisterPropDestroyed();
      }
    }
  }

  /// Muestra un efecto visual temporal cuando el prop recibe daño.
  private IEnumerator ShowHitEffect() {
    propRenderer.material.color = Color.red;
    yield return new WaitForSeconds(0.5f);
    propRenderer.material.color = originalColor;
  }

  /// Corrutina que alterna entre pistas visuales y acústicas mientras el prop no esté destruido.
  private IEnumerator HintCoroutine() {
    while (!isDestroyed) {
      yield return new WaitForSeconds(5f); /// Espera 5 segundos entre pistas.
      if (giveVisualHint) {
        StartCoroutine(ShowVisualHint());
        giveVisualHint = false;
      } else {
        PlayAcousticHint();
        giveVisualHint = true;
      }
    }
  }

  /// Muestra una pista visual cambiando temporalmente el color del prop.
  private IEnumerator ShowVisualHint() {
    Debug.Log($"Prop {gameObject.name} muestra una pista visual.");
    propRenderer.material.color = Color.yellow; /// Cambia el color a amarillo.
    yield return new WaitForSeconds(1f); /// La pista visual dura 1 segundo.
    propRenderer.material.color = originalColor;
  }

  /// Reproduce una pista acústica mediante el AudioSource.
  private void PlayAcousticHint() {
    if (audioSource != null) {
      if (hintSound != null) {
        Debug.Log($"Prop {gameObject.name} emite una pista acústica.");
        audioSource.PlayOneShot(hintSound); /// Reproduce el sonido de la pista.
      } else {
        Debug.LogError($"Prop {gameObject.name} no tiene un AudioClip asignado en hintSound.");
      }
    } else {
      Debug.LogError($"Prop {gameObject.name} no tiene un AudioSource asignado.");
    }
  }
}
