using YahooQuotesApi;

namespace Gultan.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Token, TokenDto>().ReverseMap();
        CreateMap<StockDto, Stock>().ReverseMap();
        CreateMap<StockPrice, StockPriceDto>().ReverseMap();
        CreateMap<Wallet, WalletDto>().ReverseMap();
        CreateMap<WalletStockDto, WalletStock>().ReverseMap();
        CreateMap<Goal, GoalDto>().ReverseMap();
        CreateMap<Security, StockDto>()
            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LongName))
            .ForMember(dest => dest.Exchange, opt => opt.MapFrom(src => src.FullExchangeName))
            .ForMember(dest => dest.LastPrice, opt => opt.MapFrom(src => src.RegularMarketPrice))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap))
            .ForMember(dest => dest.StockPrices, opt => opt.Ignore()); 

        CreateMap<Security, StockPriceDto>()
            .ForMember(dest => dest.StockId, opt => opt.Ignore()) // Assuming StockId will be set separately
            .ForMember(dest => dest.Stock, opt => opt.Ignore()) // Assuming Stock will be set separately
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeMilliseconds(src.RegularMarketTimeSeconds).DateTime))
            .ForMember(dest => dest.OpenPrice, opt => opt.MapFrom(src => src.RegularMarketOpen))
            .ForMember(dest => dest.HighPrice, opt => opt.MapFrom(src => src.RegularMarketDayHigh))
            .ForMember(dest => dest.LowPrice, opt => opt.MapFrom(src => src.RegularMarketDayLow))
            .ForMember(dest => dest.ClosePrice, opt => opt.MapFrom(src => src.RegularMarketPrice))
            .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.RegularMarketVolume));
    }
}