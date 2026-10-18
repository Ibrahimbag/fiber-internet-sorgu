namespace fiber_internet_sorgu;

public sealed class InfrastructureLocation
{
    public string Name { get; }
    public List<InfrastructureLocation> Children { get; }
    public List<string> Packages { get; }

    public InfrastructureLocation(string name, params InfrastructureLocation[] children)
    {
        Name = name;
        Children = children.ToList();
        Packages = [];
    }

    public InfrastructureLocation(string name, List<string> packages)
    {
        Name = name;
        Children = [];
        Packages = packages;
    }
}

public static class InfrastructureData
{
    public static List<InfrastructureLocation> Provinces =>
    [
        new InfrastructureLocation("Ýstanbul",
            new InfrastructureLocation("Kadýköy",
                new InfrastructureLocation("Caferaða Mahallesi",
                    new InfrastructureLocation("Moda Caddesi",
                        new InfrastructureLocation("Deniz Apartmaný", ["Fiber 100 Mbps", "Fiber 500 Mbps", "Fiber 1000 Mbps"]),
                        new InfrastructureLocation("Güneþ Sitesi", ["Fiber 100 Mbps", "Fiber 500 Mbps"])),
                    new InfrastructureLocation("Bahariye Sokak",
                        new InfrastructureLocation("Çýnar Apartmaný", ["Fiber 100 Mbps", "Fiber 1000 Mbps"]))),
                new InfrastructureLocation("Fenerbahçe Mahallesi",
                    new InfrastructureLocation("Baðdat Caddesi",
                        new InfrastructureLocation("Yýldýz Apartmaný", ["Fiber 100 Mbps", "Fiber 500 Mbps"])))),
            new InfrastructureLocation("Üsküdar",
                new InfrastructureLocation("Mimar Sinan Mahallesi",
                    new InfrastructureLocation("Hakimiyeti Milliye Caddesi",
                        new InfrastructureLocation("Mavi Apartmaný", ["Fiber 100 Mbps", "Fiber 500 Mbps"]))))),
        new InfrastructureLocation("Ankara",
            new InfrastructureLocation("Çankaya",
                new InfrastructureLocation("Kavaklýdere Mahallesi",
                    new InfrastructureLocation("Tunalý Hilmi Caddesi",
                        new InfrastructureLocation("Baþkent Apartmaný", ["Fiber 100 Mbps", "Fiber 500 Mbps", "Fiber 1000 Mbps"])))),
            new InfrastructureLocation("Yenimahalle",
                new InfrastructureLocation("Çarþý Mahallesi",
                    new InfrastructureLocation("Ývedik Caddesi",
                        new InfrastructureLocation("Anadolu Sitesi", new List<string>()))))),
        new InfrastructureLocation("Ýzmir",
            new InfrastructureLocation("Karþýyaka",
                new InfrastructureLocation("Bostanlý Mahallesi",
                    new InfrastructureLocation("Cemal Gürsel Caddesi",
                        new InfrastructureLocation("Ege Apartmaný", ["Fiber 100 Mbps", "Fiber 500 Mbps"])))))
    ];
}
