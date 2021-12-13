using LEBON_Nathan_DM_IPI_2021_2022.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model
{
    static public class DamageFeatureDictionnary
    {
        static public Dictionary<string, List<String>> weaknessList = new Dictionary<string, List<String>>
        {
            { CharacterFeature.None.ToString(),new List<String>{ } },
            { CharacterFeature.Blessed.ToString(),new List<String>{DamageFeature.Unholy.ToString()} },
            { CharacterFeature.Cursed.ToString(),new List<String>{DamageFeature.Holy.ToString()} },
        };
    }
}
