using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioClip btnAudio;

    private void Start()
    {
        resumeButton.onClick.AddListener(() => StartCoroutine(AnimateButton(resumeButton.transform, "Resume")));
        quitButton.onClick.AddListener(() => StartCoroutine(AnimateButton(quitButton.transform, "Quit")));
    }
    
    private IEnumerator AnimateButton(Transform buttonTransform, string command)
    {
        GetComponent<AudioSource>().PlayOneShot(btnAudio);
        Vector3 originalScale = buttonTransform.localScale;
        buttonTransform.localScale = originalScale * 0.9f;
        yield return new WaitForSecondsRealtime(0.1f); // otherwise freezes
        buttonTransform.localScale = originalScale;

        yield return new WaitForSecondsRealtime(btnAudio.length); // let audio play if needed

        if (command == "Resume")
        {
            ResumeGame();
        }
        else if (command == "Quit")
        {
            QuitGame();
        }
    }


    private void ResumeGame()
    {
        if (GameManager.instance != null)
        {
            PauseManager.Instance.TogglePause();
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
