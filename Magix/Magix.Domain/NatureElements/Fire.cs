namespace Magix.Domain.NatureElements
{
    using System;

    public class Fire : BaseNatureElement
    {
        protected override NatureElementEffect Effect => NatureElementEffect.OnFire;

        public override BaseNatureElement ApplyNatureElement(BaseNatureElement natureElement)
        {
            BaseNatureElement newNatureElement = null;

            switch (natureElement)
            {
                case Water:
                    newNatureElement = new Smoke();
                    break;
            }

            return newNatureElement;
        }
    }
}
