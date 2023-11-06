namespace Application.Utilities.Helpers;

public class PaginationHelper
{
    public static int CalculatePageCount(int totalCount, int pageSize)
    {
        int pageCount = 0;
        if (pageSize > 0)
        {
            if (totalCount % pageSize > 0)
                pageCount = (totalCount / pageSize) + 1;
            else
                pageCount = (totalCount / pageSize);
        }

        return pageCount;
    }
}
