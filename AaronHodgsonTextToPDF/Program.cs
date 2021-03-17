﻿using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.IO;

namespace AaronHodgsonTextToPDF
{
    class Program
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        static void Main(string[] args)
        {
            string outputDir = projectPath + "\\Output\\output.pdf";

            PdfWriter writer = new PdfWriter(outputDir);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            var commands = ReadSourceDocument();

            for (var x = 0; x < 10; x++)
            {
                
                var currentParagraph = new Paragraph();
                var currentText = new Text("");
               
                foreach (var unTrimmedCommand in commands)
                {
                    var command = unTrimmedCommand.TrimEnd(new char[] { '\r', '\n' });                  

                    if (command.StartsWith(".large"))
                    {
                        currentText.SetFontSize(20);
                    }
                    else if (command.StartsWith(".normal"))
                    {
                        currentText.SetFontSize(12);
                    }
                    else if (command.StartsWith(".italics"))
                    {
                        currentText.SetItalic();
                    }
                    else if (command.StartsWith(".fill"))
                    {
                        currentParagraph.SetTextAlignment(TextAlignment.JUSTIFIED);
                    }
                    else if (command.StartsWith(".nofill"))
                    {
                        currentParagraph.Add(currentText);
                        currentText = new Text("");

                        document.Add(currentParagraph);

                        currentParagraph = new Paragraph();

                        currentParagraph.SetTextAlignment(TextAlignment.LEFT);
                    }
                    else if (command.StartsWith(".bold"))
                    {
                        currentText.SetBold();
                    }
                    else if (command.StartsWith(".regular"))
                    {
                        //.regular command is handled when new Text is created.
                    }
                    else if (command.StartsWith(".indent"))
                    {
                        if (command.Contains("+"))
                        {
                            var indentValue = Convert.ToInt32(command.Substring(command.Length - 2));
                            currentParagraph.SetMarginLeft(indentValue * 20);
                        }
                        //indent reset is handled when new Paragraph is created.
                    }
                    else if (command.StartsWith(".paragraph"))
                    {
                        document.Add(currentParagraph);
                        currentParagraph = new Paragraph();
                    }
                    else
                    {
                        currentText.SetText(command);
                        currentParagraph.Add(currentText);
                        currentText = new Text("");
                    }
                }
                document.Add(currentParagraph);
            }
            
            document.Close();
        }

        static string[] ReadSourceDocument()
        {
            string inputDir = projectPath + "\\Input\\input.txt";
            
            using (StreamReader reader = new StreamReader(inputDir))
            {
                var content = reader.ReadToEnd();

                var commands = content.Split('\n');
                
                
                return commands;
            }
        }
    }
}