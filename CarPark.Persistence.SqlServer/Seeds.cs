using CarPark.Models;
using System.Collections.Generic;

namespace CarPark.Persistence.SqlServer
{
    public static class Seeds
    {
        public static ICollection<DefaultFlatRate> DefaultFlatRates { get; } = new List<DefaultFlatRate>
        {
            new DefaultFlatRate(20M).WithKey("53E52A31-300A-43E3-81C0-D07E04912289")
        };

        public static ICollection<FlatRate> FlatRates { get; } = new List<FlatRate>
        {
            // Weekends
            new FlatRate(10M, 5, new HourRange(0,24), new HourRange(0,24), canExtendToFollowingDay: true)
                .WithKey("729E1681-08ED-4051-BADB-043B32EE64ED"),
            new FlatRate(10M, 6, new HourRange(0,24), new HourRange(0,24)).WithKey("C4081969-5101-4D66-863E-26D3B8334E7C"),

            // Nights
            new FlatRate(6.5M, 0, new HourRange(0,6), new HourRange(0,6))
                .WithKey("709BB016-F728-4E77-A034-2A57ABA74276"),
            new FlatRate(6.5M, 0, new HourRange(18,24), new HourRange(18,30))
                .WithKey("90E1933E-9CCD-4CEB-A21B-3016376EECE0"),
            new FlatRate(6.5M, 1, new HourRange(0,6), new HourRange(0,6))
                .WithKey("75BB7456-BD7F-4562-BD0F-366BB06D788F"),
            new FlatRate(6.5M, 1, new HourRange(18,24), new HourRange(18,30))
                .WithKey("BEDD0515-F4AD-43A4-B053-4A47F2809B08"),
            new FlatRate(6.5M, 2, new HourRange(0,6), new HourRange(0,6))
                .WithKey("B9F841B7-966B-4D65-A582-4C2097039AF7"),
            new FlatRate(6.5M, 2, new HourRange(18,24), new HourRange(18,30))
                .WithKey("FD1C8E1D-64AC-4711-9166-787004E298CB"),
            new FlatRate(6.5M, 3, new HourRange(0,6), new HourRange(0,6)).WithKey("B69F8BCD-15CC-4317-8102-84B8B313CBE2"),
            new FlatRate(6.5M, 3, new HourRange(18,24), new HourRange(18,30))
                .WithKey("D3C39239-DB5A-4DEE-8CE9-8E07066B2994"),
            new FlatRate(6.5M, 4, new HourRange(0,6), new HourRange(0,6))
                .WithKey("714D8101-EA70-44D1-BAF7-9B81934C347F"),
            new FlatRate(6.5M, 4, new HourRange(18,24), new HourRange(18,30), canExtendToFollowingDay: true)
                .WithKey("28C5F0D3-284B-458E-9F5A-A152889403EB"),

            // Early Birds
            new FlatRate(13M, 0, new HourRange(6,9), new HourRange(15.5M, 23.5M))
                .WithKey("7BA6627F-37AC-4B7E-8AB2-BB834474471C"),
            new FlatRate(13M, 1, new HourRange(6,9), new HourRange(15.5M, 23.5M))
                .WithKey("FC535ED7-553F-4B20-AD30-C5DB0E38228E"),
            new FlatRate(13M, 2, new HourRange(6,9), new HourRange(15.5M, 23.5M))
                .WithKey("58899BEC-15C7-46DE-8CDA-C9DEDCE08F07"),
            new FlatRate(13M, 3, new HourRange(6,9), new HourRange(15.5M, 23.5M))
                .WithKey("C96EC84E-D26E-4E95-B5FF-D5695755C6A4"),
            new FlatRate(13M, 4, new HourRange(6,9), new HourRange(15.5M, 23.5M))
                .WithKey("C2E00B43-D42A-4225-8A84-E73B48EF5DB3"),
        };

        public static ICollection<HourlyRate> HourlyRates { get; } = new List<HourlyRate>
        {
            new HourlyRate(5M, 0, 1).WithKey("C5303CE4-0D4A-402D-9AB1-365D4F48237B"),
            new HourlyRate(10M, 1, 2).WithKey("4C656F4D-319D-4D93-B75B-7D2D6E135673"),
            new HourlyRate(15M, 2, 3).WithKey("5A9D5A44-2BBD-4184-BDA4-9922DA4C1DDE"),
        };
    }
}