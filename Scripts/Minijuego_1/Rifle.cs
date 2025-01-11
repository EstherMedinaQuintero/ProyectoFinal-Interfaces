using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// Clase que representa un rifle como arma.
/// Controla el disparo continuo, la personalización del color del láser
/// y la interacción con proyectiles.
public class Rifle: Weapon {
  /// Referencia al objeto láser asociado al rifle.
  [SerializeField] private GameObject laser;

  /// Tasa de disparo del rifle (disparos por segundo).
  [SerializeField] private float fireRate;

  /// Referencia al proyectil disparado por el rifle.
  private Projectile projectile;

  /// Tiempo de espera entre disparos consecutivos, calculado según la tasa de disparo.
  private WaitForSeconds wait;

  /// Color del láser cuando no está disparando (blanco semitransparente).
  private Color laserNoShootColor;

  /// Color del láser cuando está disparando (rojo opaco).
  private Color laserShootColor;

  /// Inicializa componentes y configura referencias necesarias.
  protected override void Awake() {
    base.Awake();
    /// Obtiene el proyectil hijo del rifle.
    projectile = GetComponentInChildren<Projectile>();
  }

  /// Configura el rifle al inicio, incluyendo colores del láser y tiempo entre disparos.
  private void Start() {
    /// Calcula el tiempo de espera entre disparos en función de la tasa de disparo.
    wait = new WaitForSeconds(1 / fireRate);

    /// Inicializa el proyectil asociándolo con este rifle.
    projectile.Init(this);

    /// Configura el color del láser para el estado de no disparo.
    laserNoShootColor = new Color(1, 1, 1, 0.5f);

    /// Configura el color del láser para el estado de disparo.
    laserShootColor = new Color(1, 0, 0, 1);

    /// Establece el color inicial del láser como el color de no disparo.
    laser.GetComponent<Renderer>().material.color = laserNoShootColor;
  }

  /// Inicia el disparo continuo y cambia el color del láser al de disparo.
  /// <param name="interactor"> El interactor que activa el disparo. </param>
  protected override void StartShooting(XRBaseInteractor interactor) {
    base.StartShooting(interactor);

    /// Cambia el color del láser al de disparo.
    laser.GetComponent<Renderer>().material.color = laserShootColor;

    /// Inicia la corrutina para disparar continuamente.
    StartCoroutine(ShootingCO());
  }

  /// Corrutina que controla el disparo continuo.
  /// <returns> Iterador de la corrutina. </returns>
  private IEnumerator ShootingCO() {
    while (true) {
      /// Dispara el proyectil.
      Shoot();

      /// Espera el tiempo definido antes del siguiente disparo.
      yield return wait;
    }
  }

  /// Lanza un proyectil cuando se dispara el rifle.
  protected override void Shoot() {
    base.Shoot();
    /// Lanza el proyectil asociado al rifle.
    projectile.Launch();
  }

  /// Detiene el disparo continuo y restaura el color del láser.
  /// <param name="interactor"> El interactor que detiene el disparo. </param>
  protected override void StopShooting(XRBaseInteractor interactor) {
    base.StopShooting(interactor);

    /// Cambia el color del láser al de no disparo.
    laser.GetComponent<Renderer>().material.color = laserNoShootColor;

    /// Detiene todas las corrutinas activas (en este caso, el disparo continuo).
    StopAllCoroutines();
  }
}
