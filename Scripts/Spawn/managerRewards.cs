using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que gestiona las recompensas visuales y su activación basada en los niveles completados.
public class managerRewards: MonoBehaviour {
  /// Array que contiene los objetos de recompensa.
  /// Las recompensas se activarán como resultado de los niveles completados.
  public GameObject[] rewards;

  /// Se ejecuta cada frame para aplicar una rotación lenta a los objetos de recompensa, dándoles un efecto visual atractivo.
  void Update() {
    /// Recorre cada objeto de recompensa y aplica una rotación lenta alrededor del eje Y.
    for (int i = 0; i < rewards.Length; i++) {
      /// Rotación en el eje Y para un efecto visual dinámico.
      rewards[i].transform.Rotate(0, 0.1f, 0);
    }
  }

  /// Activa las recompensas correspondientes a los niveles completados.
  /// <param name="completedLevels"> Array de booleanos que indica qué niveles han sido completados. </param>
  /// <param name="sceneName"> Nombre de la escena actual. </param>
  public void checkReward(bool[] completedLevels, string sceneName) {
    /// Recorre el array de niveles completados y activa las recompensas asociadas.
    for (int i = 0; i < completedLevels.Length; i++) {
      if (completedLevels[i]) {
        /// Activa la recompensa si el nivel correspondiente está marcado como completado.
        rewards[i].SetActive(true);
      }
    }
  }
}
