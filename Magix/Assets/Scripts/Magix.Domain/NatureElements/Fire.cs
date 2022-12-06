namespace Magix.Domain.NatureElements
{
    using Interface.NatureElements;

    public class Fire : BaseNatureElement, IFire
    {
        public override NatureElementEffect Effect => NatureElementEffect.OnFire;

        public override INatureElement ApplyNatureElement(INatureElement natureElement)
        {
            INatureElement resultanteNatureElement = null;

            switch (natureElement)
            {
                case Water:
                    resultanteNatureElement = new Smoke();
                    break;
            }

            return resultanteNatureElement;
        }
    }
}
