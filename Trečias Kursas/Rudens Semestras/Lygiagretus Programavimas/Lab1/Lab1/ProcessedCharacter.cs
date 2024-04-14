using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{

    internal class ProcessedCharacter : Character 
    {
        public long computedData;

        public ProcessedCharacter(Character character, long processedData) : base(character.Name, character.Age, character.Power)
        {
            computedData = processedData;
        }
        public override string ToString()
        {
            return String.Format("| {0,-35} | {1,-7} | {2,-10} | {3,-10} |",
                Name, Age, Power, computedData);
        }
    }
}
