namespace Domain

module Entities =
    
    type User = {
        Id: int
        Name: string
        Manager: User option
        IsHeadCoach: bool
    }

    type UserRole = 
        | HeadCoach of User
        | ActivityCreator of User
        | Participant of User
        | Reviewer of User

    type Activity = {
        Id: int
        Creator: User
        Name: string
        Participants: User list
        Reviewers: User list
        CreateDate: System.DateTime
    }