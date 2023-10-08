namespace Magix.Domain.Interface.NatureElements.Result
{
    public interface IEffectResult
    {
        public bool Blinded { get; }

        public bool TookDamage { get; }

        public bool Stunned { get; }

        public int DamageTaken { get; }
    }
}
