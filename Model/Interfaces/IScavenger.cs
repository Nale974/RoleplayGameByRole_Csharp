using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IScavenger
    {
        int LifePointsWon()
        {
            return Utils.random.Next(50, 100);
        }
    }
}
