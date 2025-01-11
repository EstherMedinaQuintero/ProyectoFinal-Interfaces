using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Interfaz para implementar funcionalidad de tomar daño en objetos de juego.
/// Permite a los objetos que implementen esta interfaz recibir daño a través
/// de un arma y un proyectil, especificando el punto de contacto.
public interface ITakeDamage {
  /// Método para manejar el daño recibido.
  /// <param name="weapon"> El arma que causó el daño. </param>
  /// <param name="projectile"> El proyectil que impactó al objeto. </param>
  /// <param name="contactPoint"> El punto en el espacio donde ocurrió el impacto. </param>
  void TakeDamage(Weapon weapon, Projectile projectile, Vector3 contactPoint);
}
