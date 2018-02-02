using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PocketRocket
{
    public class Loading : MonoBehaviour
    {
        // Next Scene
        public static int index = 1;

        [Header("Loading Bar UI")]
        // Loading Bar UI
        public Slider loadingBar;

        // Start
        private void Start()
        {
            StartCoroutine(LoadAsynchronously());
        }

        // Load Scene
        private IEnumerator LoadAsynchronously()
        {
            // Get Next Scene
            Scene scene = SceneManager.GetSceneByBuildIndex(index);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

            // Progress While Not Finished
            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                loadingBar.value = progress;

                yield return null;
            }

            // Set Next Scene
            SceneManager.SetActiveScene(scene);

            yield return null;
        }

        // Set Next Scene Index
        public static void SetNextScene(int index)
        {
            Loading.index = index;
        }

        // Get Next Scene Index
        public static int GetNextScene()
        {
            return Loading.index;
        }
    }
}