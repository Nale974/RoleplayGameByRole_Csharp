using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IAllLifeLostForDamage
    {
        public int maximumLife { get; set; }
        public int currentLife { get; set; }
        int CalculLifeLost()
        {
            return maximumLife - currentLife;
        }
    }
}
