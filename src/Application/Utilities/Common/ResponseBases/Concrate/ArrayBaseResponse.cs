using Application.Utilities.Helpers;

namespace Application.Utilities.Common.ResponseBases.Concrate;

public class ArrayBaseResponse<T>
{
    public ICollection<T> Data { get; set; }
    public MetaDto Meta { get; set; }

    public ArrayBaseResponse(ICollection<T> data, int totalData, int pageLength, int pageIndex)
    {
        Data = data;
        Meta = new MetaDto(PaginationHelper.CalculatePageCount(totalData, pageLength), totalData, pageLength, pageIndex);
    }
}
