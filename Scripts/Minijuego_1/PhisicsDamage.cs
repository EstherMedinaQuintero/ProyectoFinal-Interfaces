using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que implementa la interfaz ITakeDamage para manejar daño basado en física.
/// Requiere un componente Rigidbody para aplicar fuerzas físicas al recibir daño.
[RequireComponent(typeof(Rigidbody))]

public class PhysicsDamage: MonoBehaviour, ITakeDamage {
  /// Referencia al componente Rigidbody asociado al objeto.
  private Rigidbody rigidbody;

  /// Inicializa las referencias necesarias para la clase.
  /// Busca el componente Rigidbody en el objeto al que está adjunta esta clase.
  private void Awake() {
    rigidbody = GetComponent<Rigidbody>();
  }

  /// Implementación del método TakeDamage de la interfaz ITakeDamage.
  /// Aplica una fuerza al Rigidbody en la dirección del proyectil basado en la fuerza del arma.
  /// <param name="weapon"> El arma que causa el daño, utilizada para calcular la fuerza de disparo. </param>
  /// <param name="projectile"> El proyectil que impactó al objeto, define la dirección del impacto. </param>
  /// <param name="contactPoint"> El punto de contacto del impacto. </param>
  public void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint) {
    /// Aplica una fuerza en la dirección del proyectil multiplicada por la fuerza del arma.
    rigidbody.AddForce(projectile.transform.forward * weapon.GetShootingForce(), ForceMode.Impulse);
  }
}
