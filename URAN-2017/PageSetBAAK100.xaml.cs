﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using URAN_2017.FolderSetUp;
using URAN_2017.WorkBD;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для PageSetBAAK100.xaml
    /// </summary>
    public partial class PageSetBAAK100 : Page
    {
        UserSetting set;
        public PageSetBAAK100()
        {
            InitializeComponent();
            
            try
            {
                set = new UserSetting();
                DeSerial();
                WaySet.Text = set.WaySetup;
            }
            catch(Exception ex)
            {

            }
            list.ItemsSource = Bak._DataColecBAAK100;
        }
        private void Serial()
        {
            ClassSerilization.SerialUserSetting100(set);
            UserSetting.Serial();

        }
        private void DeSerial()
        {
            try
            {
                Bak.InstCol();
                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                ClassSerilization.DeSerialUserSetting100(out set);
                try
                {

                    md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
                    {
                        Bak._DataColecBAAK100 = (ObservableCollection<Bak>)xs.Deserialize(wr);

                    }
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Ошибка серилизации", "Ошибка");
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации", "Ошибка");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Serial();
            System.Windows.MessageBox.Show("Настройки успешно сохранены", "Настройки работы программы");
        }

        private void WaySet_TextChanged(object sender, TextChangedEventArgs e)
        {
            set.WaySetup = WaySet.Text;
        }

        private void ButWaySet_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog myDialog = new OpenFileDialog
            {
               Filter = "База данных(*.MDB;*.MDB;*.accdb; *.db; *.db3)|*.MDB;*.MDB;*.ACCDB;*DB; *DB3;" + "|Все файлы (*.*)|*.* ",
               CheckFileExists = true,
                Multiselect = true
            };
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                WaySet.Text = myDialog.FileName;
            }


        }

      
    }
}
