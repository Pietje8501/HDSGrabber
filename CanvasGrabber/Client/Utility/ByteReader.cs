using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanvasGrabber.Assets;

namespace CanvasGrabber.Client.Utility
{
    class ByteReader
    {
        private int position;
        private byte[] content;

        /// <summary>
        /// Constructor with string parameter. The string is converted to a byte array
        /// </summary>
        /// <param name="c">The string with the bytecontent</param>
        public ByteReader(string c)
        {
            byte[] bytes = new byte[c.Length * sizeof(char)];
            System.Buffer.BlockCopy(c.ToCharArray(), 0, bytes, 0, bytes.Length);
            content = bytes;
        }

        /// <summary>
        /// Alternative constructor using a byte array as parameter
        /// </summary>
        /// <param name="c">Byte array with the content</param>
        public ByteReader(byte[] c)
        {
            this.content = c;
        }

        /// <summary>
        /// Returns the current pointer in the content
        /// </summary>
        /// <returns>Returns an integer with position in the content</returns>
        public int GetPosition()
        {
            return this.position;
        }

        /// <summary>
        /// Changes the pointer in the content to a provided integer
        /// </summary>
        /// <param name="pos">The new pointer value in the content</param>
        public void SetPosition(int pos)
        {
            if (pos >= 0 && pos < content.Length)
            {
                this.position = pos;
            }
            else
            {
                throw new System.ArgumentException("Position is not in the right range.");
            }
        }

        /// <summary>
        /// Advance pointer in the content by using positive offset or set it back
        /// by using a negative offset
        /// </summary>
        /// <param name="offset">The offset over which the pointer is changed</param>
        /// <returns>Returns the new position as a result</returns>
        public int OffsetPointer(int offset)
        {
            if (this.position + offset >= 0 && this.position + offset < this.content.Length)
            {
                this.position += offset;
                return this.position;
            }
            else
            {
                throw new System.ArgumentException("Pointer exceeds content boundaries.");
            }
            
        }

        /// <summary>
        /// Searches for a string, marked at the end by an EOF character.
        /// Position is updated while fetching a new character
        /// </summary>
        /// <returns>Returns the found string</returns>
        public string GetString()
        {
            StringBuilder b = new StringBuilder();
            while (b.ToString()[b.ToString().Length - 1] != Constants.EOF)
            {
                b.Append(GetString(1));
            }
            return b.ToString();
        }

        /// <summary>
        /// Converts a byte array from a given length to a string
        /// </summary>
        /// <param name="length">The length of the requested string</param>
        /// <returns>Returns the string with the length we wanted</returns>
        public String GetString(int length)
        {
            byte[] contentSub = GetSubArray(length);
            this.position += length;
            char[] chars = new char[contentSub.Length / sizeof(char)];
            System.Buffer.BlockCopy(contentSub, 0, chars, 0, contentSub.Length);
            return new string(chars);
        }

        /// <summary>
        /// Converts a byte array of given length to an integer of 32 bits
        /// </summary>
        /// <param name="length">The length of the requested integer (in bytes)</param>
        /// <returns>Returns a 32-bit integer</returns>
        public int GetInt32(int length)
        {
            byte[] subResult = GetSubArray(length);
            this.position += length;
            if (BitConverter.IsLittleEndian)
                Array.Reverse(subResult);
            return BitConverter.ToInt32(subResult, 0);
        }

        /// <summary>
        /// Converts a byte array of given length to an integer of 64 bits
        /// </summary>
        /// <param name="length">The length of the requested integer (in bytes)</param>
        /// <returns>Returns a 64-bit integer</returns>
        public int GetInt64(int length)
        {
            byte[] subResult = GetSubArray(length);
            this.position += length;
            if (BitConverter.IsLittleEndian)
                Array.Reverse(subResult);
            return (int)BitConverter.ToInt64(subResult, 0);
        }

        /// <summary>
        /// Returns a part of the content, starting at the current position and 
        /// with a length as parameter. Position pointer should be updated in 
        /// calling method
        /// </summary>
        /// <param name="length">The length of the requested subarray</param>
        /// <returns>Returns a subarray of the content with requested length</returns>
        public byte[] GetSubArray(int length)
        {
            byte[] result = new byte[length];
            for (int i = this.position; i < this.position + length; i++)
            {
                result[i - this.position] = content[i];
            }
            return result;
        }
    }
}
