using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AaronHodgsonTextToPDF
{
    public class FileRepository : IFileRepository
    {
        public string[] GetLines(string inputDir)
        {
            if (string.IsNullOrEmpty(inputDir))
                throw new ArgumentException();

            using (StreamReader reader = new StreamReader(inputDir))
            {
                var content = reader.ReadToEnd();
                var serialiser = new GearsetSerialiser();
                return serialiser.Deserialise(content);
            }
        }
    }
}
