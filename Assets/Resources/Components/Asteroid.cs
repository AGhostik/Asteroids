using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class Asteroid : MonoBehaviour {
        public int score;
        private bool _canDissapear;
        private Destroyable _destroyable;
        private IGameScore _gameScore;

        private void Awake() {
            _destroyable = GetComponent<Destroyable>();
            _destroyable.afterDestroy = _afterDestroy;
        }

        private void OnBecameInvisible() {
            if (_canDissapear) {
                gameObject.SetActive(false);
            }
        }

        private void OnBecameVisible() {
            _canDissapear = true;
        }

        private void _afterDestroy(Collider2D obj) {
            _gameScore.Increase(score);
        }

        [Inject]
        private void _init(IGameScore gameScore) {
            _gameScore = gameScore;
        }
    }
}