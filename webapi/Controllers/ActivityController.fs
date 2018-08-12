namespace webapi.Controllers

open Microsoft.AspNetCore.Mvc

[<Route("api/[controller]")>]
[<ApiController>]
type ActivityController () =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Post([<FromBody>] activity: webapi.Models.CreateActivityModel) =
        let findUserFn = Infrastructure.InMemoryUserRepository.findUserByIdInMemoryList
        let createdActivity = 
            webapi.Models.Logic.mapToCreateCommand activity
            |> Workflows.BusinessLogic.createAcivityHandler findUserFn
        ActionResult<Domain.Entities.Activity>(createdActivity)