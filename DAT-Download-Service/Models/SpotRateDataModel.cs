﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAT_Download_Service.Models
{
    public class SpotRateDataModel
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("DATRateData")]
        public int DATRateDataId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DestinationGeoType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float AvgLinehaulRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float LowLinehaulRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float HighLinehaulRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float FuelSurcharge { get; set; }
    }
}
