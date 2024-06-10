namespace Tests.NatureElements
{
    using Magix.Domain.Interface;
    using Magix.Domain.Interface.NatureElements;
    using Magix.Domain.Interface.NatureElements.Result;
    using NSubstitute;
    using NUnit.Framework;

    public class WindTests
    {
        [Test]
        public void WindMustClearAllWizardDebuffs()
        {
            var wizard = Substitute.For<IWizard>();
            var wind = Substitute.For<IWind>();

            wizard.CanAttack.Returns(true);
            wizard.CanMove.Returns(true);
            wizard.CanPush.Returns(true);

            var expectedEffectResult = Substitute.For<IEffectResult>();

            expectedEffectResult.Blinded.Returns(false);
            expectedEffectResult.Stunned.Returns(false);
            expectedEffectResult.TookDamage.Returns(false);
            expectedEffectResult.DamageTaken.Returns(0);

            IEffectResult effectResult = wind.ApplyElementEffect(wizard);

            Assert.AreEqual(expectedEffectResult.Blinded, effectResult.Blinded);
            Assert.AreEqual(expectedEffectResult.Stunned, effectResult.Stunned);
            Assert.AreEqual(expectedEffectResult.TookDamage, effectResult.TookDamage);
            Assert.AreEqual(expectedEffectResult.DamageTaken, effectResult.DamageTaken);
        }
    }
}
