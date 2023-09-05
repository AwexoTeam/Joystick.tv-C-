using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StupidityClient.Core;
using StupidityClient.Core.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WebSocket4Net;
using WebSocket = WebSocket4Net.WebSocket;

namespace StupidityClient
{
    public partial class Bot : Form
    {               
        public static string lastPing = "";
        public static Queue<Alert> alerts;

        private static bool isHandlingAlert;
        public static bool hasLoaded;

        public Bot()
        {
            InitializeComponent();

            if (Debugger.IsAttached)
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            alerts = new Queue<Alert>();

            DataManager.Initialize();
            BotManager.Initialize();
            
            EventManager.InvokeEvent(EventType.OnPreload, null);

            alertLabel.ForeColor = AlertSettings.settings.fontColor;

            string fontFamilyStr = AlertSettings.settings.fontFamily;
            int fontSize = AlertSettings.settings.fontSize;
            FontStyle fontStyle = AlertSettings.settings.fontStyle;

            FontFamily fontFamily = null;
            try
            {
                fontFamily = new FontFamily(fontFamilyStr);
            }
            catch (Exception)
            {
                Debug.LogError($"{fontFamilyStr} couldn't be found! Defualting to generic.");
                fontFamily = FontFamily.GenericSerif;

            }

            Font font = new Font(fontFamily, fontSize, fontStyle);
            alertDuration.Interval = AlertSettings.settings.alertDelay;
            
            hasLoaded = true;


        }

        DateTime lastTick = DateTime.Now;
        public static bool shouldTick = false;
        private void tickTimer_Tick(object sender, EventArgs e)
        {
            //TODO: do this less stupid and actually learn the thing.
            if (alerts.Count > 0)
                HandleAlert();

            EventManager.InvokeEvent(EventType.OnTick, lastTick);
            lastTick = DateTime.Now;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            => EventManager.InvokeEvent(EventType.OnClosing, null);

        private void HandleAlert()
        {
            Debug.Log("Alert Tick!");
            if (isHandlingAlert)
                return;

            Debug.Log("Alert processing!");
            Alert alert = alerts.Dequeue();

            alertPicture.ImageLocation = AlertSettings.settings.GetAlertPicture(alert.type);

            string text = AlertSettings.settings.GetAlertText(alert.type);
            text = text.Replace("{user}", alert.username);
            text = text.Replace("{info}", alert.otherInfo);

            alertLabel.Text = text;

            Debug.Log("Alert Showing!");
            alertPicture.Show();
            alertLabel.Show();

            string audio = AlertSettings.settings.GetAlertSound(alert.type);
            if(audio !=  null && audio != string.Empty)
            {
                SoundPlayer notif = new SoundPlayer(audio);
                notif.Play();
            }

            isHandlingAlert = true;
            alertDuration.Enabled = true;
        }



        private void alertDuration_Tick(object sender, EventArgs e)
        {
            Debug.Log("Cleaning up from alert");
            alertLabel.Hide();
            alertPicture.Hide();

            alertPicture.ImageLocation = "";
            alertLabel.Text = "";

            isHandlingAlert = false;
            alertDuration.Enabled = false;
        }
    }
}
