namespace Magix.Controller.Match
{
    using System.Collections.Generic;
    using UnityEngine;
    using View.Match;

    public class LifeBarController : MonoBehaviour
    {
        [field: SerializeField]
        private LifePointView LifePointPrefab { get; set; }

        private List<LifePointView> LifePoints { get; set; }

        public void Initialize(int lifePoints)
        {
            LifePoints = new List<LifePointView>();

            for (int i = 0; i < lifePoints; i++)
            {
                LifePointView lifePoint = Instantiate(LifePointPrefab, gameObject.transform);

                LifePoints.Add(lifePoint);
            }

            UpdateLife(lifePoints);
        }

        public void UpdateLife(int lifePoints)
        {
            LifePoints.ForEach(l => l.Enable(false));

            if (lifePoints == 0)
                return;

            for (int i = 0; i < lifePoints; i++)
            {
                LifePoints[i].Enable(true);
            }
        }
    }
}
