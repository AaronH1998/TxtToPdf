using System;
using System.IO;
using System.Linq;

namespace AaronHodgsonTextToPDF
{
    public class GearsetSerialiser
    {
        //As this is a non standard file format.
        public string[] Deserialise(string content)
        {
            if (content == null) 
                throw new ArgumentNullException();

            var lines = content.Split('\n')
                .Select(p => p.TrimEnd(new char[] { '\r' }))
                .ToArray();

            return lines;
        }
    }
}
