﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using ToggleSwitch;
using URAN_2017;


namespace URAN_2017.FolderSetUp
{
    /// <summary>
    /// Логика взаимодействия для PageOtbor.xaml
    /// </summary>
    public partial class PageOtbor : Page
    {
        UserSetting set = new UserSetting();
        ClassOtborNeutron otb = new ClassOtborNeutron();
        public PageOtbor()
        {
           this.InitializeComponent();
            DeSerial();
            DlitNeu.Text = otb.Dlit.ToString();
            PorogNeutrona.Text = otb.Porog.ToString();
            //checOtbor.IsChecked = UserSetting.FlagOtbor;
            Bu.Toggled1 = !UserSetting.FlagOtbor;
           



        }
        private  void Window_Activated(object sender, EventArgs e)
        {

          
            

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Serial();
            
        }

  
        private void Serial()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам
            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            BinaryFormatter bf = new BinaryFormatter();
            Stream fs;
            using (fs = new FileStream(md + "\\UranSetUp\\" + "ClassOtborNeutron.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bf.Serialize(fs, otb);
             

            }
            fs.Close();


            BinaryFormatter bf1 = new BinaryFormatter();
            using (Stream fs1 = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                try
                {
                    bf1.Serialize(fs1, set);
                    System.Windows.MessageBox.Show("Сохранено");
                }
                catch
                {

                }
                finally
                {
                    fs1.Close();
                }

            }
            UserSetting.Serial();


            if (Directory.Exists(md + "\\UranSetUp") == false)
            {
                Directory.CreateDirectory(md + "\\UranSetUp");
            }
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
            using (StreamWriter wr = new StreamWriter(md + "\\UranSetUp\\" + "setting1.xml"))
            {
                xs.Serialize(wr, Bak._DataColec1);
                //  xs.Serialize(wr, Bak._DataColec1NoTail);
                wr.Close();
            }
            XmlSerializer xs100 = new XmlSerializer(typeof(ObservableCollection<Bak>));
            using (StreamWriter wr100 = new StreamWriter(md + "\\UranSetUp\\" + "settingBAAK12-100.xml"))
            {
                xs.Serialize(wr100, Bak._DataColecBAAK100);
                //  xs.Serialize(wr, Bak._DataColec1NoTail);
                wr100.Close();
            }


        }
        private void DeSerial()
        {
            try
            {

                string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//путь к Документам

                FileStream fs = new FileStream(md + "\\UranSetUp\\" + "ClassOtborNeutron.dat", FileMode.Open);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    otb = (ClassOtborNeutron)bf.Deserialize(fs);
                    fs.Close();

                }
                catch (SerializationException)
                {
                    System.Windows.MessageBox.Show("ошибка");
                    fs.Close();
                }
                finally
                {
                    fs.Close();
                }

                Bak.InstCol();


                FileStream fs1 = new FileStream(md + "\\UranSetUp\\" + "setting.dat", FileMode.Open);
                try
                {
                    BinaryFormatter bf1 = new BinaryFormatter();
                    set = (UserSetting)bf1.Deserialize(fs1);

                }
                catch (SerializationException)
                {
                    System.Windows.MessageBox.Show("ошибка");
                }
                finally
                {
                    fs1.Close();
                }
                try
                {


                    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Bak>));
                    using (StreamReader wr = new StreamReader(md + "\\UranSetUp\\" + "setting1.xml"))
                    {
                        Bak._DataColec1 = (ObservableCollection<Bak>)xs.Deserialize(wr);
                        wr.Close();

                    }
                }
                catch (Exception)
                {

                }



            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Ошибка серилизации");
            }

        }

        private void DlitNeu_TextChanged(object sender, TextChangedEventArgs e)
        {
            otb.Dlit = Convert.ToInt32(DlitNeu.Text);
        }

        private void PorogNeutrona_TextChanged(object sender, TextChangedEventArgs e)
        {
            otb.Porog = Convert.ToInt32(PorogNeutrona.Text);
        }

        private void HorizontalToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            //ToggleSwitch.HorizontalToggleSwitch rb = sender as HorizontalToggleSwitch;
            //UserSetting.FlagOtbor = true;
          
        }

        private void HorizontalToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            // ToggleSwitch.HorizontalToggleSwitch rb = sender as HorizontalToggleSwitch;
           // UserSetting.FlagOtbor = false;
           
        }
        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            if (Bu.Toggled1 == true)
            {
                UserSetting.FlagOtbor = true;


            }
            else
            {
                UserSetting.FlagOtbor = false;
            }


        }
        
    }
}
