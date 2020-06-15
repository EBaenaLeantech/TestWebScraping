using AutoMapper;
using DAT_Download_Service.Models;
using System;
using System.Data;

namespace DAT_Download_Service.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DataRow, DATRateData>()
                .ForMember(d => d.TruckType, o => o.MapFrom(s => s["Truck Type"]))
                .ForMember(d => d.PCMilerPracticalMileage, o => o.MapFrom(s => s["PC-Miler Practical Mileage"]))
                .ForMember(d => d.DestinationPostalCode, o => o.MapFrom(s => s["Dest Postal Code"]))
                .ForMember(d => d.DestinationState, o => o.MapFrom(s => s["Dest State"]))
                .ForMember(d => d.DestinationCity, o => o.MapFrom(s => s["Dest City"]))
                .ForMember(d => d.OriginPostalCode, o => o.MapFrom(s => s["Orig Postal Code"]))
                .ForMember(d => d.OriginState, o => o.MapFrom(s => s["Orig State"]))
                .ForMember(d => d.OriginCity, o => o.MapFrom(s => s["Orig City"]))
                .AfterMap((origin, dest) =>
                {
                    dest.ContractRateData = new ContractRateDataModel();
                    dest.ContractRateData.OriginGeoExpansion = origin["Contract Origin Geo Expansion"].ToString();
                    dest.ContractRateData.DestinationGeoExpansion = origin["Contract Destination Geo Expansion"].ToString();
                    dest.ContractRateData.AvgLinehaulRate = Convert.ToDouble(origin["Contract Avg Linehaul Rate"]);
                    dest.ContractRateData.LowLinehaulRate = Convert.ToDouble(origin["Contract Low Linehaul Rate"]);
                    dest.ContractRateData.HighLinehaulRate = Convert.ToDouble(origin["Contract High Linehaul Rate"]);
                    dest.ContractRateData.FuelSurcharge = Convert.ToDouble(origin["Contract Fuel Surcharge"]);
                    dest.ContractRateData.AccessorialsExcludingFuel = Convert.ToDouble(origin["Contract Avg Accessorial Excludes Fuel"]);
                    dest.ContractRateData.Companies = Convert.ToInt32(origin["Contract # of Companies"]);
                    dest.ContractRateData.Reports = Convert.ToInt32(origin["Contract # of Reports"]);
                    dest.ContractRateData.LinehaulRateStdDev = Convert.ToDouble(origin["Contract Linehaul Rate StdDev"]);
                })
                .AfterMap((origin, dest) =>
                {
                    dest.SpotRateData = new SpotRateDataModel();
                    dest.SpotRateData.OriginGeoExpansion = origin["Spot Origin Geo Expansion"].ToString();
                    dest.SpotRateData.DestinationGeoExpansion = origin["Spot Destination Geo Expansion"].ToString();
                    dest.SpotRateData.AvgLinehaulRate = Convert.ToDouble(origin["Spot Avg Linehaul Rate"]);
                    dest.SpotRateData.LowLinehaulRate = Convert.ToDouble(origin["Spot Low Linehaul Rate"]);
                    dest.SpotRateData.HighLinehaulRate = Convert.ToDouble(origin["Spot High Linehaul Rate"]);
                    dest.SpotRateData.FuelSurcharge = Convert.ToDouble(origin["Spot Fuel Surcharge"]);
                    dest.SpotRateData.Companies = Convert.ToInt32(origin["Spot # of Companies"]);
                    dest.SpotRateData.Reports = Convert.ToInt32(origin["Spot # of Reports"]);
                    dest.SpotRateData.LinehaulRateStdDev = Convert.ToDouble(origin["Spot Linehaul Rate StdDev"]);
                });
        }
    }
}
