namespace VisualiseR.Main
{
    public interface IMessage
    {
        MessageType Type { get; set; }
        string Title { get; set; }
        string Text { get; set; }
    }
}