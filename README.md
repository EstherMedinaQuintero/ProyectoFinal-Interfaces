# Proyecto final: The Judgment of the Spirits

## 1. Introducción

Este proyecto es un videojuego interactivo que integra múltiples mecánicas de juego divididas en tres minijuegos principales, con elementos de transición, efectos visuales y un diseño enfocado en la experiencia del usuario. Cada minijuego propone un reto único, incentivando la exploración, la resolución de problemas y el uso de habilidades del jugador.

---

## 2. Explicación del juego

El juego consta de tres minijuegos principales:

1. **Minijuego 1 - Prop Hunt VR**:
   - Encuentra y destruye objetos escondidos utilizando armas como pistolas y rifles.
   - Los objetos ofrecen pistas visuales y sonoras para facilitar su localización.
   - Un sistema de tutorial guía al jugador durante las fases iniciales.

2. **Minijuego 2 - Camino Mágico**:
   - Sigue un camino encantado mientras superas tres pruebas mágicas para avanzar.
      - **Prueba 1**: Combina un cristal, una flor y una runa en una plataforma mágico para desbloquear la primera puerta.
      - **Prueba 2**: Encuentra y coloca cuatro cristales en los pedestales correctos, iluminando el camino hacia la siguiente área.
      - **Prueba 3**: Recorre un laberinto y encuentra el portal final que te llevará de regreso al inicio.
   - El juego incluye instrucciones visuales y auditivas para guiar al jugador, aprovechando las capacidades inmersivas de la realidad virtual.

3. **Minijuego 3 - Teletransporte y Checkpoints**:
   - Usa una perla mágica para teletransportarte a ubicaciones estratégicas mientras evitas peligros.
   - Los checkpoints permiten guardar el progreso y reaparecer en puntos seguros tras errores.

Adicionalmente, un sistema de transición conecta los diferentes niveles y gestiona recompensas según el progreso.

---

## 3. Explicación de la división del repositorio

El repositorio se organiza en carpetas que reflejan los módulos principales del proyecto:

- **Multimedia**: Contiene recursos gráficos y sonoros utilizados en el juego.
- **Scripts**: Incluye todo el código del proyecto, organizado en subcarpetas:
  - **Minijuego_1**: Scripts relacionados con el Prop Hunt.
  - **Minijuego_2**: Scripts para la lógica del Camino Mágico.
  - **Minijuego_3**: Scripts para teletransportes, checkpoints y desafíos asociados.
  - **Spawn**: Scripts generales para la transición entre escenas, portales y gestión de recompensas.

Cada subcarpeta incluye un archivo `EXPLICACION.md` que detalla el propósito de los scripts contenidos.

---

## 4. Hitos de programación logrados

1. **Diseño modular**:
   - Los scripts están organizados para ser reutilizables y fáciles de extender.
   - Uso de patrones como el Singleton para la gestión centralizada de transiciones.

2. **Interacciones inmersivas**:
   - Integración de armas con efectos físicos y proyectiles realistas.
   - Teletransporte dinámico mediante elementos VR como la "Ender Pearl".
   - Diálogos de Eva que guían al jugador durante su aventura.

3. **Gestión de progresos**:
   - Implementación de un sistema de checkpoints y guardado de posiciones.
   - Activación dinámica de recompensas y portales según el progreso del jugador.

4. **Feedback al jugador**:
   - Tutoriales interactivos que facilitan la comprensión de las mecánicas del juego.
   - Pistas sonoras y visuales para reforzar la interacción y guiar al jugador.
   - Transiciones suaves entre escenas, con efectos de animación como fade in/fade out.
5. **Uso de eventos**:
   - Implementación de eventos para manejar las interacciones con los botones del mando.
   - Activación de mensajes y notificaciones durante el juego.
   - Desbloqueo de elementos y funciones en diferentes niveles según las acciones del jugador.

---

## 5. Aspectos relacionados con el confort y la buena experiencia del usuario

- **Transiciones suaves**: El uso de animaciones de fade in/out y tiempos de espera ajustados proporciona una experiencia fluida y evita cambios bruscos entre escenas.
- **Movimiento suave mediante teletransporte:** En lugar de movimientos fluidos continuos que pueden generar mareos, el sistema utiliza teletransporte para reposicionar al jugador de forma inmediata.
- **Tutoriales guiados**: Los mensajes visuales y los audios en tiempo real aseguran que los jugadores comprendan las mecánicas sin frustraciones.
- **Efectos de audio y partículas**: Cada acción importante (como disparos o activación de portales) está acompañada de efectos que aumentan la inmersión.
- **Checkpoints y guardado**: Los jugadores no pierden todo su progreso al fallar, minimizando la frustración.

---

## 6. Aspectos destacados de la aplicación

1. **Sistema de recompensas dinámicas**:
   - Los portales y recompensas se desbloquean en función del progreso en los niveles, creando una sensación de logro.

2. **Interactividad VR**:
   - Los elementos como la "Ender Pearl" introducen mecánicas innovadoras que aprovechan las capacidades de realidad virtual.

3. **Efectos visuales y sonoros personalizados**:
   - Los sistemas de partículas, las pistas acústicas y los cambios de color mantienen al jugador enfocado e inmerso en el juego.

4. **Diseño flexible y escalable**:
   - La estructura del proyecto permite agregar nuevos minijuegos o funcionalidades sin alterar significativamente el código existente.

---
