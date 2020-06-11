using System;
using System.ComponentModel.DataAnnotations;

namespace DAT_Download_Service.Models
{
    public class DATRateData
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Represents the data date: For historical this is just the first day of the next month, 
        /// for current it should be the date the data is pulled.
        /// </summary>
        public DateTime DataDownloadDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TruckType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationPostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationCity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationGeoLabelShort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationGeoLabelLong { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginPostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginCity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginGeoType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginGeoLabelShort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OriginGeoLabelLong { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PCMilerPracticalMileage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SpotRateDataModel SpotRateData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ContractRateDataModel ContractRateData { get; set; }

        public DATRateData()
        {
            this.InsertDate = DateTime.Now;
            this.DataDownloadDate = DateTime.Now;
        }

    }


}
