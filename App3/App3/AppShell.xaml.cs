﻿using App3.ViewModels;
using App3.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App3
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(HomeLayout2), typeof(HomeLayout2));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            
        }

        

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        private async void OnLogout(object sender, EventArgs e)
        {
            //SecureStorage.RemoveAll();
            SecureStorage.Remove("iduser");
            SecureStorage.Remove("tokenuser");
            SecureStorage.Remove("remember");
            await Shell.Current.GoToAsync("//MainPage");
        }
        private async void Irchat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatPage("1"));
        }
        private async void TapGestureRecognizer_Tapped_Config(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigPage());
        }
        protected override bool OnBackButtonPressed()
        {
            // true or false to disable or enable the action
            Navigation.PushAsync(new HomeLayout2());
            return base.OnBackButtonPressed();
        }



    }
}
