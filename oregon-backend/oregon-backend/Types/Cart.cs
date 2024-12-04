namespace oregon_backend.types;

public class CartAddRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CartUpdateRequest
{
    public int Quantity { get; set; }
}

public class CartAddResponse
{
    public string Message { get; set; }
}

public class CartUpdateResponse
{
    public string Message { get; set; }
}

public class CartDeleteResponse
{
    public string Message { get; set; }
}