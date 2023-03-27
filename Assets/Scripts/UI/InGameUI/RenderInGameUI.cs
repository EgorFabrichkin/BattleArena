using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.InGameUI
{
    public class RenderInGameUI : MonoBehaviour
    {
        [SerializeField] private Text health = null!;
        [SerializeField] private Text power = null!;

        private void Awake()
        {
            health.EnsureNotNull("text health not specified");
            power.EnsureNotNull("text power not specified");
        }

        public void InitRenderInGameUI(IReadOnlyReactiveProperty<int> health, IReadOnlyReactiveProperty<int> power)
        {
            health.Subscribe(RenderHealth);
            power.Subscribe(RenderPower);
        }
        
        private void RenderHealth(int value) => health.text = $"Health: {value}";
        private void RenderPower(int value) => power.text = $"Power: {value}";
    }
}