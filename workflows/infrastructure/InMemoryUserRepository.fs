namespace Infrastructure

module InMemoryUserRepository =

    let private _userList =
        let headCoach = Domain.UserLogic.createUser 1 "The Head Coach" true None
        let anActivityCreator1 = Domain.UserLogic.createUser 2 "An Activity Creator 01" false (Some headCoach)
        let anActivityCreator2 = Domain.UserLogic.createUser 3 "An Activity Creator 01" false (Some headCoach)
        let manager1 = Domain.UserLogic.createUser 4 "Manager 01" false None
        let manager2 = Domain.UserLogic.createUser 5 "Manager 02" false None
        let participant1 = Domain.UserLogic.createUser 6 "Participant 01" false (Some manager1)
        let participant2 = Domain.UserLogic.createUser 7 "Participant 02" false (Some manager1)
        let participant3 = Domain.UserLogic.createUser 8 "Participant 03" false (Some manager2)
        let participant4 = Domain.UserLogic.createUser 9 "Participant 04" false (Some manager2)
        let participant5 = Domain.UserLogic.createUser 10 "Participant 05" false None
        let reviewer1 = Domain.UserLogic.createUser 11 "Reviewer 01" false (Some manager1)
        let reviewer2 = Domain.UserLogic.createUser 12 "Reviewer 02" false None
        let reviewer3 = Domain.UserLogic.createUser 13 "Reviewer 03" false (Some manager2)
        [ headCoach
          anActivityCreator1; anActivityCreator2
          manager1; manager2
          participant1; participant2; participant3; participant4; participant5
          reviewer1; reviewer2; reviewer3 ]

    let findUserByIdInMemoryList: UserRepositoryTypes.FindUserById = 
        fun userId ->
            try
                let userFound = _userList |> List.find (fun user -> user.Id = userId)
                Some userFound
            with
                | :? System.Collections.Generic.KeyNotFoundException -> None