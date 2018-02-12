using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchrosly(sceneName));
    }

    IEnumerator LoadAsynchrosly(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / .9f);

            progressText.text = progress * 100 + "%";
            slider.value = progress;

            yield return null;
        }
    }
}