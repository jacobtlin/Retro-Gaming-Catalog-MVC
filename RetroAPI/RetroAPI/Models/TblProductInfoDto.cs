namespace RetroAPI.Models
{
    public class TblProductInfoDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public virtual ICollection<TblProductRankingDto> TblProductRankings { get; set; } = null!;

    }
}
