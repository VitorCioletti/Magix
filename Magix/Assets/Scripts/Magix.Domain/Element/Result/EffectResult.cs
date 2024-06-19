namespace Magix.Domain.Element.Result
{
    using System;
    using Interface.Element.Result;

    public class EffectResult : IEffectResult
    {
        public bool Blinded { get; }

        public bool Stunned { get; }

        public int DamageTaken { get; }

        public bool TookDamage => DamageTaken > 0;

        public EffectResult(
            bool blinded,
            bool stunned,
            int damageTaken)
        {
            Blinded = blinded;
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
