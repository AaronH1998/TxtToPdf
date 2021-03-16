using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AaronHodgsonTextToPDF
{
    public class FileRepository
    {
        public string[] ReadSourceDocument(StreamReader reader)
        {
            var content = reader.ReadToEnd();

            var lines = content.Split('\n')
                .Select(p => p.TrimEnd(new char[] { '\r' }))
                .ToArray();

            return lines;
        }
    }
}
