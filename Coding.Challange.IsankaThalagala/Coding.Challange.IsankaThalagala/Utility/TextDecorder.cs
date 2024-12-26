using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coding.Challange.IsankaThalagala.Utility
{
    public static class TextDecorder
    {
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "????";

            StringBuilder output = new StringBuilder();
            input = input.Trim();
            if (input.EndsWith('#'))// remove the last # charactor
                input = input.Remove(input.Length - 1, 1);

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);// split by space
            foreach (string part in parts)
            {
                var segment = part;
                if (string.IsNullOrWhiteSpace(segment))
                    continue;

                // Backspace function
                if (segment.Contains("*"))
                    BackspaceFunction(ref segment);


                var currentKey = segment[0]; // get the 1st key
                var pressedCount = 1; // set the pressedCount as 1 because part[0] is already 1

                // loop all the charactors in the part to identify the all keys in the current part. starting with 1 idex to skip the 1st item in the part.
                for (int i = 1; i < segment.Length; i++)
                {
                    var test = segment[i];// todo : remove
                    if (segment[i] == currentKey && (pressedCount + 1) <= ButtonMapper.ButtonMapping[currentKey.ToString()].Length)
                        pressedCount++; // same key pressed and pressed count less than or equel to currentKey's assined letters count. (eg: 6-> MNO=3 , 7-> PQRS = 4)
                    else
                    {
                        // Decode the last charactor (-1 index) when user pressed a different key    
                        output.Append(GetExactLetter(currentKey.ToString(), pressedCount - 1));

                        // Skip invalid charactors, (direct input can include invalid charactors.)
                        if (!Regex.IsMatch(segment[i].ToString(), @"^\d+$"))
                        {
                            output.Append(segment[i].ToString());
                            pressedCount = 0;
                            continue;
                        }

                        // reset to capture the next key
                        currentKey = segment[i];
                        pressedCount = 1;

                        if (i == segment.Length - 1)
                            // DEcode the current letter when is  last charactor of the part. 
                            output.Append(GetExactLetter(currentKey.ToString(), pressedCount - 1));
                    }
                }

                // if user pressed single time or multiple times in same letter but less than 1 seconds delay.. eg : 33 or 8
                if (segment.Length == 1 || pressedCount > 1)
                    output.Append(GetExactLetter(currentKey.ToString(), pressedCount - 1));

            }

            return output.ToString();
        }

        private static void BackspaceFunction(ref string segment)
        {

            int asteriskCount = segment.Count(c => c == '*');
            while (asteriskCount > 0)
            {
                int asteriskIndex = segment.IndexOf("*");
                if (asteriskIndex > 0)
                {
                    //* is not the first character
                    segment = segment.Remove(asteriskIndex - 1, 2); // Remove the charactor before * and the * it self

                }
                else if (asteriskIndex == 0)
                {
                    //* is the first character.;
                    segment = segment.Remove(0, 1);// remove the * from the start index
                }

                /*
                  sample possibilites:
                    *
                    **454
                    4454*
                    *454**454*                 
                */

                asteriskCount = segment.Count(c => c == '*');
                if (asteriskCount > 0)
                    BackspaceFunction(ref segment);// call same function untill all the * removed.
            }
        }

        private static char GetExactLetter(string currentKey, int pressedCount)
        {
            var charactors = ButtonMapper.ButtonMapping[currentKey.ToString()]; // get the assigned letters for the key. 
            return charactors[pressedCount % charactors.Length];// get the exact letter for the pressed count


        }
    }
}
