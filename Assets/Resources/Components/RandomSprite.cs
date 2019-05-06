using UnityEngine;

namespace Resources.Components {
    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomSprite : MonoBehaviour {
        public Sprite[] sprites;

        private SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _setRandomSprite();
        }

        private void OnEnable() {
            _setRandomSprite();
        }

        private void _setRandomSprite() {
            if (sprites.Length <= 0) {
                return;
            }

            var indes = Random.Range(0, sprites.Length);
            _spriteRenderer.sprite = sprites[indes];
        }
    }
}