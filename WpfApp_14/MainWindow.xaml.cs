﻿using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
using WpfApp_14.f.mod;

namespace WpfApp_14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            List<Product> products= new();
            DataContext = this;
            this.Loaded += Sqlite_Loaded;

        }

        private void Sqlite_Loaded(object sender, RoutedEventArgs e)
        {
            Sq sq = new Sq();
             List<Product> products = sq.Products.ToList();
            ProductsList.ItemsSource = products;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            foreach (Product product in products)
            {
                string combined = "Уникальный идентификатор: " + product.ID + "\r\n" + "Имя товара: " + product.Name + "\r\n" + "Описание товара: " + product.Description + "\r\n" + "Цена товара: " + product.Price + " RUB";
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(combined, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                BitmapImage qrCodeImage = new BitmapImage();
                using (MemoryStream stream = new MemoryStream())
                {
                    qrCode.GetGraphic(20).Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    qrCodeImage.BeginInit();
                    qrCodeImage.CacheOption = BitmapCacheOption.OnLoad;
                    qrCodeImage.StreamSource = stream;
                    qrCodeImage.EndInit();
                }

                ListProduct.Add(new Product { Name = product.Name, Price = product.Price, QRCode = qrCodeImage, Description = product.Description, ID = product.ID });
            }
            ProductsList.ItemsSource = ListProduct;
        }

            private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            f.D add = new f.D();
            Close();
            add.ShowDialog();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {

                var product = ProductsList.SelectedItem as Product;
                if (new f.Ep(product).ShowDialog() == true)
                {
                    using (var context = new Sq())
                    {
                        context.Entry(product).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    ProductsList.Items.Refresh();
                }
            }
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }

        private void btn_delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var product = ProductsList.SelectedItem as Product;
                    using (var context = new Sq())
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                        ProductsList.ItemsSource = context.Products.ToList();


                    }
                }
            }
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }
    }

   
    
}
