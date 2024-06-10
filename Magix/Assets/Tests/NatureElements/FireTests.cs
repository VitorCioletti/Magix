namespace Tests.NatureElements
{
    using Magix.Domain;
    using Magix.Domain.Board;
    using Magix.Domain.NatureElements;
    using Magix.Domain.NatureElements.Result;
    using NUnit.Framework;

    public class FireTests
    {
        [Test]
        public void FireMustMixWithNatural()
        {
            var fire = new Fire();
            var natural = new Natural();

            var expectedElement = fire;

            var mixedElement = natural.GetMixedElement(fire);

            Assert.AreEqual(expectedElement, mixedElement);
        }

        [Test]
        public void FireMustNotBlockPassage()
        {
            var fire = new Fire();

            Assert.AreEqual(fire.Blocking, false);
        }

        [Test]
        public void FireMustMixWithWaterCreatingSmoke()
        {
            var fire = new Fire();
            var water = new Water();

            var expectedElement = new Smoke();

            var mixedElement = water.GetMixedElement(fire);

            Assert.AreEqual(expectedElement.GetType(), mixedElement.GetType());
        }

        [Test]
        public void FireMustDamageWizard()
        {
            var position = new Position(0, 1);
            var wizard = new Wizard(position);

            var fire = new Fire();

            var expectedEffectResult = new EffectResult(false, false, Fire.Damage);

            var expectedLifeAfterDamage = wizard.LifePoints - Fire.Damage;

            var effectResult = fire.ApplyElementEffect(wizard);

            Assert.AreEqual(expectedLifeAfterDamage, wizard.LifePoints);

            Assert.AreEqual(expectedEffectResult.Blinded, effectResult.Blinded);
            Assert.AreEqual(expectedEffectResult.Stunned, effectResult.Stunned);
            Assert.AreEqual(expectedEffectResult.DamageTaken, effectResult.DamageTaken);
            Assert.AreEqual(expectedEffectResult.TookDamage, effectResult.TookDamage);
        }
    }
}
