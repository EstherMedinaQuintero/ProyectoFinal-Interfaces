using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// Clase que representa una pistola en el juego.
/// Hereda de la clase Weapon y añade lógica específica para disparar proyectiles individuales.
public class Pistol: Weapon {
  /// Prefab del proyectil que la pistola dispara.
  [SerializeField] private Projectile bulletPrefab;

  /// Inicia el proceso de disparo. Se llama cuando el jugador activa el disparo.
  /// <param name="interactor"> El interactor que activa el disparo. </param>
  protected override void StartShooting(XRBaseInteractor interactor) {
    base.StartShooting(interactor);
    /// Dispara una única bala al iniciar el disparo.
    Shoot();
  }

  /// Maneja la lógica específica del disparo, incluyendo la creación y lanzamiento del proyectil.
  protected override void Shoot() {
    base.Shoot();

    /// Crea una instancia del proyectil en la posición y rotación del punto de disparo.
    Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

    /// Inicializa el proyectil con la información de esta arma.
    projectileInstance.Init(this);

    /// Lanza el proyectil.
    projectileInstance.Launch();
  }

  /// Detiene el proceso de disparo. Se llama cuando el jugador desactiva el disparo.
  /// <param name="interactor"> El interactor que detiene el disparo. </param>
  protected override void StopShooting(XRBaseInteractor interactor) {
    base.StopShooting(interactor);
    /// La lógica para detener el disparo ya está manejada en la clase base.
  }
}
