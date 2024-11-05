using KoiFish_Core.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KoiFish_Data.Configurations
{
    public class ElementConfiguration : IEntityTypeConfiguration<Element>
    {
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.HasData(new Element
            {
                BirthYear = 1960,
                ElementName = "Earth"
            },
            new Element { BirthYear = 1961, ElementName = "Earth" },
        new Element { BirthYear = 1962, ElementName = "Metal" },
        new Element { BirthYear = 1963, ElementName = "Metal" },
        new Element { BirthYear = 1964, ElementName = "Fire" },
        new Element { BirthYear = 1965, ElementName = "Fire" },
        new Element { BirthYear = 1966, ElementName = "Water" },
        new Element { BirthYear = 1967, ElementName = "Water" },
        new Element { BirthYear = 1968, ElementName = "Earth" },
        new Element { BirthYear = 1969, ElementName = "Earth" },
        new Element { BirthYear = 1970, ElementName = "Metal" },
        new Element { BirthYear = 1971, ElementName = "Metal" },
        new Element { BirthYear = 1972, ElementName = "Wood" },
        new Element { BirthYear = 1973, ElementName = "Wood" },
        new Element { BirthYear = 1974, ElementName = "Water" },
        new Element { BirthYear = 1975, ElementName = "Water" },
        new Element { BirthYear = 1976, ElementName = "Earth" },
        new Element { BirthYear = 1977, ElementName = "Earth" },
        new Element { BirthYear = 1978, ElementName = "Fire" },
        new Element { BirthYear = 1979, ElementName = "Fire" },
        new Element { BirthYear = 1980, ElementName = "Wood" },
        new Element { BirthYear = 1981, ElementName = "Wood" },
        new Element { BirthYear = 1982, ElementName = "Water" },
        new Element { BirthYear = 1983, ElementName = "Water" },
        new Element { BirthYear = 1984, ElementName = "Metal" },
        new Element { BirthYear = 1985, ElementName = "Metal" },
        new Element { BirthYear = 1986, ElementName = "Fire" },
        new Element { BirthYear = 1987, ElementName = "Fire" },
        new Element { BirthYear = 1988, ElementName = "Wood" },
        new Element { BirthYear = 1989, ElementName = "Wood" },
        new Element { BirthYear = 1990, ElementName = "Earth" },
        new Element { BirthYear = 1991, ElementName = "Earth" },
        new Element { BirthYear = 1992, ElementName = "Metal" },
        new Element { BirthYear = 1993, ElementName = "Metal" },
        new Element { BirthYear = 1994, ElementName = "Fire" },
        new Element { BirthYear = 1995, ElementName = "Fire" },
        new Element { BirthYear = 1996, ElementName = "Water" },
        new Element { BirthYear = 1997, ElementName = "Water" },
        new Element { BirthYear = 1998, ElementName = "Earth" },
        new Element { BirthYear = 1999, ElementName = "Earth" },
        new Element { BirthYear = 2000, ElementName = "Metal" });
        }
    }
}
