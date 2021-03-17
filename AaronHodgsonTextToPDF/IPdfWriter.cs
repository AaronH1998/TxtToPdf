using iText.Layout.Properties;

namespace AaronHodgsonTextToPDF
{
    public interface IPdfWriter
    {
        void AddToParagraph(string text);
        void CloseDocument();
        void CommitParagraph();
        void SetFontSize(float size);
        void Fill();
        void SetBold();
        void SetItalic();
        void Indent(int indentValue);
    }
}