namespace Infrastructure

module UserRepositoryTypes =
    type FindUserById = int -> (Domain.Entities.User option)