namespace Domain

module ActivityLogic =
    open Entities

    let createActivity name creator =
        { Id = 0
          Name = name
          Creator = creator
          Participants = []
          Reviewers = []
          CreateDate = System.DateTime.Now }

    let addParticipant participant activity = 
        { activity with Participants = activity.Participants @ [ participant ] }

    let addReviewer reviewer activity = 
        { activity with Reviewers = activity.Reviewers @ [ reviewer ] }

    let userRolesInActivity activity user =
        let userIsHeadCoach = user.IsHeadCoach
        let userIsActivityCreator = activity.Creator = user
        let userIsParticipant = List.exists (fun u -> u = user) activity.Participants
        let userIsReviewer = List.exists (fun u -> u = user) activity.Reviewers

        let rolesForHeadCoach roles = if userIsHeadCoach then roles @ [HeadCoach user] else roles
        let rolesForAcivityCreator roles = if userIsActivityCreator then roles @ [ActivityCreator user] else roles
        let rolesForParticipant roles = if userIsParticipant then roles @ [Participant user] else roles
        let rolesForReviewer roles = if userIsReviewer then roles @ [Reviewer user] else roles

        [] |> rolesForHeadCoach |> rolesForAcivityCreator |> rolesForParticipant |> rolesForReviewer

    let isUserAHeadCoach roles =
        roles |> List.exists (fun role -> match role with | HeadCoach _ -> true | _ -> false )

    let isUserAnActivityCreator roles =
        roles |> List.exists (fun role -> match role with | ActivityCreator _ -> true | _ -> false )

    let isUserAParticipant roles =
        roles |> List.exists (fun role -> match role with | Participant _ -> true | _ -> false )

    let isUserAReviewer roles =
        roles |> List.exists (fun role -> match role with | Reviewer _ -> true | _ -> false )