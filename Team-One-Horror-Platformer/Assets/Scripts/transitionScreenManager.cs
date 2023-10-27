using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class transitionScreenManager : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float displayDuration = 3.0f;
    public GameObject canvasHolder;

    private void Start()
    {
        canvasHolder.SetActive(false);

        StartCoroutine(DisplayTextForSeconds());
    }

    public IEnumerator DisplayTextForSeconds()
    {
        canvasHolder.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        // Deactivate the GameObject containing the UI Canvas
        canvasHolder.SetActive(false);
    }
}
