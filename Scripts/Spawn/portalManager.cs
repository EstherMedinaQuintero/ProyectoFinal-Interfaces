using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que controla todos los portales en la escena.
/// Consulta el ManagerTransition para obtener el estado de los niveles completados
/// y desactiva los portales correspondientes si ya están completados.
public class portalManager: MonoBehaviour {
  /// Array que almacena los portales en la escena.
  public GameObject[] portals;

  /// Array que almacena el estado de los niveles completados (true si completado, false si no).
  public bool[] completedLevels = new bool[4];

  /// Inicializa el estado de los portales en la escena.
  void Start() {
    /// Busca el objeto TransitionManager en la escena.
    GameObject element = GameObject.Find("TransitionManager");
    if (element != null) {
      /// Intenta obtener el componente ManagerTransition del objeto encontrado.
      ManagerTransition managerTransition = element.GetComponent<ManagerTransition>();
      if (managerTransition != null) {
        /// Si se encuentra el script ManagerTransition, copia los niveles completados.
        completedLevels = managerTransition.completedLevels;
      } else {
        Debug.LogError("No se encontró el script ManagerTransition en el objeto ManagerTransition.");
      }
    } else {
      Debug.LogError("No se encontró el objeto ManagerTransition en la escena.");
    }

    /// Configura los portales según los niveles completados.
    for (int i = 0; i < completedLevels.Length; i++) {
      if (completedLevels[i]) {
        /// Desactiva el portal si el nivel está completado.
        portals[i].SetActive(false);
        Debug.Log("Portal " + i + " desactivado");
      } else {
        /// Deja el portal activado si el nivel no está completado.
        Debug.Log("Portal " + i + " activado");
      }
    }
  }

  /// Método para comprobar y actualizar el estado de los portales según los niveles completados.
  /// <param name="completedLevels"> Array de booleanos que indica el estado de los niveles completados. </param>
  public void checkPortal(bool[] completedLevels) {
    /// Recorre el array de niveles completados y actualiza el estado de los portales.
    for (int i = 0; i < completedLevels.Length; i++) {
      if (completedLevels[i]) {
        /// Desactiva el portal si el nivel está completado.
        portals[i].SetActive(false);
        Debug.Log("Portal " + i + " desactivado");
      }
    }
  }
}
