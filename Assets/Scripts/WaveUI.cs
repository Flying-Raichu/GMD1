using System.Collections;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private float displayTime = 2f;
    private float fadeDuration = 0.5f;

    void OnEnable()
    {
        EnemyManager.OnWaveStarted += ShowWaveText;
    }

    void OnDisable()
    {
        EnemyManager.OnWaveStarted -= ShowWaveText;
    }

    void ShowWaveText(int waveNumber)
    {
        StartCoroutine(ShowTextRoutine(waveNumber));
    }

    IEnumerator ShowTextRoutine(int waveNumber)
    {
        waveText.text = $"WAVE {waveNumber}";
        CanvasGroup canvas = waveText.GetComponent<CanvasGroup>();
        canvas.alpha = 0f;
        float t = 0f;
        
        yield return new WaitForSeconds(1f);
        
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        canvas.alpha = 1f;
        yield return new WaitForSeconds(displayTime);

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        canvas.alpha = 0f;
        waveText.text = "";
    }
}