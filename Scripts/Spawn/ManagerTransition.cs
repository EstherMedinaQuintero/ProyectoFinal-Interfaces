using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Clase que gestiona las transiciones entre escenas, la persistencia de datos de progreso y el control de estados como niveles completados.
public class ManagerTransition: MonoBehaviour {
  /// Animador responsable de las transiciones de fade in/out.
  [SerializeField] private Animator fadeAnimator;

  /// Array para rastrear los niveles completados.
  public bool[] completedLevels = new bool[4];

  /// Indica si el jugador está jugando por primera vez.
  public bool FirstTime = true;

  /// Instancia estática para implementar el patrón Singleton.
  private static ManagerTransition instance;

  /// Referencia al jugador y su posición.
  public GameObject player;
  private Vector3 playerPosition;

  /// Método Awake para implementar el patrón Singleton y evitar duplicados.
  void Awake() {
    /// Si ya existe una instancia, destruye esta para evitar duplicados
    if (instance != null && instance != this) {
      Destroy(this.gameObject);
      return;
    }

    /// Asigna esta instancia como la única
    instance = this;

    /// Marca este objeto para que no sea destruido al cambiar de escena
    DontDestroyOnLoad(gameObject);

    /// Inicializa el estado de los niveles como no completados
    for (int i = 0; i < completedLevels.Length; i++) {
      completedLevels[i] = false;
    }
  }

  /// Inicia la transición de escena con fade in/out y carga la escena especificada.
  /// <param name="sceneToLoad"> Nombre de la escena a cargar. </param>
  /// <param name="levelIndex"> Índice del nivel que se está completando. </param>
  public void StartSceneTransition(string sceneToLoad, int levelIndex) {
    /// Marca que ya no es la primera vez que se juega
    FirstTime = false;

    /// Lanza la corrutina para manejar la transición de escena
    StartCoroutine(TransitionCoroutine(sceneToLoad, levelIndex));
  }

  /// Corrutina que maneja la transición de escena con efectos de fade y lógica específica para la escena "home".
  private IEnumerator TransitionCoroutine(string sceneToLoad, int levelIndex) {
    /// Reproduce la animación de fade out
    fadeAnimator.Play("fadeout");

    /// Espera a que termine la animación de fade out
    yield return new WaitForSeconds(1.0f);

    /// Carga la nueva escena
    SceneManager.LoadScene(sceneToLoad);

    /// Lógica adicional para la escena "home"
    if (sceneToLoad == "home") {
      /// Espera un frame para asegurar que la nueva escena se haya cargado
      yield return new WaitForSeconds(1.0f);

      /// Actualiza el estado de recompensas
      GameObject rewardController = GameObject.Find("ControllerReward");
      if (rewardController != null) {
        managerRewards rewardManager = rewardController.GetComponent<managerRewards>();
        if (rewardManager != null) {
          completedLevels[levelIndex] = true;
          rewardManager.checkReward(completedLevels, sceneToLoad);
        } else {
          Debug.LogError("No se encontró el script managerRewards en el objeto ControllerReward.");
        }
      } else {
        Debug.LogError("No se encontró el objeto ControllerReward en la escena.");
      }

      /// Actualiza el estado de los portales
      GameObject portalController = GameObject.Find("PortalManager");
      if (portalController != null) {
        portalManager portalManager = portalController.GetComponent<portalManager>();
        if (portalManager != null) {
          completedLevels[levelIndex] = true;
          portalManager.checkPortal(completedLevels);
        } else {
          Debug.LogError("No se encontró el script portalManager en el objeto ControllerPortal.");
        }
      } else {
        Debug.LogError("No se encontró el objeto ControllerPortal en la escena.");
      }
    }

    /// Espera otro segundo antes de reproducir el fade in
    yield return new WaitForSeconds(1.0f);

    /// Reproduce la animación de fade in
    fadeAnimator.Play("fadeIn");

    /// Espera a que termine la animación de fade in
    yield return new WaitForSeconds(1.0f);
  }

  /// Marca un nivel como completado.
  /// <param name="levelIndex"> Índice del nivel completado. </param>
  public void SetLevelCompleted(int levelIndex) {
    completedLevels[levelIndex] = true;
  }

  /// Guarda la posición actual del jugador.
  /// <returns> Devuelve true si la posición se guardó correctamente. </returns>
  public bool SavePosition() {
    playerPosition = player.transform.position;
    return true;
  }

  /// Carga la posición previamente guardada del jugador.
  /// <returns> La posición guardada del jugador. </returns>
  public Vector3 LoadPosition() {
    return playerPosition;
  }
}
