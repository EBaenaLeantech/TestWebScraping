namespace DAT_Download_Service.Models
{
    public abstract class RateDetailData
    {
        /// <summary>
        /// 
        /// </summary>
        public string OriginGeoExpansion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationGeoExpansion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AvgLinehaulRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double LowLinehaulRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double HighLinehaulRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double FuelSurcharge { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Companies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Reports { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double LinehaulRateStdDev { get; set; }


    }
}
