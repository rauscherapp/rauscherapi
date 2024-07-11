using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Globalization;
using System.Linq;

namespace Application.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      //ConfigureDomainToViewModel 
      CreateMap<EventRegistry, EventRegistryViewModel>()
        .ForMember(dest => dest.EventRegistryId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
        .ForMember(dest => dest.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
        .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType))
        .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
        .ForMember(dest => dest.EventLocation, opt => opt.MapFrom(src => src.EventLocation))
        .ForMember(dest => dest.EventLink, opt => opt.MapFrom(src => src.EventLink))
        .ForMember(dest => dest.EventDateMonth, opt => opt.MapFrom(src => src.EventDate.ToString("MMM").ToUpper()))
        .ForMember(dest => dest.EventDateDay, opt => opt.MapFrom(src => src.EventDate.Day.ToString()))
        .ForMember(dest => dest.EventDateHour, opt => opt.MapFrom(src => src.EventDate.ToString("hh:mm tt").ToUpper()))
        .ForMember(dest => dest.EventDateYear, opt => opt.MapFrom(src => src.EventDate.ToString("YYYY")));

      CreateMap<AppParameters, AppParametersViewModel>();
        CreateMap<CommoditiesRate, CommoditiesRateViewModel>()
            .ForMember(dest => dest.FullVariationPricePercent, opt => opt.MapFrom<FullVariationPricePercentResolver>())
            .ForMember(dest => dest.TimestampDate, opt => opt.MapFrom<TimestampToDateTimeResolver>())
            .ForMember(dest => dest.FormattedPrice, opt => opt.MapFrom<CurrencyResolver>());


      CreateMap<Symbols, SymbolsViewModel>()
        .ForMember(dest => dest.LastRate, opt => opt.MapFrom(src => src.CommoditiesRates.OrderByDescending(cr => cr.Timestamp).FirstOrDefault()));
      CreateMap<ApiCredentials, ApicredentialsViewModel>();
      CreateMap<Post, PostViewModel>();
      CreateMap<Folder, FolderViewModel>();
    }
  }
}


public class FullVariationPricePercentResolver : IValueResolver<CommoditiesRate, object, string>
{
  public string Resolve(CommoditiesRate source, object destination, string destMember, ResolutionContext context)
  {
    string variationPrice = source.Variationprice.HasValue ? source.Variationprice.Value.ToString("F2") : "0.00";
    string variationPricePercent = source.Variationpricepercent.HasValue ? source.Variationpricepercent.Value.ToString("F2") : "0.00";
    string isUpSignal = source.Isup ? "+" : "";

    return $"{isUpSignal}{variationPrice}({isUpSignal}{variationPricePercent}%)";
  }
}

public class TimestampToDateTimeResolver : IValueResolver<CommoditiesRate, object, string>
{
  protected DateTime date;
  public string Resolve(CommoditiesRate source, object destination, string destMember, ResolutionContext context)
  {
    
    if (source.Timestamp.HasValue)
    {
      
      if (source.Timestamp.Value > 10000000000)
      {
        date = DateTimeOffset.FromUnixTimeMilliseconds(source.Timestamp.Value).DateTime;
      }
      else
      {
        date = DateTimeOffset.FromUnixTimeSeconds(source.Timestamp.Value).DateTime;
      }
      
      return date.ToString("HH:mm:ss");
    }
    return null;
  }
}

public class CurrencyResolver : IValueResolver<CommoditiesRate, object, string>
{
  protected DateTime date;
  public string Resolve(CommoditiesRate source, object destination, string destMember, ResolutionContext context)
  {

    if (source.SymbolCode.Equals("PTAX"))
    {
      // Custom formatting to ensure no rounding occurs
      decimal value = source.Price;
      string formattedValue = $"{new CultureInfo("en-US").NumberFormat.CurrencySymbol} {value.ToString("0.0000", new CultureInfo("en-US"))}";
      return formattedValue;
    }
    else
    {
      return source.Price.ToString("C", new CultureInfo("en-US"));
    }
  }
}
public class PriceToCurrencyConverter : IValueConverter<decimal?, string>
{
  public string Convert(decimal? sourceMember, ResolutionContext context)
  {
    if (sourceMember.HasValue)
    {
      // Custom formatting to ensure no rounding occurs
      decimal value = sourceMember.Value;
      string formattedValue = $"{new CultureInfo("en-US").NumberFormat.CurrencySymbol} {value.ToString("0.0000", new CultureInfo("en-US"))}";
      return formattedValue;
    }
    return null;
  }
}