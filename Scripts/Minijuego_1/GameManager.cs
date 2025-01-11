using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// Clase para gestionar la lógica del juego relacionada con los props.
/// Controla cuántos props se deben encontrar, cuántos se han destruido,
/// y notifica cuando todos han sido encontrados.
public class GameManager: MonoBehaviour {
  /// Referencia al componente de texto en la UI para mostrar información sobre los props restantes.
  [SerializeField] private TextMeshPro propsText;

  /// Número total de props que deben ser encontrados en el juego.
  private int totalProps;

  /// Número de props que han sido destruidos por el jugador.
  private int propsDestroyed;

  /// Delegado para manejar la finalización del juego.
  public delegate void GameCompletedHandler();

  /// Evento que se activa cuando todos los props han sido encontrados.
  public event GameCompletedHandler OnGameCompleted;

  /// Método llamado por un prop cuando es destruido.
  /// Incrementa el contador de props destruidos, actualiza la UI
  /// y verifica si todos los props han sido encontrados.
  public void RegisterPropDestroyed() {
    propsDestroyed++;
    UpdateUI();

    /// Comprueba si todos los props han sido destruidos.
    if (propsDestroyed >= totalProps) {
      Debug.Log("¡Todos los props han sido encontrados!");
      OnGameCompleted?.Invoke(); /// Notifica a los suscriptores del evento.
    }
  }

  /// Configura el número total de props que deben ser encontrados.
  /// Reinicia el contador de props destruidos y actualiza la UI.
  /// <param name="total"> El número total de props. </param>
  public void SetTotalProps(int total) {
    totalProps = total;
    propsDestroyed = 0; /// Reinicia el contador de props destruidos.
    UpdateUI();
  }

  /// Actualiza el texto en la UI para mostrar cuántos props quedan por encontrar.
  private void UpdateUI() {
    int remainingProps = totalProps - propsDestroyed;
    propsText.text = $"You have to find {remainingProps} props";
  }
}
