using Allari.NetCore.Application.Abstractions.Queries;
using MediatR;

namespace Allari.NetCore.Application.Queries.ImageInfo;

public class ImageInfoQueryRequest : IRequest<List<ImageInfoQueryResponse>>
{
    public int NumberOfImages { get; set; }
}