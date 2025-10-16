using nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Suppliers;

public class DeleteSupplierUseCase(ISupplierRepository repository)
    : DeleteUseCaseBase<Supplier, ISupplierRepository>(repository)
{ }
