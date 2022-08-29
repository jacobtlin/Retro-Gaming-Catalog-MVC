namespace RetroAPI.Models
{
    public class TblProductRankingDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? ProductRanking { get; set; }
        public int? ProductVotes { get; set; }
        public string ProductNameNavigation { get; set; } = null!;
    }
}
