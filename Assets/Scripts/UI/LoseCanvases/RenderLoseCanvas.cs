using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI.LoseCanvases
{
    public class RenderLoseCanvas : MonoBehaviour
    {
        [SerializeField] private Text count = null!;
        [SerializeField] private float artificialDelay = 0.2f;
        
        private void Awake()
        {
            count.EnsureNotNull("text count not specified");
        }

        public void InitRenderLoseCanvas(IReadOnlyReactiveProperty<int> count)
        {
            count.Subscribe(RenderCount);
        }
        
        private void RenderCount(int value) => count.text = $"You score: {value}";
        
        public void LoadScene(int sceneId) => StartCoroutine(Load(sceneId));

        private IEnumerator Load(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
            yield return new WaitForSecondsRealtime(artificialDelay);
        }
    }
}