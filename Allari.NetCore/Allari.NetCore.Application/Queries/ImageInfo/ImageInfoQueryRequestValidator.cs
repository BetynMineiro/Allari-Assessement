using Allari.NetCore.Application.Abstractions.Queries;
using FluentValidation;

namespace Allari.NetCore.Application.Queries.ImageInfo;

public class ImageInfoQueryRequestValidator : AbstractValidator<ImageInfoQueryRequest>
{
    public ImageInfoQueryRequestValidator()
    {
        RuleFor(x => x.NumberOfImages).NotEmpty();
    }
}