using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface IIncreasedTotalAttackNumber
    {
        public int totalAttackNumber { get; set; }
        public int totalAttackNumberIncrease { get; set; }
        public double totalAttackNumberLimit { get; set; }
    }
}
