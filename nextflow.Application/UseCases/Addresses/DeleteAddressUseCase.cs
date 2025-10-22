using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Addresses;

public class DeleteAddressUseCase(IAddressRepository repository)
: DeleteUseCaseBase<Address, IAddressRepository>(repository)
{ }
