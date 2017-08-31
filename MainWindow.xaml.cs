using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;

namespace TravelCards
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> cities = new List<string>();
        List<Card> cards = new List<Card>();
        Roadmap roadmap = new Roadmap();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog _OFD = new OpenFileDialog();
                _OFD.InitialDirectory = "";
                _OFD.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
                _OFD.FilterIndex = 1;
                _OFD.RestoreDirectory = true;

                _OFD.ShowDialog();
                if (_OFD.FileName!="")
                {
                    StreamReader sr = new StreamReader(_OFD.FileName);
                    string line = String.Empty;
                    while((line=sr.ReadLine())!=null)
                        cities.Add(line);
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Exception!\n" + ex.Message); }
        }

        private void btn_randomize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> tmpList = cities;
                Random rnd = new Random();
                int cardsCount = Int32.Parse(tb_count.Text);

                //формируем список карточек
                while (cardsCount>0)
                {
                    string city0 = tmpList[rnd.Next(tmpList.Count)];
                    string city1 = tmpList[rnd.Next(tmpList.Count)];
                    if (city0 != city1)
                    {
                        cards.Add(new Card(city0, city1));
                        cardsCount--;
                    }
                }

                //удаляем дубликаты
                for(int i=0;i<cards.Count;i++)
                {
                    var cs = new CardSearch(cards[i].DeparturePoint, cards[i].ArrivalPoint);
                    for(int j=i+1;j<cards.Count;j++)
                        if (cs.CardMatch(cards[j]))
                        {
                            MessageBox.Show(cards[j].DeparturePoint + " " + cards[j].ArrivalPoint + " removed.");
                            cards.Remove(cards[j]);
                        }
                }
                foreach(string c in cities)
                {
                    cb_start.Items.Add(c);
                    cb_finish.Items.Add(c);
                }

                //отображаем карточки
                DataTable dt = new DataTable();
                dt.Columns.Add("Отправление");
                dt.Columns.Add("Прибытие");
                foreach (Card c in cards)
                {
                    dt.Rows.Add(c.DeparturePoint, c.ArrivalPoint);
                }
                dataGrid0.ItemsSource = dt.DefaultView;

                List<Node> nodes = new List<Node>();

                foreach (string cityname in cities)
                    roadmap.AddCity(new City(cityname));
                foreach (Card c in cards)
                    roadmap.AddRoad(new Road(roadmap.GetCity(c.DeparturePoint), roadmap.GetCity(c.ArrivalPoint)));

                MessageBox.Show("DONE!\n" + cards.Count);
            }
            catch (Exception ex)
            { MessageBox.Show("Exception!\n" + ex.Message); }
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cb_start.SelectedItem == null || cb_finish.SelectedItem == null)
                {
                    MessageBox.Show("Укажите пункты отправления и/или прибытия");
                    return;
                }
                else
                {
                    List<Card> resultCards = roadmap.CalculateRoute(cb_start.SelectedItem.ToString(), cb_finish.SelectedItem.ToString());

                    if (resultCards.Count == 0)
                        MessageBox.Show("Не удалось построить маршрут между выбранными пунктами");
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Отправление");
                        dt.Columns.Add("Прибытие");
                        for (int i = resultCards.Count; i > 0; i--)
                            dt.Rows.Add(resultCards[i - 1].DeparturePoint, resultCards[i - 1].ArrivalPoint);
                        dataGrid1.ItemsSource = dt.DefaultView;

                        MessageBox.Show("DONE!");
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Exception!\n" + ex.Message); }
        }
    }
}
