﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IgrejaPage : ContentPage
    {
        public IgrejaPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped_MaisPerto(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MaisProxPage());
        }
    }
}