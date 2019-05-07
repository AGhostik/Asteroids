using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class Gun : MonoBehaviour {
        public float fireRate = 1f;
        public GameObject bulletPrefab;

        private IController _controller;
        private float _fireRateCountdown;
        private ISpawner _spawner;
        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();
        }

        private void Update() {
            if (_fireRateCountdown <= 0) {
                if (_controller.Fire1()) {
                    _shot(bulletPrefab);
                    _fireRateCountdown = fireRate;
                }
            } else {
                _fireRateCountdown -= Time.deltaTime;
            }
        }

        [Inject]
        private void _init(IController controller, ISpawner spawner) {
            _controller = controller;
            _spawner = spawner;
        }

        private void _shot(GameObject prefab) {
            var bullet = _spawner.Spawn(prefab);
            bullet.transform.position = _transform.position;
            bullet.transform.rotation = _transform.rotation;
            bullet.SetActive(true);
        }
    }
}