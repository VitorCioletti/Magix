namespace Magix.View.Match.Board
{
    using UnityEngine;

    public class NatureElementView : MonoBehaviour
    {
        [field: SerializeField]
        private SpriteRenderer _spriteRenderer { get; set; }

        [field: SerializeField]
        private Animator _animator { get; set; }

        public void Initialize()
        {
            _spriteRenderer.gameObject.SetActive(false);
            _animator.runtimeAnimatorController = null;
        }

        public void UpdateNatureElement(RuntimeAnimatorController runtimeAnimator)
        {
            _spriteRenderer.gameObject.SetActive(runtimeAnimator != null);
            _animator.runtimeAnimatorController = runtimeAnimator;
        }
    }
}
