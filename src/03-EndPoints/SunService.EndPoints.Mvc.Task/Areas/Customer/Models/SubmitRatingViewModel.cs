namespace SunService.EndPoints.Mvc.Task.Areas.Customer.Models
{
    public class SubmitRatingViewModel
    {
        public int OrderId { get; set; }
        public int Score { get; set; } 
        public string? Comment { get; set; }
    }
}
