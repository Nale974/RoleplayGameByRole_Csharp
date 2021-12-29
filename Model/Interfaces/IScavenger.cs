using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IScavenger
    {
        public int currentLife { get; set; }
        int LifePointsWon(int deaths)
        {
            int lifePointsWon = 0;
            for (int j = 0; j < deaths; j++)
            {
                lifePointsWon =+ Utils.random.Next(50, 100);
            }
            currentLife += lifePointsWon;
            return lifePointsWon;
        }
    }
}
