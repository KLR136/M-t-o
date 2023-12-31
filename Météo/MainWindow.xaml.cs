﻿using System;
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

using System.Net.Http;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Météo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Clouds
    {
        public int all { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Root
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public async Task<string> Getweather()
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=annecy,fr&appid=c21a75b667d6f7abb81f118dcf8d4611&units=metric");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Root root = JsonConvert.DeserializeObject<Root>(content);
                    Main main = root.main;
                    Tb_Temp.Text = "La température de " + main.temp.ToString() + "°C.";
                    Tb_Humi.Text = "L'humidité est de " + main.humidity.ToString() + "%.";
                    Tb_Max.Text = "La température maximum est de " + main.temp_max.ToString() + "°C.";
                    Tb_Min.Text = "La température minimum est de " + main.temp_min.ToString() + "°C.";
                    Tb_Res.Text = "Le ressenti est de " + main.feels_like.ToString() + "°C.";
                    return main.temp.ToString();
                }
                else
                {
                    var tt = "error";
                }
                return null;
            }
            catch (Exception ex) {
                return "";
            }
        }
    }
}