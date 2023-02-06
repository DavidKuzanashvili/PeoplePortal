using People.API.Endpoints.People.Contracts;
using People.API.Filters;
using People.Application.People.Dtos;

namespace People.API.Endpoints.People;

internal static partial class EndpointsRegistrations
{
    internal static void RegisterPeopleEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("api")
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .WithTags("People")
            .WithOpenApi();

        root.MapGet("person/{personId}", GetPersonEndpoint.ExecuteAsync)
            .WithName("GetPerson")
            .Produces<GetPersonDto>(StatusCodes.Status200OK);

        root.MapGet("people", GetPeopleEndpoint.ExecuteAsync)
            .WithName("GetPeople")
            .Produces<GetPeopleDto>(StatusCodes.Status200OK);

        root.MapPost("person", CreatePersonEndpoint.ExecuteAsync)
            .WithName("CreatePerson")
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        root.MapPut("person", UpdatePersonEndpoint.ExecuteAsync)
            .WithName("UpdatePerson")
            .Produces<UpdatePersonDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        root.MapDelete("person/{personId}", DeletePersonEndpoint.ExecuteAsync)
            .WithName("DeletePerson")
            .Produces(StatusCodes.Status200OK);

        root.MapPost("upload-image/{personId}", UploadImageEndpoint.ExecuteAsync)
            .WithName("UploadImage")
            .Produces<string>(StatusCodes.Status200OK);

        root.MapPut("assign-person", AssingRelatedPeopleEndpoint.ExecuteAsync)
            .WithName("AssignPerson")
            .Produces<List<AssignedPersonItem>>(StatusCodes.Status200OK);

        root.MapGet("related-people-report/download", DownloadRelatedPeopleReportEndpoint.ExecuteAsync)
            .WithName("DownloadReportFile")
            .Produces<byte[]>(StatusCodes.Status200OK);
    }
}
