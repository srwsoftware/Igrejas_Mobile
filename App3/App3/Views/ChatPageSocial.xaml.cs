﻿using App3.Models;
using App3.Services;
using App3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPageSocial : ContentPage
    {
        RestService restService;
        User userChat;
        Social Social;
        private ChatSocialViewModel aaa;
        public ChatPageSocial(Social social)
        {
            InitializeComponent();
            Social = social;

            restService = new RestService();
            Shell.Current.FlyoutIsPresented = false;
            AtualizaUser();
            aaa = new ChatSocialViewModel(Social.Idsocial.ToString());
            this.BindingContext = aaa;


        }
        private async void AtualizaUser()
        {
            userChat = await restService.GetUserChatAsync(Social.Iduser.ToString());
            imagemT.Source = await restService.GetImagemServer(userChat.Imagem);
            titulo.Text = Social.Nomesocial;// userChat.Nome + " " + userChat.Apelido;

        }
        protected override void OnDisappearing()
        {
            aaa.myTimer.Stop();
        }
        protected override void OnAppearing()
        {
            aaa.myTimer.Start();
        }

    }
}