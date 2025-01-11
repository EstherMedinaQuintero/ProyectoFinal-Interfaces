using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
  public AudioSource audioSource; 
    public AudioClip[] audioClips;  
    public GameObject[] panels;     
    public MonoBehaviour scriptToActivate; 

    private int currentClipIndex = 0;

    private void Start() {
      foreach (GameObject panel in panels) {
        panel.SetActive(false);
      }
      if (scriptToActivate != null) {
        scriptToActivate.enabled = false;
      }
      PlayNextClip();
    }

    private void PlayNextClip() {
      if (currentClipIndex < audioClips.Length) {
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.volume = 0.5f;
        audioSource.Play();
        currentClipIndex++;
        Invoke(nameof(PlayNextClip), audioSource.clip.length);
      } else {
        OnAudioSequenceComplete();
      }
    }

    private void OnAudioSequenceComplete() {
        foreach (GameObject panel in panels) {
          panel.SetActive(true);
          StartCoroutine(DropFromAbove(panel.transform));
        }
        if (scriptToActivate != null) {
          scriptToActivate.enabled = true;
        }
    }

     private IEnumerator DropFromAbove(Transform panelTransform) {
        float duration = 1f;  // Duración de la caída
        float elapsedTime = 0f;

        Vector3 startPos = panelTransform.position + new Vector3(0, 500, 0); // 500 unidades arriba
        Vector3 endPos = panelTransform.position;

        panelTransform.position = startPos;

        while (elapsedTime < duration) {
            panelTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelTransform.position = endPos;
    }
}
