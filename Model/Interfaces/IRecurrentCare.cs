using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IRecurrentCare
    {
        int currentLife { get; set; }
        int maximumLife { get; set; }
        double recurrentCarePercent { get; set; }

        void Care()
        {
            currentLife += (int)(recurrentCarePercent * maximumLife);
        }
    }
}
