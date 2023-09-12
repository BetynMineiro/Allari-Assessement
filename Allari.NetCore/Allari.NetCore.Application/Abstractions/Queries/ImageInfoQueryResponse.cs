namespace Allari.NetCore.Application.Abstractions.Queries;

public class ImageInfoQueryResponse
{
    public ImageInfoQueryResponse(string title, string url)
    {
        Tittle = title;
        Url = url;
    }

    public string Url { get; set; }

    public string Tittle { get; set; }
}