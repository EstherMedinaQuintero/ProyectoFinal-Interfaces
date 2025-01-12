using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    // Desactiva el objeto que contiene el texto pasado un tiempo
    public float timeToDisappear;

    void Start()
    {
        // Desactiva el objeto que contiene el texto pasado un tiempo
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(timeToDisappear);
        gameObject.SetActive(false);
    }
}
