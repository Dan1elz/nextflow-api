using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.States;

public class DeleteStateUseCase(IStateRepository repository)
: DeleteUseCaseBase<State, IStateRepository>(repository)
{ }
