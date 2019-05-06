using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    [RequireComponent(typeof(Destroyable))]
    public class GetScore : MonoBehaviour {
        public int score;
        private Destroyable _destroyable;
        private IGameScore _gameScore;

        private void Awake() {
            _destroyable = GetComponent<Destroyable>();
            _destroyable.KillEvent += _destroyableOnKillEvent;
        }

        private void OnDestroy() {
            _destroyable.KillEvent -= _destroyableOnKillEvent;
        }

        private void _destroyableOnKillEvent() {
            _gameScore.Increase(score);
        }

        [Inject]
        private void _init(IGameScore gameScore) {
            _gameScore = gameScore;
        }
    }
}