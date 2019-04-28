using System.Collections.Generic;
using System.Linq;
using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class Gun : MonoBehaviour {
        public float fireRate = 1;
        public GameObject bulletPrefab;
        private readonly List<Bullet> _bulletCache = new List<Bullet>();
        private Transform _bulletContainer;
        private IController _controller;
        private float _fireRateCountdown;

        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();

            var bulletContainer = new GameObject("BulletContainer");
            _bulletContainer = bulletContainer.GetComponent<Transform>();
            _bulletContainer.position = _transform.position;
            _bulletContainer.parent = _transform;
        }

        private void Update() {
            if (_fireRateCountdown <= 0) {
                if (_controller.Fire1()) {
                    var bullet = _getNextBullet();
                    bullet.Launch(_transform.position, _transform.rotation);
                    _fireRateCountdown = fireRate;
                }
            } else {
                _fireRateCountdown -= Time.deltaTime;
            }
        }

        private Bullet _getNextBullet() {
            var nextBullet = _bulletCache.FirstOrDefault(_ => _.CanLaunch());
            if (nextBullet == null) {
                var bullet = Instantiate(bulletPrefab, _bulletContainer);
                nextBullet = bullet.GetComponent<Bullet>();
                _bulletCache.Add(nextBullet);
            }

            return nextBullet;
        }

        [Inject]
        private void _init(IController controller) {
            _controller = controller;
        }
    }
}