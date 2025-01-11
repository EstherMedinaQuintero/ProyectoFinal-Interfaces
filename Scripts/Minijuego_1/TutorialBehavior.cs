using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

/// Clase para gestionar el tutorial del juego.
/// Proporciona pasos interactivos para enseñar al jugador cómo jugar, incluyendo
/// la interacción con armas y objetivos, y utiliza texto y audio para la guía.
public class TutorialBehavior: MonoBehaviour {
  /// Referencia al componente de texto de la UI para mostrar las instrucciones del tutorial.
  [SerializeField] private TextMeshPro tutorialText;

  /// Bandera para verificar si el jugador se ha movido.
  private bool hasMoved = false;

  /// Bandera para verificar si el jugador ha recogido un arma.
  private bool hasPickedUpWeapon = false;

  /// Bandera para verificar si el jugador ha disparado.
  private bool hasShot = false;

  /// Clips de audio que guían al jugador a lo largo del tutorial.
  public AudioClip welcomeClip;
  public AudioClip grabWeaponClip;
  public AudioClip shootClip;
  public AudioClip endClip;

  /// Referencia al hada (fairy) que reproduce los clips de audio.
  public GameObject fairy;

  /// Inicia la secuencia del tutorial al comenzar el juego.
  void Start() {
    StartCoroutine(TutorialSequence());
  }

  /// Secuencia del tutorial que guía al jugador a través de pasos interactivos.
  private IEnumerator TutorialSequence() {
    /// Paso de bienvenida
    tutorialText.text = "¡Bienvenido a Prop Hunt!";
    fairy.GetComponent<AudioSource>().clip = welcomeClip;
    fairy.GetComponent<AudioSource>().Play();
    yield return new WaitForSeconds(3f);

    /// Paso 1: Coger un arma
    tutorialText.text = "Usa el gatillo lateral del mando derecho para coger un arma.";
    fairy.GetComponent<AudioSource>().clip = grabWeaponClip;
    fairy.GetComponent<AudioSource>().Play();
    while (!hasPickedUpWeapon) {
      yield return null; /// Espera a que el jugador coja un arma
    }

    /// Paso 2: Disparar
    tutorialText.text = "Ahora, usa el gatillo trasero del mando derecho para disparar.";
    fairy.GetComponent<AudioSource>().clip = shootClip;
    fairy.GetComponent<AudioSource>().Play();
    while (!hasShot) {
      yield return null; /// Espera a que el jugador dispare
    }

    /// Paso final: Explicación del juego
    tutorialText.text = "¡Bien hecho! Ahora busca y dispara a los 5 objetos escondidos. ¡Atento a las pistas!";
    fairy.GetComponent<AudioSource>().clip = endClip;
    fairy.GetComponent<AudioSource>().Play();
    yield return new WaitForSeconds(5f);

    /// Activar el juego principal
    FindObjectOfType<GameManager>().enabled = true;
    FindObjectOfType<PropSelector>().enabled = true;

    /// Limpiar texto del tutorial
    tutorialText.text = "";
  }

  /// Marca que el jugador se ha movido.
  public void PlayerMoved() {
    hasMoved = true;
  }

  /// Marca que el jugador ha recogido un arma.
  /// Este método puede ser llamado por otros sistemas, como el manejo de armas.
  public void WeaponPickedUp() {
    hasPickedUpWeapon = true;
  }

  /// Marca que el jugador ha disparado.
  /// Este método puede ser llamado desde las armas cuando el jugador dispara por primera vez.
  public void PlayerShot() {
    hasShot = true;
  }
}
