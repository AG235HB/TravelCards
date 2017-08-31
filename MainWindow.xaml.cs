using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
                
                    //string path = _OFD.FileName;
                    StreamReader sr = new StreamReader(_OFD.FileName);
                    string line = String.Empty;
                    while((line=sr.ReadLine())!=null)
                    {
                        cities.Add(line);
                        //cb_start.Items.Add(line);
                        //cb_finish.Items.Add(line);
                    }
                
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
                    {
                        if (cs.CardMatch(cards[j]))
                        {
                            MessageBox.Show(cards[j].DeparturePoint + " " + cards[j].ArrivalPoint + " removed.");
                            cards.Remove(cards[j]);
                        }
                    }
                }
                foreach(Card c in cards)
                {
                    cb_start.Items.Add(c.DeparturePoint);
                    cb_finish.Items.Add(c.ArrivalPoint);
                }
                //перемешиваем карточки
                //for (int i=0;i<cards.Count*2;i++)
                //{
                //    int a = rnd.Next(cards.Count), b = rnd.Next(cards.Count);
                //    Card tempCard = cards[a];
                //    cards[a] = cards[b];
                //    cards[b] = tempCard;
                //}
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
                //Node n0 = new Node("0");
                //Node n1 = new Node("1");
                //n0.AddCity(n1.DepCity);
                //строим граф
                //int counter = 0;
                //while(cards.Count>0)
                //{
                //    Node node = new Node(cards[0].DeparturePoint);
                //    node.AddCity(cards[0].ArrivalPoint);
                //    FillChildren(cards, node);
                //    nodes.Add(node);

                //    counter++;
                //}

                foreach (string cityname in cities)
                    roadmap.AddCity(new City(cityname));
                foreach (Card c in cards)
                    roadmap.AddRoad(new Road(roadmap.GetCity(c.DeparturePoint), roadmap.GetCity(c.ArrivalPoint)));
                //roadmap.AddRoad(new Road(new City(c.DeparturePoint), new City(c.ArrivalPoint)));

                /*
                for(int i=0;i<cards.Count;i++)
                {
                    Node node = new Node(cards[i].DeparturePoint);
                    node.AddCity(cards[i].ArrivalPoint);
                    //for(int j=i;j<cards.Count;j++)
                    //{
                    //    if (cards[j].DeparturePoint==node.DepCity)
                    //    {
                    //        node.AddCity(cards[j].ArrivalPoint);
                    //        cards.Remove(cards[j]);
                    //    }
                    //}
                    FillChildren(cards, node);
                    nodes.Add(node);
                }
                */

                MessageBox.Show("DONE!\n" + cards.Count);
            }
            catch (Exception ex)
            { MessageBox.Show("Exception!\n" + ex.Message); }
        }

        /*private void FillChildren(List<Card> cards, Node node)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if(cards[i].DeparturePoint==node.DepCity)
                {
                    node.AddCity(cards[i].ArrivalPoint);
                    cards.Remove(cards[i]);
                }
            }
            foreach(Node n in node.ArrCities)
            {
                for (int i=0;i<cards.Count;i++)
                {
                    if (cards[i].DeparturePoint==n.DepCity)
                    {
                        n.AddCity(cards[i].ArrivalPoint);
                        cards.Remove(cards[i]);
                    }
                }
                foreach (Node arrNode in n.ArrCities)
                    FillChildren(cards, arrNode);
            }
        }*/

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
                    //List<Card> sortedCards = new List<Card>();

                    //List<List<Card>> res = new List<List<Card>>();
                    /*List<Road> resultRoute = */List<Card> resultCards = roadmap.CalculateRoute(cb_start.SelectedItem.ToString(), cb_finish.SelectedItem.ToString());


                    //for (int i = 0; i < cards.Count; i++)
                    //{
                    //    sortedCards.Add(FindCard(sortedCards[sortedCards.Count - 1].ArrivalPoint));
                    //    if (sortedCards[i].ArrivalPoint == cb_finish.SelectedValue.ToString())
                    //    {
                    //        MessageBox.Show("Маршрут построен");
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
                    }
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Не удалось построить маршрут между выбранными пунктами");
                        //        break;
                        //    }
                    //}

                    MessageBox.Show("DONE!");
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Exception!\n" + ex.Message); }
        }
    }
}
