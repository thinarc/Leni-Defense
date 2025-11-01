using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Loading
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager Instance { get; private set; }
        
        public GameObject loadingUi;
        public Slider progressBar;
        public TMP_Text statusText;

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public async UniTask LoadSceneAsync(string sceneName, List<LoadingStep> steps)
        {
            var loadingSceneStep = new LoadingStep("Load Scene..", async () => await LoadScene(sceneName));
            steps.Add(loadingSceneStep);
            
            var stepFraction = 1f / steps.Count;
            var currentProgress = 0f;
            
            progressBar.value = currentProgress;
            loadingUi.SetActive(true);
        
            foreach (var step in steps)
            {
                statusText.text = step.Description;
                await step.ActionAsync();
                
                currentProgress += stepFraction;
                progressBar.value = currentProgress;
            }
            
            loadingUi.SetActive(false);
        }

        private static async UniTask LoadScene(string sceneName)
        {
            var asyncOp = SceneManager.LoadSceneAsync(sceneName);
            if (asyncOp == null) return;
            asyncOp.allowSceneActivation = false;
            
            while (asyncOp.progress < 0.9f)
            {
                await UniTask.Yield();
            }
            
            asyncOp.allowSceneActivation = true;
        }
    }
}