using System;
using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class ChangeView : MonoBehaviour {
        public MeshRenderer meshRenderer;
        public SpriteRenderer spriteRenderer;

        private IGameSettings _gameSettings;
        private GameView _gameView;
        private bool _set;
        private bool _skipUpdate;

        private void Awake() {
            _setGameView();
            _gameSettings.GameViewChanged += _setGameView;
        }

        private void OnDestroy() {
            _gameSettings.GameViewChanged -= _setGameView;
        }

        private void Update() {
            if (!_set) {
                return;
            }

            if (_skipUpdate) {
                _skipUpdate = false;
                return;
            }

            switch (_gameView) {
                case GameView.Sprite:
                    meshRenderer.enabled = false;
                    break;
                case GameView.ThreeD:
                    spriteRenderer.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _set = false;
        }

        [Inject]
        private void _init(IGameSettings gameSettings) {
            _gameSettings = gameSettings;
        }

        private void _setGameView() {
            // выключение одного и включение другого рендера одновременно приводит к тому,
            // что за один кадр они оба оказываются IsVisible = false
            // по этой причине в компоненте DisableInvisible они выключаются из сцены
            _set = true;
            _skipUpdate = true;
            _gameView = _gameSettings.GameView;
            switch (_gameView) {
                case GameView.Sprite:
                    spriteRenderer.enabled = true;
                    break;
                case GameView.ThreeD:
                    meshRenderer.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}