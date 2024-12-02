namespace oregon_backend.types;

public class CommentAddRequest
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string? Content { get; set; }
}

public class CommentUpdateRequest
{
    public string? Content { get; set; }
}

public class CommentAddResponse
{
    public string Message { get; set; }
}

public class CommentUpdateResponse
{
    public string Message { get; set; }
}

public class CommentDeleteResponse
{
    public string Message { get; set; }
}