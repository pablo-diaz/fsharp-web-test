#r "../workflows/bin/Debug/netstandard2.0/domain.dll"
#r "../workflows/bin/Debug/netstandard2.0/workflows.dll"

open Workflows.DTOs

let input = {
    Name = "A test activity"
    CreatorId = 1
    ParticipantIds = [6;7;8]
    ReviewerIds = [11;12]
}

let findUserFn = Infrastructure.InMemoryUserRepository.findUserByIdInMemoryList
let activity = Workflows.BusinessLogic.createAcivityHandler findUserFn input