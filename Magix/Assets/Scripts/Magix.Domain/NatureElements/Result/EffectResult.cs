namespace Magix.Domain.NatureElements.Result
{
    using Interface.NatureElements.Result;

    public class EffectResult : IEffectResult
    {
        public bool Blinded { get; private set; }

        public bool TookDamage { get; private set; }

        public bool Stunned { get; private set; }

        public int DamageTaken { get; private set; }

        public EffectResult(
            bool blinded,
            bool tookDamage,
            bool stunned,
            int damageTaken)
        {
            Blinded = blinded;
            TookDamage = tookDamage;
            Stunned = stunned;
            DamageTaken = damageTaken;

        }
    }
}
