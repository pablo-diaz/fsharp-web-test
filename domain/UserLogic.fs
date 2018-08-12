namespace Domain

module UserLogic =
    open Entities

    let createUser id name isHeadCoach manager =
        { Id = id
          Name = name
          IsHeadCoach = isHeadCoach
          Manager = manager }