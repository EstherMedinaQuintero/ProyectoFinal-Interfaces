using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Clase base para proyectiles en el juego.
/// Proporciona funcionalidad básica para inicializar un proyectil con un arma y lanzarlo.
/// Se espera que las clases derivadas implementen comportamientos específicos.
public class Projectile: MonoBehaviour {
  /// Referencia al arma que disparó este proyectil.
  protected Weapon weapon;

  /// Inicializa el proyectil con las propiedades del arma que lo disparó.
  /// Este método puede ser sobrescrito por clases derivadas para personalizar la inicialización.
  /// <param name="weapon"> El arma que dispara el proyectil. </param>
  public virtual void Init(Weapon weapon) {
    this.weapon = weapon;
  }

  /// Lógica para lanzar el proyectil.
  /// Este método es virtual y debe ser sobrescrito por las clases derivadas
  /// para implementar un comportamiento de lanzamiento específico.
  public virtual void Launch() {
    /// Método intencionalmente vacío para ser sobrescrito.
  }
}
