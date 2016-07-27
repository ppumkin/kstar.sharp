using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kstar.sharp.domain.Extensions
{
    public static class HexToDecimalExtensions
    {
        // Disclaimer
        //  All these tools thanks to answers on StackOverflow. 
        //  Some may have been modified for the purpose of this project.


        /// <summary>
        /// Converts a HEX string into Decimal number. 
        /// </summary>
        /// <param name="hexValue">String must not contain dashes or any other seperators. Must not start with 0x.</param>
        /// <returns></returns>
        public static decimal HexToDecimal(this string hexValue)
        {
            long result = 0;
            long.TryParse(hexValue, System.Globalization.NumberStyles.HexNumber, null, out result);
            return result;
        }

        /// <summary>
        /// Converts a HEX string into human readable ASCII.
        /// </summary>
        /// <param name="hexString">String must not contain dashes or any other seperators</param>
        /// <returns></returns>
        public static string HexToASCII(this string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hs = hexString.Substring(i, 2);
                sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }
            return sb.ToString();
        }


        public static byte[] ToByteArray(this string value)
        {
            byte[] bytes = null;
            if (String.IsNullOrEmpty(value))
                bytes = new byte[0];
            else
            {
                int string_length = value.Length;
                int character_index = (value.StartsWith("0x", StringComparison.Ordinal)) ? 2 : 0; // Does the string define leading HEX indicator '0x'. Adjust starting index accordingly.               
                int number_of_characters = string_length - character_index;

                bool add_leading_zero = false;
                if (0 != (number_of_characters % 2))
                {
                    add_leading_zero = true;

                    number_of_characters += 1;  // Leading '0' has been striped from the string presentation.
                }

                bytes = new byte[number_of_characters / 2]; // Initialize our byte array to hold the converted string.

                int write_index = 0;
                if (add_leading_zero)
                {
                    bytes[write_index++] = FromCharacterToByte(value[character_index], character_index);
                    character_index += 1;
                }

                for (int read_index = character_index; read_index < value.Length; read_index += 2)
                {
                    byte upper = FromCharacterToByte(value[read_index], read_index, 4);
                    byte lower = FromCharacterToByte(value[read_index + 1], read_index + 1);

                    bytes[write_index++] = (byte)(upper | lower);
                }
            }

            return bytes;
        }

        //Solely used by ConvertToByteArray
        private static byte FromCharacterToByte(char character, int index, int shift = 0)
        {
            byte value = (byte)character;
            if (((0x40 < value) && (0x47 > value)) || ((0x60 < value) && (0x67 > value)))
            {
                if (0x40 == (0x40 & value))
                {
                    if (0x20 == (0x20 & value))
                        value = (byte)(((value + 0xA) - 0x61) << shift);
                    else
                        value = (byte)(((value + 0xA) - 0x41) << shift);
                }
            }
            else if ((0x29 < value) && (0x40 > value))
                value = (byte)((value - 0x30) << shift);
            else
                throw new InvalidOperationException(String.Format("Character '{0}' at index '{1}' is not valid alphanumeric character.", character, index));

            return value;
        }

       

    }
}
