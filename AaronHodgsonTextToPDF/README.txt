AaronHodgsonTextToPdf is a Console application designed to take an input text file with commands and text and convert them in to a formatted pdf document.

The application requires two paramters:
	1. The location of the input text file.
	2. The destination of the output pdf file.
Please edit these before running the application.

The input text file is formatted as commands/text seperated by a new line. The program then splits the content into an array of strings to be processed by the controller and applied to a pdf file using iText7.

The program creates the pdf in the following way:
	1. Create a Document.
	2. Start a new Text and new Paragraph.
	3. If the line is a style command then apply it to the text.
	4. If the line is not one of the commands listed bellow then set this text to the Text object and add it to the current paragraph. Then start a new Text object.
	5. If the line is a .paragraph command, add the current paragraph to the document and start a new Paragraph object.

The converter currently translates the following commands:
	1. .large: Set text Font size to 20.
	2. .regular: Set text Font size to 12.
	3. .italics: Set Font style to italic.
	4. .fill: Set Paragraph text alignment to Justified.
	5. .nofill: Reset text alignment.
		> note: This is performed by creating beginning a new paragraph (as .paragraph does) as to meet the expected output.
	6. .bold: Set Font weight to bold.
	7. .regular: Set Font weight to normal. 
		note: This command is accepted but swallowed as the logic for it is already completed whenever a new Text object is created.
	8. .indent <number>: Set indent to a multiplier of the width of "WWWWW".
		> note: Negative indents are swallowed as it was intepreted that they were meant to reset the indent but this is already completed when a new Paragraph is created (see limitations below).
	9. .paragraph: Add the current paragraph and start a new paragraph.

Limitations of this program:
	1. Stylings are not carried over when applied to one Text in a Paragraph. I took this approach as it meets the minimum requirements of the provided text file but may cause issues with other formats. For example, if one wanted
to apply italics to "a quick brown fox" but apply bold only to the word "quick", the commands would have to be ".italics\na\n.italics\n.bold\nquick\n.italics\nbrown fox".
	2. The provided input does not agree with the way iText7 handles Indentation (.indent <number>) and Text Alignment(.fill and .nofill). iText7 removed indenting entire Paragraphs so a margin was applied to the paragraph. 
A paragraph with one Text with Justified Alignment and another Text with Left Alignment will ignore the Justified Alignment. These two points mean that a new Paragraph must be created to correctly apply styling.
	3. I intepreted the command ".indent -2" to be resetting the previous indent command "indent +2". However, due to how stylings are reset when creating new paragraphs, this will not reset the indentation, but instead push the next
paragraph 2 units behind the margin which conflicts with the expected output. This means that if one wanted to indent one paragraph 2 more than the last indented paragraph then the commands would have to be 
".indent +2\nFirst Paragraph\n.paragraph\n.indent +4\nSecond Paragraph".

What could be improved:
	1. The unit tests in this solution mainly measure the number of times a method was called. Preferably they would measure the actual output but due to how this program changes the state on each conversion, this is difficult.
An alternative approach would be to split the input content into paragraphs rather than individual lines so a method returns a testable Paragraph output.
	2. I have null arguement checks for applicable methods. Range of number paramater tests could be added for more confidence in methods that use numbers.