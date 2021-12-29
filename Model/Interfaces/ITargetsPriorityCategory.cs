using LEBON_Nathan_DM_IPI_2021_2022.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEBON_Nathan_DM_IPI_2021_2022.Model.Interfaces
{
    interface ITargetsPriorityCategory
    {
        public CharacterCategory targetsPriorityCategory { get; set;}

        public List<Character> CharactersAliveTargetsPriorityCategory(List<Character> characters)
        {
            List<Character> resultList = new List<Character>();
            foreach (Character character in characters)
            {
                if (character != this && character.currentLife > 0 && character.characterCategory == targetsPriorityCategory)
                {
                    resultList.Add(character);
                }
            }
            return resultList;
        }
    }
}
