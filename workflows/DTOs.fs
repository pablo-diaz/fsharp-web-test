namespace Workflows

module DTOs =
    type ActivityCreateCommand = {
        Name: string
        CreatorId: int
        ParticipantIds: int list
        ReviewerIds: int list
    }