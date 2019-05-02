using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class LaserGun : MonoBehaviour {
        private const int BeamHitsCount = 64;

        public float laserTime = 1.5f;
        public float coolDownTime = 5f;
        public float range = 50f;
        public LayerMask enemyLayer;

        private RaycastHit2D[] _beamHits;
        private IController _controller;
        private float _coolDown;
        private float _laserChargeTime;
        private LineRenderer _lineRenderer;
        private Transform _transform;

        private void Awake() {
            _lineRenderer = GetComponent<LineRenderer>();
            _transform = GetComponent<Transform>();

            _beamHits = new RaycastHit2D[BeamHitsCount];
        }

        private void Update() {
            if (_coolDown <= 0) {
                if (_controller.Fire2()) {
                    _coolDown = coolDownTime;
                    _laserChargeTime = laserTime;
                }
            }

            if (_laserChargeTime > 0) {
                _beam();
                _laserChargeTime -= Time.deltaTime;
            } else {
                _lineRenderer.positionCount = 0;
                if (_coolDown > 0) {
                    _coolDown -= Time.deltaTime;
                }
            }
        }

        private void _beam() {
            _lineRenderer.positionCount = 2;

            var transformUp = _transform.up;
            var transformPosition = _transform.position;

            var size = Physics2D.RaycastNonAlloc(transformPosition, transformUp, _beamHits, range, enemyLayer);

            _lineRenderer.SetPosition(0, transformPosition);
            _lineRenderer.SetPosition(1, transformPosition + transformUp * range);

            for (var index = 0; index < size; index++) {
                // вызов GetComponent в Update уменьшает производительность
                // как вариант, можно кешировать компоненты в Dictionary<GameObjectInstanceID, Destroyable>
                // и для кеширования использовать Spawner
                var destroyable = _beamHits[index].collider.gameObject.GetComponent<Destroyable>();
                destroyable.Kill();
            }
        }

        [Inject]
        private void _init(IController controller) {
            _controller = controller;
        }
    }
}