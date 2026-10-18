namespace fiber_internet_sorgu;

public partial class Form1 : Form
{
    private readonly ComboBox provinceComboBox = CreateComboBox();
    private readonly ComboBox districtComboBox = CreateComboBox();
    private readonly ComboBox neighborhoodComboBox = CreateComboBox();
    private readonly ComboBox streetComboBox = CreateComboBox();
    private readonly ComboBox buildingComboBox = CreateComboBox();
    private readonly TextBox doorNumberTextBox = new();
    private readonly ListBox packagesListBox = new();

    public Form1()
    {
        InitializeComponent();
        BuildScreen();
        LoadLocations();
    }

    private static ComboBox CreateComboBox() => new()
    {
        Dock = DockStyle.Fill,
        DropDownStyle = ComboBoxStyle.DropDownList,
        Enabled = false
    };

    private void BuildScreen()
    {
        Text = "Fiber Ýnternet Altyapý Sorgulama";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(760, 500);
        ClientSize = new Size(900, 560);
        BackColor = Color.FromArgb(245, 247, 250);

        var title = new Label
        {
            Text = "Fiber Ýnternet Altyapý Sorgulama",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(25, 55, 95),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24),
            ColumnCount = 1,
            RowCount = 2
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.Controls.Add(title, 0, 0);

        var columns = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1
        };
        columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));
        columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
        root.Controls.Add(columns, 0, 1);

        var addressGroup = new GroupBox
        {
            Text = "Adres bilgileri",
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
        var addressLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 7,
            Padding = new Padding(8)
        };
        addressLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        addressLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (var row = 0; row < 6; row++)
            addressLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14));
        addressLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 16));

        AddAddressRow(addressLayout, 0, "Ýl", provinceComboBox);
        AddAddressRow(addressLayout, 1, "Ýlçe", districtComboBox);
        AddAddressRow(addressLayout, 2, "Mahalle", neighborhoodComboBox);
        AddAddressRow(addressLayout, 3, "Sokak / Cadde", streetComboBox);
        AddAddressRow(addressLayout, 4, "Bina", buildingComboBox);
        AddAddressRow(addressLayout, 5, "Kapý numarasý", doorNumberTextBox);

        var queryButton = new Button
        {
            Text = "Altyapýyý Sorgula",
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(25, 105, 180),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
        queryButton.Click += QueryButton_Click;
        addressLayout.Controls.Add(queryButton, 1, 6);
        addressGroup.Controls.Add(addressLayout);
        columns.Controls.Add(addressGroup, 0, 0);

        var resultGroup = new GroupBox
        {
            Text = "Sorgu sonucu",
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };
        packagesListBox.Dock = DockStyle.Fill;
        packagesListBox.Font = new Font("Segoe UI", 10);
        packagesListBox.Items.Add("Adresinizi seçip sorgulayýn.");
        resultGroup.Controls.Add(packagesListBox);
        columns.Controls.Add(resultGroup, 1, 0);

        Controls.Add(root);

        provinceComboBox.SelectedIndexChanged += ProvinceComboBox_SelectedIndexChanged;
        districtComboBox.SelectedIndexChanged += DistrictComboBox_SelectedIndexChanged;
        neighborhoodComboBox.SelectedIndexChanged += NeighborhoodComboBox_SelectedIndexChanged;
        streetComboBox.SelectedIndexChanged += StreetComboBox_SelectedIndexChanged;
    }

    private static void AddAddressRow(TableLayoutPanel layout, int row, string label, Control control)
    {
        layout.Controls.Add(new Label
        {
            Text = label,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 9)
        }, 0, row);
        control.Margin = new Padding(3, 8, 3, 8);
        layout.Controls.Add(control, 1, row);
    }

    private void LoadLocations()
    {
        BindLocations(provinceComboBox, InfrastructureData.Provinces);
    }

    private static void BindLocations(ComboBox comboBox, List<InfrastructureLocation> locations)
    {
        comboBox.DataSource = null;
        comboBox.Enabled = locations.Count > 0;
        comboBox.DisplayMember = nameof(InfrastructureLocation.Name);
        comboBox.DataSource = locations;
    }

    private void ProvinceComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var province = provinceComboBox.SelectedItem as InfrastructureLocation;
        BindLocations(districtComboBox, province?.Children ?? []);
        BindLocations(neighborhoodComboBox, []);
        BindLocations(streetComboBox, []);
        BindLocations(buildingComboBox, []);
    }

    private void DistrictComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var district = districtComboBox.SelectedItem as InfrastructureLocation;
        BindLocations(neighborhoodComboBox, district?.Children ?? []);
        BindLocations(streetComboBox, []);
        BindLocations(buildingComboBox, []);
    }

    private void NeighborhoodComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var neighborhood = neighborhoodComboBox.SelectedItem as InfrastructureLocation;
        BindLocations(streetComboBox, neighborhood?.Children ?? []);
        BindLocations(buildingComboBox, []);
    }

    private void StreetComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var street = streetComboBox.SelectedItem as InfrastructureLocation;
        BindLocations(buildingComboBox, street?.Children ?? []);
    }

    private void QueryButton_Click(object? sender, EventArgs e)
    {
        if (buildingComboBox.SelectedItem is not InfrastructureLocation building ||
            string.IsNullOrWhiteSpace(doorNumberTextBox.Text))
        {
            MessageBox.Show("Lütfen adres bilgilerini ve kapý numarasýný girin.", "Eksik bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        packagesListBox.Items.Clear();
        packagesListBox.Items.Add($"{provinceComboBox.Text} / {districtComboBox.Text}");
        packagesListBox.Items.Add($"{neighborhoodComboBox.Text}, {streetComboBox.Text}");
        packagesListBox.Items.Add($"{building.Name}, No: {doorNumberTextBox.Text}");
        packagesListBox.Items.Add(string.Empty);

        if (building.Packages.Count == 0)
        {
            packagesListBox.Items.Add("Bu adreste fiber altyapý bulunamadý.");
            return;
        }

        packagesListBox.Items.Add("Kullanýlabilir fiber paketleri:");
        foreach (var package in building.Packages)
            packagesListBox.Items.Add(package);
    }
}
