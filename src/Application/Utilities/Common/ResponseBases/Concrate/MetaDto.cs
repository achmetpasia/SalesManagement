namespace Application.Utilities.Common.ResponseBases.Concrate;

public class MetaDto
{
    public int TotalPage { get; set; }
    public int TotalData { get; set; }
    public int PageLength { get; set; }
    public int PageIndex { get; set; }

    public MetaDto(int totalPage, int totalData, int pageLength, int pageIndex)
    {
        TotalPage = totalPage;
        TotalData = totalData;
        PageLength = pageLength;
        PageIndex = pageIndex;
    }
}
