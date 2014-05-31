using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Library;
using Library.Responses;

namespace ReviewActivityInterface
{
    public partial class Form1 : Form
    {
        private static readonly string[] Labels =
        {
            "Suggested Edits", "Close Votes", "Low Quality posts", "First Posts",
            "Late Answers", "Reopen"
        };

        private static readonly int[] Values = {6, 7, 2, 15, 5, 0};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (var index = 0; index < Labels.Length; index++)
            {
                var label = Labels[index];
                var series = new Series(label);
                series.ChartType = SeriesChartType.Column;
                series.XValueType = ChartValueType.Int32;
                chart1.Series.Add(series);
            }

            chart1.Refresh();
            //var settings = new ReviewDashboardActivityRequestParameters
            //{
            //    SiteId = 1
            //};

            //settings.OnReviewActivity += OnReviewActivityDataReceived;

            //var socket = new StackSocket("wss://qa.sockets.stackexchange.com", settings);
            //socket.Connect();
        }

        private void OnReviewActivityDataReceived(object sender, SocketEventArgs e)
        {
            AddEntry((int) ((e.Response.Data as ReviewDashboardActivityData).Activity));
        }

        private void AddEntry(int type)
        {
            var total = ++Values[type];
            Values[type] = total;

            chart1.BeginInvoke(new Action(() => { chart1.Series[type].Points.Add(1); }));
        }
    }
}