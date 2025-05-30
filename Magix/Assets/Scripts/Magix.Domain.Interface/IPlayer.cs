﻿namespace Magix.Domain.Interface
{
    using System;
    using System.Collections.Generic;

    public interface IPlayer
    {
        Guid Id { get; }

        int Number { get; }

        List<IWizard> Wizards { get; }

        bool HasRemainingActions { get; }
    }
}
