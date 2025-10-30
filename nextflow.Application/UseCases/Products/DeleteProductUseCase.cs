using Nextflow.Application.UseCases.Base;
using Nextflow.Domain.Interfaces.Repositories;
using Nextflow.Domain.Models;

namespace Nextflow.Application.UseCases.Products;

public class DeleteProductUseCase(IProductRepository repository)
    : DeleteUseCaseBase<Product, IProductRepository>(repository)
{ }
