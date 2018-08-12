namespace webapi.Models

type CreateActivityModel = {
        Name: string
        CreatorId: int
        ParticipantIds: int list
        ReviewerIds: int list
    }

module Logic =
    open Workflows.DTOs

    let mapToCreateCommand (activity: CreateActivityModel) = 
        { Name = activity.Name
          CreatorId = activity.CreatorId
          ParticipantIds = activity.ParticipantIds
          ReviewerIds = activity.ReviewerIds }