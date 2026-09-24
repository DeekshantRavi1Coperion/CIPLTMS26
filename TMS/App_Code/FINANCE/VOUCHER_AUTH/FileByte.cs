public class FileByte
{
    public string mainFileName { get; set; }
    public string supportingFileName { get; set; }
    public string pdfCopyFileName { get; set; }

    public byte[] mainBytes { get; set; }
    public byte[] supportingBytes { get; set; }
    public byte[] pdfCopyBytes { get; set; }
}