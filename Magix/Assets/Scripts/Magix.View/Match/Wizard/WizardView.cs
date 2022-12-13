namespace Magix.View.Match.Wizard
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DG.Tweening;
    using UnityEngine;

    public class WizardView : MonoBehaviour
    {
        [field: SerializeField]
        private Animator _animator { get; set; }

        [field: SerializeField]
        private SpriteRenderer _spriteRenderer { get; set; }

        private readonly Vector3 _positionOffset = new(0.05f, 0.6f, 0);

        public void Initialize(Transform transformToSpawn)
        {
            transform.position = transformToSpawn.position + _positionOffset;
        }

        public void AnimateIdle()
        {
            _animator.Play("Idle");
        }

        public async Task AnimateCastAsync()
        {
            Sequence sequence = DOTween.Sequence();

            string animationName = "Casting";

            float castingTime = _getAnimationTime(animationName);

            sequence.SetDelay(castingTime);

            sequence.OnStart(OnSequenceStart);
            sequence.OnComplete(AnimateIdle);

            sequence.Play();

            await sequence.AsyncWaitForCompletion();

            void OnSequenceStart()
            {
                _animator.Play(animationName);
            }
        }

        public async Task AnimateMoveAsync(List<Transform> transformsToMove)
        {
            Sequence sequence = DOTween.Sequence();

            const float moveSpeed = 0.2f;

            foreach (Transform transformToMove in transformsToMove)
            {
                Vector3 transformToMovePosition = transformToMove.position;

                sequence.AppendCallback(() => CalculateFlip(transformToMove));
                sequence.Append(transform.DOMove(transformToMovePosition + _positionOffset, moveSpeed));
            }

            sequence.OnStart(OnSequeceStart);
            sequence.OnComplete(AnimateIdle);

            await sequence.Play().AsyncWaitForCompletion();

            void CalculateFlip(Transform target)
            {
                Vector3 targetPosition = target.position;
                Vector3 wizardPosition = transform.position;

                _spriteRenderer.flipX = targetPosition.x < wizardPosition.x;
            }

            void OnSequeceStart()
            {
                _animator.Play("Running");
            }
        }

        private float _getAnimationTime(string animation)
        {
            AnimationClip[] animations = _animator.runtimeAnimatorController.animationClips;

            return animations.First(a => a.name == animation).length;
        }
    }
}
