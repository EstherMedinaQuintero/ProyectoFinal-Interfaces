using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.IO; // Añade esto para usar MemoryStream

namespace HuggingFace.API.Examples {
    public class SpeechRecognitionWithOculus : MonoBehaviour {
        [SerializeField] private GameObject portal;
        [SerializeField] private string activationWord = "portal";

        private AudioClip clip;
        private byte[] bytes;
        private bool recording;

        // Acción de entrada para iniciar y detener la grabación
        public InputActionReference startRecordingAction; // Acción para iniciar la grabación
        public InputActionReference stopRecordingAction; // Acción para detener la grabación
        public GameObject errorObject;
        [SerializeField] private TMP_Text recognizedText; // Referencia al TextMeshPro


        private void Awake() {
            // Habilitar las acciones de entrada
            startRecordingAction.action.Enable();
            stopRecordingAction.action.Enable();
            
            // Asociar las acciones de entrada a los métodos
            startRecordingAction.action.performed += ctx => StartRecording();
            stopRecordingAction.action.performed += ctx => StopRecording();
        }

        private void Update() {
            // Verificar si se debe detener automáticamente después de grabar
            if (recording && Microphone.GetPosition(null) >= clip.samples) {
                StopRecording();
            }
        }

        private void StartRecording() {
            Debug.Log("Has presionado el botón de grabación.");
            recognizedText.text = "¿Quieres hablarnos, mortal?";
            if (recording) return;

            Debug.Log("Recording...");
            clip = Microphone.Start(null, false, 10, 44100);
            recording = true;
        }

        private void StopRecording() {
            Debug.Log("Has presionado el botón de detener grabación.");
            if (!recording) return;
            var position = Microphone.GetPosition(null);
            Microphone.End(null);
            if (position <= 0) {
                Debug.LogWarning("Posición del micrófono no válida: " + position);
                recognizedText.text = "Tu voz no está funcionando correctamente mortal.";
                return;  // No sigas con el proceso si la posición no es válida
            }
            var samples = new float[position * clip.channels];
            recognizedText.text = "Antes de getData";
            clip.GetData(samples, 0);
            recognizedText.text = "Después de getData";
            bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
            recognizedText.text = "Después de encode";
            recording = false;
            SendRecording();
        }

        private void SendRecording() {
            Debug.Log("Sending...");
            recognizedText.text = "Gracias por intentar hablar con nosotros mortal, ahora escucharemos lo que has dicho.";
            HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
                Debug.Log("Dijiste: " + response);
                errorObject.SetActive(false);
                // si la respuesta no es nula activa el portal
                if (response != null) {
                    recognizedText.text = "Hemos escuchado algo: " + response;
                    Debug.Log("¡Palabra correcta! El portal se ha activado.");
                    ActivatePortal();
                } else {
                    recognizedText.text = "No has dicho nada";
                    Debug.Log("Palabra incorrecta. Intenta de nuevo.");
                }

            }, error => {
                recognizedText.text = "No te podemos escuchar ahora: " + error;
                errorObject.SetActive(true);
            });
            if (recognizedText.text == "Gracias por intentar hablar con nosotros mortal, ahora escucharemos lo que has dicho.") {
                recognizedText.text = "Nuestro magia para escuchar no ha funcionado";
            }
        }

        private void ActivatePortal() {
            if (portal != null) {
                portal.SetActive(true); 
            }
        }

        private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
            using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
                using (var writer = new BinaryWriter(memoryStream)) {
                    writer.Write("RIFF".ToCharArray());
                    writer.Write(36 + samples.Length * 2);
                    writer.Write("WAVE".ToCharArray());
                    writer.Write("fmt ".ToCharArray());
                    writer.Write(16);
                    writer.Write((ushort)1);
                    writer.Write((ushort)channels);
                    writer.Write(frequency);
                    writer.Write(frequency * channels * 2);
                    writer.Write((ushort)(channels * 2));
                    writer.Write((ushort)16);
                    writer.Write("data".ToCharArray());
                    writer.Write(samples.Length * 2);

                    foreach (var sample in samples) {
                        writer.Write((short)(sample * short.MaxValue));
                    }
                }
                return memoryStream.ToArray();
            }
        }
    }
}
