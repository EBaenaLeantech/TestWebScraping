using AutoMapper;
using DAT_Download_Service.Models;
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
                .ForMember(d => d.DestinationPostalCode, o => o.MapFrom(s => s["Orig Postal Code"]))
                .ForMember(d => d.DestinationState, o => o.MapFrom(s => s["Orig State"]))
                .ForMember(d => d.DestinationCity, o => o.MapFrom(s => s["Orig City"]));

            CreateMap<DataRow, SpotRateDataModel>()
                .ForMember(d => d.AvgLinehaulRate, o => o.MapFrom(s => s["Spot Avg Linehaul Rate"]))
                .ForMember(d => d.LowLinehaulRate, o => o.MapFrom(s => s["Spot Low Linehaul Rate"]))
                .ForMember(d => d.HighLinehaulRate, o => o.MapFrom(s => s["Spot High Linehaul Rate"]))
                .ForMember(d => d.FuelSurcharge, o => o.MapFrom(s => s["Spot Fuel Surcharge"]));

            CreateMap<DataRow, ContractRateDataModel>()
                .ForMember(d => d.AvgLinehaulRate, o => o.MapFrom(s => s["Contract Avg Linehaul Rate"]))
                .ForMember(d => d.LowLinehaulRate, o => o.MapFrom(s => s["Contract Low Linehaul Rate"]))
                .ForMember(d => d.HighLinehaulRate, o => o.MapFrom(s => s["Contract High Linehaul Rate"]))
                .ForMember(d => d.FuelSurcharge, o => o.MapFrom(s => s["Contract Fuel Surcharge"]))
                .ForMember(d => d.AccessorialsExcludingFuel, o => o.MapFrom(s => s["Contract Avg Accessorial Excludes Fuel"]))
                .ForMember(d => d.Companies, o => o.MapFrom(s => s["Contract # of Companies"]))
                .ForMember(d => d.Reports, o => o.MapFrom(s => s["Contract # of Reports"]))
                .ForMember(d => d.ContractLinehaulRateStdDev, o => o.MapFrom(s => s["Contract Linehaul Rate StdDev"]));
        }
    }
}
