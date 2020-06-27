using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAT_Download_Service.Models
{
    public class ContractRateDataModel : RateDetailData
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual DATRateData DATRateData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AccessorialsExcludingFuel { get; set; }
    }
}
