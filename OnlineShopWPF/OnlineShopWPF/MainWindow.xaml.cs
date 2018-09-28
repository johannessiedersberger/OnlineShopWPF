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
using OnlineShop;

namespace OnlineShopWPF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      ShowNotebooks();
    }

    private void ShowNotebooks()
    {
      DatabaseFactory dbF = new DatabaseFactory(new MySqliteDatabase(Shop.file));
      List<Product> products = dbF.FindMatchingProducts(new NotebookQueryParams { });
      foreach (Product notebookProduct in products)
      {
        Notebook nb = dbF.GetNotebook(notebookProduct.ProductId);
        NotebookView view = new NotebookView
        {
          Name = notebookProduct.Name,
          Price = notebookProduct.Price,
          Cpu = dbF.GetCPU(nb.CpuId).Name,
          Ram = nb.RamMemory,
          HdMemory = dbF.GetHardDrive(nb.HardDriveId).Memory,
          HdType = dbF.GetHardDrive(nb.HardDriveId).Type,
        };
        DataGridTest.Items.Add(view);
      }
    }
  }

  public class NotebookView
  {
    public string Name { get; set; }
    public double Price { get; set; }
    public string Cpu { get; set; }
    public int Ram { get; set; }
    public int HdMemory { get; set; }
    public string HdType { get; set; }
  }
}
