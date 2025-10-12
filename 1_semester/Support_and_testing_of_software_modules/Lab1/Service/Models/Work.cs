namespace Service.Models;

public partial class Work
{
    public int WorkId { get; set; }

    public string? Message { get; set; }

    public int? FkMasterId { get; set; }

    public int? FkRequestId { get; set; }

    public virtual Master? FkMaster { get; set; }

    public virtual Request? FkRequest { get; set; }
}
