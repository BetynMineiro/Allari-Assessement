using System.Net.Mime;
using Allari.NetCore.Application.Abstractions.Queries;
using Allari.NetCore.Application.Queries.ImageInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Allari.NetCore.API.Controllers;

[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AllariAssessementController : Controller
{
    private readonly IMediator _mediator;

    public AllariAssessementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("imageinfo/size/{size}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ImageInfoQueryResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> ImageInfoListInfo(int size, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ImageInfoQueryRequest() { NumberOfImages = size },
            cancellationToken);
        return Ok(response);
    }
}