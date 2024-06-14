namespace Magix.Domain.Interface.NatureElements
{
    using System;

    [Flags]
    public enum ElementEffect
    {
        None,
        OnFire,
        Wet,
        Dry,
        Blind,
        Shocked,
    }
}
