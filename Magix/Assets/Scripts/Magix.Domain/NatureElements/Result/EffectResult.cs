namespace Magix.Domain.NatureElements.Result
{
    using System;
    using Interface.NatureElements.Result;

    public class EffectResult : IEffectResult
    {
        public bool Blinded { get; }

        public bool TookDamage { get; }

        public bool Stunned { get; }

        public int DamageTaken { get; }

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

        public override bool Equals(object obj)
        {
            if (obj is not IEffectResult effectResult)
                return false;

            return Blinded == effectResult.Blinded && TookDamage == effectResult.TookDamage &&
                   Stunned == effectResult.Stunned && DamageTaken == effectResult.DamageTaken;
        }

        public override int GetHashCode() => HashCode.Combine(
            Blinded,
            TookDamage,
            Stunned,
            DamageTaken);
    }
}
