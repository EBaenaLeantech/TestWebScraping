using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAT_Download_Service.Models
{
    public class SpotRateDataModel : RateDetailData
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("DATRateData")]
        public int DATRateDataId { get; set; }
        
    }
}
