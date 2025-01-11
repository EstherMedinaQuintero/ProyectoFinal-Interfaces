using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que representa un proyectil basado en rayos (raycast).
/// Detecta colisiones a lo largo de su trayectoria y aplica daño a los objetos afectados.
public class RaycastProjectile: Projectile {
  /// Lanza el proyectil usando un raycast para detectar colisiones en la dirección hacia adelante.
  /// Aplica daño a los objetos impactados que implementen la interfaz ITakeDamage.
  public override void Launch() {
    base.Launch();

    /// Variable para almacenar información sobre el impacto del raycast.
    RaycastHit hit;

    /// Realiza un raycast desde la posición del proyectil en la dirección hacia adelante.
    if (Physics.Raycast(transform.position, transform.forward, out hit)) {
      /// Busca todos los componentes ITakeDamage en los objetos padre del objeto impactado.
      ITakeDamage[] damageable = hit.collider.GetComponentsInParent<ITakeDamage>();

      /// Aplica daño a cada uno de los objetos que implementen ITakeDamage.
      foreach (ITakeDamage damageTaker in damageable) {
        damageTaker.TakeDamage(weapon, this, hit.point);
      }
    }
  }
}
