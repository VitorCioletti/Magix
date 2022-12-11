namespace Magix.View.Match.Wizard
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DG.Tweening;
    using UnityEngine;

    public class WizardView : MonoBehaviour
    {
        [field: SerializeField]
        private Animator _animator { get; set; }

        private readonly Vector3 _positionOffset = new(0.15f, 0.7f, 0);

        public void Initialize(Transform transformToSpawn)
        {
            transform.position = transformToSpawn.position + _positionOffset;
        }

        public void AnimateIdle()
        {
            _animator.Play("Idle");
        }

        public async Task AnimateMoveAsync(List<Transform> transformsToMove)
        {
            Sequence sequence = DOTween.Sequence();

            float moveSpeed = 0.2f;

            foreach (Transform transformToMove in transformsToMove)
            {
                Vector3 transformToMovePosition = transformToMove.position;

                sequence.Append(transform.DOMove(transformToMovePosition + _positionOffset, moveSpeed));
            }

            sequence.OnStart(_animateMove);
            sequence.OnComplete(AnimateIdle);

            await sequence.Play().AsyncWaitForCompletion();
        }

        private void _animateMove()
        {
            _animator.Play("Run");
        }
    }
}
