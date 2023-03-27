using System.Collections;
using UI.InGameUI;
using UI.LoseCanvases;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils;

namespace UI
{
    public class UIFacade : MonoBehaviour
    {
        [SerializeField] private RenderInGameUI renderInGameUI = null!;
        [SerializeField] private RenderLoseCanvas renderLoseCanvas = null!;

        private void Awake()
        {
            renderInGameUI.EnsureNotNull("RenderPlayerValues not specified");
            renderLoseCanvas.EnsureNotNull("RenderKillEnemiesCount not specified");
            renderLoseCanvas.gameObject.SetActive(false);
        }

        public void InitRenderValue(IReadOnlyReactiveProperty<int> health, IReadOnlyReactiveProperty<int> power)
        {
            renderInGameUI.InitRenderInGameUI(health, power);
        }

        public void InitRenderLoseCanvas(IReadOnlyReactiveProperty<int> count)
        {
            renderLoseCanvas.InitRenderLoseCanvas(count);
        }

        public void OnLose(UnityEvent onPlayerDead)
        {
            onPlayerDead.AddListener(() =>
                {
                    renderLoseCanvas.gameObject.SetActive(true);
                    renderInGameUI.gameObject.SetActive(false);
                }
            );
        }
    }
}