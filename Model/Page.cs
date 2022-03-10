using System.Text.Json.Serialization;

namespace InqService.Model
{
    public class Page
    {
        [JsonPropertyName("page_no")]
        public int PageNo { get; set; }

        [JsonPropertyName("total_page")]
        public int TotalPage { get; set; }

        [JsonPropertyName("total_row")]
        public int TotalRow { get; set; }

        [JsonPropertyName("row_per_page")]
        public int RowPerPage { get; set; }
    }
}
