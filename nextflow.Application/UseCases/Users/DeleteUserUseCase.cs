using nextflow.Application.UseCases.Base;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Users;

public class DeleteUserUseCase(IUserRepository repository)
    : DeleteUseCaseBase<User, IUserRepository>(repository)
{ }
