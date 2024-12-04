namespace oregon_backend.types;

public class ProductAddRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}

public class ProductUpdateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}

public class ProductAddRatingRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Value { get; set; }
}

public class ProductAddResponse
{
    public string Message { get; set; }
}

public class ProductUpdateResponse
{
    public string Message { get; set; }
}

public class ProductDeleteResponse
{
    public string Message { get; set; }
}

public class ProductAddRatingResponse
{
    public string Message { get; set; }
}