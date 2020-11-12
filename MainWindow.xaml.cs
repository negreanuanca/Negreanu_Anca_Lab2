using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Negreanu_Anca_Lab2.Doughnut;

namespace Negreanu_Anca_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        private int mRaisedGlazed = 0;
        private int mRaisedSugar = 0;
        private int mfilledLemon = 0;
        private int mfilledChocolate = 0;
        private int mfilledVanilla = 0;
        private double Total=0;

        KeyValuePair<DoughnutType, double>[] PriceList = {
            new KeyValuePair<DoughnutType, double>(DoughnutType.Sugar, 2.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Glazed,3),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Chocolate,4.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Vanilla,4),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Lemon,3.5)
 };
        DoughnutType selectedDoughnut;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void glazedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mRaisedGlazed++;
            txtGlazedRaised.Text = mRaisedGlazed.ToString();
        }

        private void sugarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mRaisedSugar++;
            txtSugarRaised.Text = mRaisedSugar.ToString();
        }

        private void lemonMenuTitem_Click(object sender, RoutedEventArgs e)
        {
            mfilledLemon++;
            txtLemonFilled.Text = mfilledLemon.ToString();
        }

        private void chocolateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mfilledChocolate++;
            txtChocolateFilled.Text = mfilledChocolate.ToString();
        }

        private void vanillaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mfilledVanilla++;
            txtVanillaFilled.Text = mfilledVanilla.ToString();
        }
        private void FilledItemsShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;
            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            mesaj = SelectedItem.Header.ToString() + " doughnuts are being cooked!";
            this.Title = mesaj;
        }
        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key";
            cmbType.SelectedValuePath = "Value";
        }
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<DoughnutType, double> selectedEntry =
           (KeyValuePair<DoughnutType, double>)cmbType.SelectedItem;
            selectedDoughnut = selectedEntry.Key;
        }
        private int ValidateQuantity(DoughnutType selectedDoughnut)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;

            switch (selectedDoughnut)
            {
                case DoughnutType.Glazed:
                    if (q > mRaisedGlazed)
                        r = 0;
                    break;
                case DoughnutType.Sugar:
                    if (q > mRaisedSugar)
                        r = 0;
                    break;
                case DoughnutType.Chocolate:
                    if (q > mfilledChocolate)
                        r = 0;
                    break;
                case DoughnutType.Lemon:
                    if (q > mfilledLemon)
                        r = 0;
                    break;
                case DoughnutType.Vanilla:
                    if (q > mfilledVanilla)
                        r = 0;
                    break;
            }
            return r;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateQuantity(selectedDoughnut) > 0)
            {
                lstSale.Items.Add(txtQuantity.Text + " " + selectedDoughnut.ToString() +
               ":" + txtPrice.Text + " " + double.Parse(txtQuantity.Text) *
               double.Parse(txtPrice.Text));
                Total = Total + double.Parse(txtQuantity.Text) *
               double.Parse(txtPrice.Text);
                txtTotal.Text = Total.ToString();
            }
            else
            {
                MessageBox.Show("Cantitatea introdusa nu este disponibila in stoc!");
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = "0";

            foreach (string s in lstSale.Items)
            {
                switch (s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") -
               1))
                {
                    case "Glazed":
                        mRaisedGlazed = mRaisedGlazed - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtGlazedRaised.Text = mRaisedGlazed.ToString();
                        break;
                    case "Sugar":
                        mRaisedSugar = mRaisedSugar - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtSugarRaised.Text = mRaisedSugar.ToString();
                        break;
                    case "Chocolate":
                        mfilledChocolate = mfilledChocolate - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtChocolateFilled.Text = mfilledChocolate.ToString();
                        break;
                    case "Lemon":
                        mfilledLemon = mfilledLemon - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtLemonFilled.Text = mfilledLemon.ToString();
                        break;
                    case "Vanilla":
                        mfilledVanilla = mfilledVanilla - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtVanillaFilled.Text = mfilledVanilla.ToString();
                        break;
                }
            }
            lstSale.Items.Clear();
        }

        private void txtQuantity_KeyPress(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce!", "Input Error", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
    }
}


