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
using System.Windows.Shapes;
using WpfApp_14.f.mod;

namespace WpfApp_14.f
{
    /// <summary>
    /// Логика взаимодействия для Ep.xaml
    /// </summary>
    public partial class Ep : Window
    {
         public Product Product { get; set; }
            public Ep(Product product)
            {
                InitializeComponent();
                Product = product;
                DataContext = Product;
            }


            private void btn_add_Click(object sender, RoutedEventArgs e)
            {

                DialogResult = true;

                Product product = new Product
                {
                    Price = Product.Price,
                    Name = Product.Name,
                    ID = Guid.NewGuid(),
                    Description = Product.Description
                };
                string show = "Уникальный идентификатор: " + Product.ID + "\r\n" + "Имя товара: " + Product.Name + "\r\n" + "Описание товара: " + Product.Description + "\r\n" + "Цена товара: " + Product.Price + " RUB";
                MessageBox.Show(show, "Изменные данные, занесенные в базу данных");
                Close();


            }
            private void btn_cancel_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = false;
                Close();
            }

        
    }
}
