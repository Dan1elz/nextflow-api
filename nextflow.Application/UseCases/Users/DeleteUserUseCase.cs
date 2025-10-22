using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Users;

public class DeleteUserUseCase(IUserRepository repository)
    : DeleteUseCaseBase<User, IUserRepository>(repository)
{ }
