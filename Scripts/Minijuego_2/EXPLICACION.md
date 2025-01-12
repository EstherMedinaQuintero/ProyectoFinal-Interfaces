# Minijuego Camino Mágico

Este proyecto implementa un minijuego interactivo donde el jugador debe seguir un camino mágico, superar tres pruebas y llegar al final del nivel. Este documento explica el funcionamiento del juego y describe el propósito de cada script.

---

## Índice

1. [Funcionalidad del juego](#funcionalidad-del-juego)
2. [Estructura de scripts](#estructura-de-scripts)
    - [Gestión del audio](#gestión-del-audio)
    - [Pruebas](#pruebas)
    - [Puertas y camino](#puertas-y-camino)
3. [Flujo del juego](#flujo-del-juego)
4. [Cómo jugar](#cómo-jugar)

---

## Funcionalidad del juego <div id="funcionalidad-del-juego"/>

1. **Inicio del minijuego**:
   - El jugador aparece en el área inicial y recibe instrucciones auditivas para avanzar por el camino.

2. **Pruebas mágicas**:
   - El jugador debe completar tres pruebas consecutivas:
     - **Prueba 1**: Colocar una flor, una runa y un cristal en la base iluminada.
     - **Prueba 2**: Llevar cuatro cristales a los pedestales correspondientes.
     - **Prueba 3**: Recorrer un laberinto hasta llegar al final.

3. **Desbloqueo de caminos**:
   - Cada prueba tiene una puerta que bloquea el camino hasta que se cumpla la condición para superarla.

4. **Portal de salida**:
   - Al final del laberinto, el jugador encuentra un portal que lo regresa a la sala principal.

---

## Estructura de scripts <div id="estructura-de-scripts"/>

### 1. Gestión del audio <div id="gestión-del-audio"/>

- **`Game1_Audio.cs`**:
  - Se activa cuando el jugador entra en contacto con un cubo invisible al inicio del minijuego.
  - Reproduce un audio con instrucciones iniciales. Este audio se reproduce solo una vez.

### 2. Pruebas <div id="pruebas"/>
#### Prueba 1
- **`MagicCouldron.cs`**
  - Detecta que los tres elementos necesarios (flor, runa y cristal) están en la base asignada.
  - Al completarse la prueba:
    - Se activa una luz indicativa.
    - Se llama a la función que desbloquea la puerta correspondiente.
#### Prueba 2
- **`WildCrystal.cs`**:
  - Cada pedestal tiene un cubo invisible con este script.
  - Detecta si el cristal correcto está colocado en el pedestal correspondiente.
  - Al colocar el cristal correcto:
    - Se ilumina la plataforma asociada.
    - Se actualiza el estado en el `CrystalManager`.
- **`CrystalManager.cs`**:
  - Controla el estado de los pedestales en la segunda prueba.
  - Cuando los cuatro pedestales están correctos, llama a la función para desbloquear la puerta de esta prueba.

#### Prueba 3 
- **`PortalController.cs`**:
  - Gestiona el portal al final del laberinto.
  - Transporta al jugador de regreso a la sala principal al interactuar con el portal.

### 3. Puertas y camino <div id="puertas-y-camino"/>
- **`WallBehaviour.cs`**:
  - Asociado a las puertas que bloquean el camino.
  - Define una función para desactivar las puertas cuando se completa la prueba correspondiente.

---

## Flujo del juego <div id="flujo-del-juego"/>

1. **Inicio**:
   - El jugador aparece en el área inicial y escucha las instrucciones.
   - Se le indica que siga el camino y complete las pruebas.
   - Hay un cuadro de texto con las intrucciones de los mandos
   
2. **Prueba 1: La receta mágica**:
   - El jugador recoge los tres elementos iluminados y los coloca en la base.
   - Al completarse, se desbloquea la primera puerta.

3. **Prueba 2: Cristales elementales**:
   - El jugador busca los cuatro cristales y los coloca en los pedestales correctos.
   Al completarse, se desbloquea la segunda puerta.

4. **Prueba 3: El laberinto**:
   - El jugador recorre el laberinto hasta encontrar el portal al final.
   - Al interactuar con el portal, regresa a la sala principal.

---

## Cómo jugar <div id="cómo-jugar"/>

1. Sigue el camino iluminado y completa las pruebas en el orden indicado.
2. Usa el controlador para:
   - Agarrar los elementos necesarios.
   - Colocarlos en las áreas indicadas.
3. Resuelve las pruebas y atraviesa el laberinto para llegar al portal.
