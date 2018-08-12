namespace Workflows

module BusinessLogic =
    type CreateAcivityHandlerFn = Infrastructure.UserRepositoryTypes.FindUserById -> DTOs.ActivityCreateCommand -> Domain.Entities.Activity

    let createAcivityHandler: CreateAcivityHandlerFn = 
        let rec _addUsersToActivityFn userList (addUserFn: Domain.Entities.User -> Domain.Entities.Activity -> Domain.Entities.Activity) sourceActivity =
            match userList with
                | user::others -> 
                    sourceActivity 
                    |> addUserFn user 
                    |> _addUsersToActivityFn others addUserFn
                | [] -> sourceActivity

        fun findUserByIdFn command ->
            let userIdNotFoundErrorFn forRole userId = sprintf "%s with id=%d not found in storage" forRole userId

            let getUserListFromUserIdList forRole userIdList = 
                userIdList 
                |> List.map (fun userId -> 
                                match findUserByIdFn userId with 
                                | Some user -> user 
                                | None -> raise (new System.ApplicationException (userIdNotFoundErrorFn forRole userId)))
            
            let activityCreatorFound = findUserByIdFn command.CreatorId
            let participantList = getUserListFromUserIdList "Participant" command.ParticipantIds
            let reviewerList = getUserListFromUserIdList "Reviewer" command.ReviewerIds

            match activityCreatorFound with
                | Some activityCreator -> 
                    Domain.ActivityLogic.createActivity command.Name activityCreator
                    |> _addUsersToActivityFn participantList Domain.ActivityLogic.addParticipant
                    |> _addUsersToActivityFn reviewerList Domain.ActivityLogic.addReviewer
                | None -> raise (new System.ApplicationException (userIdNotFoundErrorFn "Activity Creator" command.CreatorId))