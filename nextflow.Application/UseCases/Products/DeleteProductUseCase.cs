using nextflow.Application.UseCases.Base;
using nextflow.Domain.Interfaces.Repositories;
using nextflow.Domain.Models;

namespace nextflow.Application.UseCases.Products;

public class DeleteProductUseCase(IProductRepository repository)
    : DeleteUseCaseBase<Product, IProductRepository>(repository)
{ }
