namespace Nextflow.Domain.Dtos;

public class ApiResponse<T>
{
    public int Status { get; set; }
    public required string Message { get; set; }
    public required T Data { get; set; }
}
public class ApiResponseMessage
{
    public int Status { get; set; }
    public required string Message { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}
public class ApiResponseTable<T>
{
    public int TotalItems { get; set; }
    public required List<T> Data { get; set; }
}
public class OptionDto
{
    public required string Value { get; set; }
    public required string Label { get; set; }
}
public class ListIdsGuidDto
{
    public required List<Guid>? Ids { get; set; }
}