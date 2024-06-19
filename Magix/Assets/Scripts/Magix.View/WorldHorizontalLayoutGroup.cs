namespace Magix.View
{
    using UnityEngine;

    [ExecuteAlways]
    public class WorldHorizontalLayoutGroup : MonoBehaviour
    {
        [field: SerializeField]
        private float _spacing;

        private void OnTransformChildrenChanged()
        {
            UpdateChildrenPositions();
        }

        private void OnValidate()
        {
            UpdateChildrenPositions();
        }

        private void UpdateChildrenPositions()
        {
            SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

            float currentX = CalculateInitialPosition(renderers);

            foreach (SpriteRenderer spriteRenderer in renderers)
            {
                float width = spriteRenderer.bounds.size.x;

                spriteRenderer.gameObject.transform.localPosition = new Vector3(currentX + width / 2, 0, 0);

                currentX += width + _spacing;
            }
        }

        private float CalculateInitialPosition(SpriteRenderer[] renderers)
        {
            float totalWidth = 0f;

            foreach (SpriteRenderer spriteRenderer in renderers)
            {
                totalWidth += spriteRenderer.bounds.size.x;
            }

            totalWidth += _spacing * (renderers.Length - 1);

            return -totalWidth / 2;
        }
    }
}
