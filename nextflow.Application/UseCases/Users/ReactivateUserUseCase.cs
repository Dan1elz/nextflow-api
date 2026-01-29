using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Users;

public class ReactivateUserUseCase(IUserRepository repository)
    : ReactivateUseCaseBase<User, IUserRepository>(repository)
{ }
