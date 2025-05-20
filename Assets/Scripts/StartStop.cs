using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartStop : MonoBehaviour
{
    public void StartGame(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    
    public void OnButtonClick(String command)
    {
        StartCoroutine(AnimateButton(command));
    }

    IEnumerator AnimateButton(String command)
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;

        if (command == "Start")
        {
            StartGame("Main");
        }
        else if (command == "Quit") { QuitGame(); }
    }
}
