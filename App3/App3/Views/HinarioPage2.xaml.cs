﻿using App3.Models;
using App3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HinarioPage2 : ContentPage
    {
        RestService restService;
        List<Hinario> hinarioList = new List<Hinario>();
        RootHinario hin;
        public ObservableCollection<Hinario> Hinarios { get; set; } = new ObservableCollection<Hinario>();
        public HinarioPage2()
        {
            InitializeComponent();
            restService = new RestService();
            AtualizaHinario();




        }
        public HinarioPage2(string nome)
        {
            InitializeComponent();
            restService = new RestService();
            AtualizaHinario();
            titulo.Text = nome;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView lv = (ListView)sender;

            // this assumes your List is bound to a List<Club>
            var item = (Hinario)lista.SelectedItem;
            var itemstrig = item.NomeHinario;

            // assuiming Club has an Id property
            await Navigation.PushAsync(new HinarioPage3(itemstrig.ToString()));
        }

        async void AtualizaHinario()
        {
            hin = await restService.GetHinariosAsync();
            hinarioList = hin.data;
            hinarioList.RemoveAt(0);
            foreach (var item in hinarioList)
            {
                Hinarios.Add(item);
            }
            lista.ItemsSource = Hinarios;

        }
        private void MySearchBarOnTextChangedHin(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            // Has Cancel has been pressed?
            if (textChangedEventArgs.NewTextValue == null)
            {
                Hinarios.Clear();
                foreach (var item in hinarioList)
                {
                    Hinarios.Add(item);

                }
            }

            var txtsearch = pesquisaH.Text;
            Hinarios.Clear();
            if (txtsearch == null || txtsearch.Length == 0 || txtsearch == "")
            {
                foreach (var item in hinarioList)
                {
                    Hinarios.Add(item);

                }
            }
            else
            {


                foreach (var item in hinarioList)
                {
                    if (item.NomeHinario.IndexOf(txtsearch, StringComparison.OrdinalIgnoreCase) >= 0)
                        Hinarios.Add(item);

                }
            }

        }
    }
}