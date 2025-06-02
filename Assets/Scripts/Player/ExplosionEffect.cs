using UnityEngine;

namespace Player
{
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] float duration = 0.15f;
        public float maxScale { get; set; }

        private SpriteRenderer spriteRenderer;
        private float timer = 0f;
        private Color startColor;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            startColor = spriteRenderer.color;
            transform.localScale = Vector3.zero;
        }

        void Update()
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            float scaleT = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(0.5f, 1f, t));
            
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * maxScale, scaleT);

            float fadeT = Mathf.SmoothStep(1f, 0f, Mathf.InverseLerp(0.8f, 1f, t));
            Color color = startColor;
            color.a = fadeT;
            spriteRenderer.color = color;

            if (t >= 1f)
                Destroy(gameObject);
        }
    }
}