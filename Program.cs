﻿using LEBON_Nathan_DM_IPI_2021_2022.Model;
using LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEBON_Nathan_DM_IPI_2021_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            Tournament tournament = new Tournament();
            tournament.Run();
        }
    }
}
