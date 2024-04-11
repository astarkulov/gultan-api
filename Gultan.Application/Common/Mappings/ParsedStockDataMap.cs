using CsvHelper.Configuration;

namespace Gultan.Application.Common.Mappings;

public sealed class ParsedStockDataMap : ClassMap<ParsedStockData>
{
    public ParsedStockDataMap()
    {
        Map(x => x.Date).Index(0).Name("DATE");
        Map(x => x.Time).Index(1).Name("TIME");
        Map(x => x.Open).Index(2).Name("OPEN");
        Map(x => x.High).Index(3).Name("HIGH");
        Map(x => x.Low).Index(4).Name("LOW");
        Map(x => x.Close).Index(5).Name("CLOSE");
        Map(x => x.Volume).Index(6).Name("VOL");
    }
}