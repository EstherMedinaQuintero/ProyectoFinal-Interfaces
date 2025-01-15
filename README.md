# Proyecto final: The Judgment of the Spirits

## Índice

1. [Introducción](#1-Introducción)
2. [Explicación del juego](#2-Explicación-del-juego)
   1. [Minijuego 1 - Prop Hunt](#Minijuego-1---Prop-Hunt)
   2. [Minijuego 2 - Caldero Mágico](#Minijuego-2---Caldero-Mágico)
   3. [Minijuego 3 - Teletransporte y Checkpoints](#Minijuego-3---Teletransporte-y-Checkpoints)
3. [Explicación de la división del repositorio](#3-Explicación-de-la-división-del-repositorio)
4. [Hitos de programación logrados](#4-Hitos-de-programación-logrados)
5. [Aspectos relacionados con el confort y la buena experiencia del usuario](#5-Aspectos-relacionados-con-el-confort-y-la-buena-experiencia-del-usuario)
6. [Aspectos destacados de la aplicación](#6-Aspectos-destacados-de-la-aplicación)
7. [Interacción multimodal](#7-Interacción-multimodal)
8. [Lista de tareas](#8-Lista-de-tareas)

## 1. Introducción <div id="1-Introducción"/>

Este proyecto es un videojuego interactivo que integra múltiples mecánicas de juego divididas en tres minijuegos principales, con elementos de transición, efectos visuales y un diseño enfocado en la experiencia del usuario. Cada minijuego propone un reto único, incentivando la exploración, la resolución de problemas y el uso de habilidades del jugador.

## 2. Explicación del juego <div id="2-Explicación-del-juego"/>

El juego consta de tres minijuegos principales:

### Minijuego 1 - Prop Hunt <div id="Minijuego-1---Prop-Hunt"/>

- Encuentra y destruye objetos escondidos utilizando armas como pistolas y rifles.
- Los objetos ofrecen pistas visuales y sonoras para facilitar su localización.
- Un sistema de tutorial guía al jugador durante las fases iniciales.

### Minijuego 2 - Caldero Mágico <div id="Minijuego-2---Caldero-Mágico"/>

- Combina ingredientes específicos (cristal, flor, runa) en un caldero para activar efectos mágicos.
- Al completar el desafío, el jugador desbloquea nuevas áreas.

### Minijuego 3 - Teletransporte y Checkpoints <div id="Minijuego-3---Teletransporte-y-Checkpoints"/>

- Usa una perla mágica para teletransportarte a ubicaciones estratégicas mientras evitas peligros.
- Los checkpoints permiten guardar el progreso y reaparecer en puntos seguros tras errores.

Adicionalmente, un sistema de transición conecta los diferentes niveles y gestiona recompensas según el progreso.

## 3. Explicación de la división del repositorio <div id="3-Explicación-de-la-división-del-repositorio"/>

El repositorio se organiza en carpetas que reflejan los módulos principales del proyecto:

- **Multimedia**: Contiene recursos gráficos y sonoros utilizados en el juego.
- **Scripts**: Incluye todo el código del proyecto, organizado en subcarpetas:
  - **Minijuego_1**: Scripts relacionados con el Prop Hunt.
  - **Minijuego_2**: Scripts para la lógica del Caldero Mágico.
  - **Minijuego_3**: Scripts para teletransportes, checkpoints y desafíos asociados.
  - **Spawn**: Scripts generales para la transición entre escenas, portales y gestión de recompensas.

Cada subcarpeta incluye un archivo `EXPLICACION.md` que detalla el propósito de los scripts contenidos.

## 4. Hitos de programación logrados <div id="4-Hitos-de-programación-logrados"/>

1. **Diseño modular**:
   - Los scripts están organizados para ser reutilizables y fáciles de extender.
   - Uso de patrones como el Singleton para la gestión centralizada de transiciones.

2. **Interacciones inmersivas**:
   - Integración de armas con efectos físicos y proyectiles realistas.
   - Teletransporte dinámico mediante elementos VR como la "Ender Pearl".

3. **Gestión de progresos**:
   - Implementación de un sistema de checkpoints y guardado de posiciones.
   - Activación dinámica de recompensas y portales según el progreso del jugador.

4. **Feedback al jugador**:
   - Incorporación de tutoriales interactivos y pistas sonoras/visuales.
   - Efectos de transición suaves entre escenas, incluyendo animaciones de fade in/out.

## 5. Aspectos relacionados con el confort y la buena experiencia del usuario <div id="5-Aspectos-relacionados-con-el-confort-y-la-buena-experiencia-del-usuario"/>

- **Transiciones suaves**: El uso de animaciones de fade in/out y tiempos de espera ajustados proporciona una experiencia fluida y evita cambios bruscos entre escenas.
- **Movimiento suave mediante teletransporte:** En lugar de movimientos fluidos continuos que pueden generar mareos, el sistema utiliza teletransporte para reposicionar al jugador de forma inmediata.
- **Tutoriales guiados**: Los mensajes visuales y los audios en tiempo real aseguran que los jugadores comprendan las mecánicas sin frustraciones.
- **Efectos de audio y partículas**: Cada acción importante (como disparos o activación de portales) está acompañada de efectos que aumentan la inmersión.
- **Checkpoints y guardado**: Los jugadores no pierden todo su progreso al fallar, minimizando la frustración.
- **Entornos relajantes inspirados en la naturaleza**: Espacios diseñados con elementos naturales, como paisajes verdes y ambientes llenos de vegetación, ayudan a reducir la sobrecarga sensorial y promueven una sensación de calma y bienestar.

## 6. Aspectos destacados de la aplicación <div id="6-Aspectos-destacados-de-la-aplicación"/>

1. **Sistema de recompensas dinámicas**:
   - Los portales y recompensas se desbloquean en función del progreso en los niveles, creando una sensación de logro.

2. **Interactividad VR**:
   - Los elementos como la "Ender Pearl" introducen mecánicas innovadoras que aprovechan las capacidades de realidad virtual.

3. **Efectos visuales y sonoros personalizados**:
   - Los sistemas de partículas, las pistas acústicas y los cambios de color mantienen al jugador enfocado e inmerso en el juego.

4. **Diseño flexible y escalable**:
   - La estructura del proyecto permite agregar nuevos minijuegos o funcionalidades sin alterar significativamente el código existente.

## 7. Interacción multimodal <div id="7-Interacción-multimodal"/>

El juego incorpora **comandos de voz** para activar portales, permitiendo una interacción más natural. Usando un micrófono, los jugadores pueden realizar esta acción sin necesidad de controles físicos. Esta integración de voz y Realidad Virtual mejora la inmersión y fluidez de la experiencia, haciendo el juego más accesible e intuitivo.

## 8. Lista de tareas <div id="8-Lista-de-tareas"/>

Cada miembro del equipo ha asumido un **25% de las tareas totales**, asegurando una participación equilibrada en el proyecto.

1. **Desarrollo de los minijuegos**:
   - Minijuego 1 - Prop Hunt
   - Minijuego 2 - Caldero Mágico
   - Minijuego 3 - Teletransporte y Checkpoints

2. **Documentación del código**:
   - Detallar la funcionalidad de cada script y su integración en el proyecto.

3. **Diseño e implementación del repositorio**:
   - Organización de carpetas y estructura del proyecto.
   - Creación del archivo `README.md` explicando el propósito y uso del proyecto.

4. **Interacción multimodal**:
   - Integración de comandos de voz mediante Hugging Face.
   - Pruebas y ajustes de accesibilidad y experiencia de usuario.

5. **Efectos visuales y sonoros**:
   - Implementación de partículas, transiciones suaves y efectos inmersivos.

6. **Integración del proyecto**:
   - Revisión conjunta para garantizar la cohesión de los diferentes módulos.

7. **Presentación final**:
   - Diseño de diapositivas y preparación de la exposición.
