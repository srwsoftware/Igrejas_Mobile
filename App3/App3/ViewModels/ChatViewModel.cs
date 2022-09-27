﻿using App3.Models;
using App3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        RestService restService;
        RootMsg msg;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Mensagem> Messages { get; set; } = new ObservableCollection<Mensagem>();

        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }
        public Timer myTimer = new Timer();
        private string idRecetor;
        public ChatViewModel(string idrecetor)
        {
            idRecetor = idrecetor;
            AtualizaMsgFirst();
            myTimer.Elapsed += new ElapsedEventHandler(AtualizaMsg);
            myTimer.Interval = 2000;
            myTimer.Start();
            


            OnSendCommand = new Command(async () =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    EnviarMensagem();
                    TextToSend = string.Empty;
                }

            });
        }
        public async void EnviarMensagem()
        {
            string data = @"{'descmsg':'" + TextToSend + "','emissor':'" + await SecureStorage.GetAsync("iduser") + "','receptor':'"+idRecetor+ "','lido':'S'}";
            var dataal = data.Replace('\'', '\"');
            var res = await restService.SendMensagemAsync(dataal);
            if (res == null)
            {
                Console.WriteLine("Erro ao Enviar, Tente mais Tarde");
            }
            else if (res.IsSuccessStatusCode)
            {

                Console.WriteLine("Mensagem Enviada");
            }
        }
        
        public async void AtualizaMsg(object source, ElapsedEventArgs e)
        {
            try
            {
                
                restService = new RestService();
                msg = await restService.GetMensagensUserAsync(await SecureStorage.GetAsync("iduser"), idRecetor);
                List<Mensagem> listmsg = msg.data;
                listmsg.Reverse();
                //Console.WriteLine(listmsg[0].Dtmsg.ToString());
                
                foreach (Mensagem m in listmsg)
                {
                    if (!Messages.Any(u => u.Idmensagem == m.Idmensagem))
                    {
                        if (m.Idemissor == Int32.Parse(await SecureStorage.GetAsync("iduser")))
                        {
                            Messages.Add(new Mensagem() { Descmsg = m.Descmsg, Idemissor = Int32.Parse(await SecureStorage.GetAsync("iduser")), Idmensagem = m.Idmensagem, Idreceptor=m.Idreceptor, Emissor=m.Emissor,Receptor=m.Receptor,ImgEmissor=m.ImgEmissor,ImgReceptor=m.ImgReceptor, Dtmsg = m.Dtmsg });

                        }
                        else
                        {
                            Messages.Add(new Mensagem() { Descmsg = m.Descmsg, Idemissor = m.Idemissor, Idmensagem = m.Idmensagem, Idreceptor = m.Idreceptor, Emissor = m.Emissor, Receptor = m.Receptor, ImgEmissor = m.ImgEmissor, ImgReceptor = m.ImgReceptor, Dtmsg = m.Dtmsg });

                        }
                    }
                   
                }        
                
                
            }catch (Exception ex)
            {
                Messages.Add(new Mensagem() { Descmsg = ex.ToString() });
                Console.WriteLine(ex.ToString());
            }
            
        }

        public async void AtualizaMsgFirst()
        {
            try
            {

                restService = new RestService();
                msg = await restService.GetMensagensUserAsync(await SecureStorage.GetAsync("iduser"), idRecetor);
                List<Mensagem> listmsg = msg.data;
                listmsg.Reverse();
                Console.WriteLine(msg);

                foreach (Mensagem m in listmsg)
                {

                    if (!Messages.Any(u => u.Idmensagem == m.Idmensagem))
                    {
                        if (m.Idemissor == Int32.Parse(await SecureStorage.GetAsync("iduser")))
                        {
                            Messages.Add(new Mensagem() { Descmsg = m.Descmsg, Idemissor = Int32.Parse(await SecureStorage.GetAsync("iduser")), Idmensagem = m.Idmensagem, Idreceptor = m.Idreceptor, Emissor = m.Emissor, Receptor = m.Receptor, ImgEmissor = m.ImgEmissor, ImgReceptor = m.ImgReceptor, Dtmsg = m.Dtmsg });

                        }
                        else
                        {
                            Messages.Add(new Mensagem() { Descmsg = m.Descmsg, Idemissor = m.Idemissor, Idmensagem = m.Idmensagem, Idreceptor = m.Idreceptor, Emissor = m.Emissor, Receptor = m.Receptor, ImgEmissor = m.ImgEmissor, ImgReceptor = m.ImgReceptor, Dtmsg = m.Dtmsg });

                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Messages.Add(new Mensagem() { Descmsg = ex.ToString() });
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
