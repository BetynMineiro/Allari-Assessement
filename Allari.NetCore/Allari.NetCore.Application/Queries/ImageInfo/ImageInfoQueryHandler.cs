using Allari.NetCore.Application.Abstractions.Queries;
using MediatR;

namespace Allari.NetCore.Application.Queries.ImageInfo;

public class ImageInfoQueryHandler : IRequestHandler<ImageInfoQueryRequest, List<ImageInfoQueryResponse>?>
{
    public Task<List<ImageInfoQueryResponse>?> Handle(ImageInfoQueryRequest request,
        CancellationToken cancellationToken)
    {
        var listImages = new List<ImageInfoQueryResponse>();

        for (int i = 0; i < request.NumberOfImages; i++)
        {
            var title = $"Image {i + 1} by picsum photos";
            listImages.Add(new ImageInfoQueryResponse(title, GenerateRandomImage()));
        }

        return Task.FromResult(listImages)!;
    }

    private string GenerateRandomImage()
    {
        var random = new Random();
        var id = random.Next(1, 1000);
        return $"https://picsum.photos/600/600?random={id}";
    }
}