using Core.Persistence.Paging;

namespace core.Application.Responses;

public class GetListResponse<T> : BasePageableModel
{
    private IList<T>? _items;

    public IList<T> Items
    {
        get => _items ??= [];
        set => _items = value;
    }
}
