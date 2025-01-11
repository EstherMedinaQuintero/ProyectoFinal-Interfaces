using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase que extiende la funcionalidad de Projectile para proyectiles que utilizan física.
/// Este proyectil usa un Rigidbody para aplicar movimiento físico y colisiones.
[RequireComponent(typeof(Rigidbody))]

public class PhysicsProjectile: Projectile {
  /// Tiempo de vida del proyectil antes de que sea destruido automáticamente.
  [SerializeField] private float lifeTime;

  /// Referencia al componente Rigidbody asociado al proyectil.
  private Rigidbody rigidbody;

  /// Método llamado cuando se inicializa el objeto.
  /// Asigna el componente Rigidbody.
  private void Awake() {
    rigidbody = GetComponent<Rigidbody>();
  }

  /// Inicializa el proyectil con las propiedades del arma.
  /// Configura el tiempo de destrucción automática.
  /// <param name="weapon"> El arma que dispara el proyectil. </param>
  public override void Init(Weapon weapon) {
    base.Init(weapon);
    /// Destruye el proyectil después de 'lifeTime' segundos.
    Destroy(gameObject, lifeTime);
  }

  /// Lanza el proyectil aplicando una fuerza física en la dirección deseada.
  public override void Launch() {
    base.Launch();
    /// Aplica fuerza relativa hacia adelante, usando la fuerza del disparo del arma.
    rigidbody.AddRelativeForce(Vector3.forward * weapon.GetShootingForce(), ForceMode.Impulse);
  }

  /// Detecta colisiones con otros objetos y aplica daño si corresponde.
  /// <param name="other"> El collider del objeto con el que colisionó. </param>
  private void OnTriggerEnter(Collider other) {
    /// Destruye el proyectil al impactar.
    Destroy(gameObject);

    /// Busca todos los componentes ITakeDamage en los objetos padre del objeto impactado.
    ITakeDamage[] damageTakers = other.GetComponentsInParent<ITakeDamage>();
    
    /// Aplica daño a cada uno de los objetos que implementen ITakeDamage.
    foreach (ITakeDamage damageTaker in damageTakers) {
      damageTaker.TakeDamage(weapon, this, transform.position);
    }
  }
}
