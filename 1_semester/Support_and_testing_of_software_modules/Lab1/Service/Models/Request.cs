namespace Service.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public DateTime? StartDate { get; set; }

    public int? FkTechModelId { get; set; }

    public string? ProblemDescryption { get; set; }

    public int? FkRequestStatusId { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? FkRepairPartId { get; set; }

    public int? FkClientId { get; set; }

    public virtual Client? FkClient { get; set; }

    public virtual RepairPart? FkRepairPart { get; set; }

    public virtual RequestStatus? FkRequestStatus { get; set; }

    public virtual TechModel? FkTechModel { get; set; }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
