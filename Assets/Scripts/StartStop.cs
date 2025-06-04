using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartStop : MonoBehaviour
{
    [SerializeField] private AudioClip btnAudio;
    
    public void StartGame(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnButtonClick(String command)
    {
        StartCoroutine(AnimateButton(command));
        gameObject.GetComponent<AudioSource>().PlayOneShot(btnAudio);
    }

    IEnumerator AnimateButton(String command)
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;

        yield return new WaitForSeconds(btnAudio.length); //so audio can play
        
        if (command == "Start")
        {
            StartGame("Main");
        }
        else if (command == "Quit") { QuitGame(); }
    }
}
