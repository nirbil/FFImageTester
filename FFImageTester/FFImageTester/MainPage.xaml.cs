using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FFImageTester
{
    public partial class MainPage : ContentPage
    {
        List<string> _sources = new List<string>
        {
            "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/2016_Renault_Twingo_III_SCe_70_engine.jpg/1200px-2016_Renault_Twingo_III_SCe_70_engine.jpg",
            "https://upload.wikimedia.org/wikipedia/commons/8/84/Balanceshaft.jpg",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Fullerene_Nanogears_-_GPN-2000-001535.jpg/1200px-Fullerene_Nanogears_-_GPN-2000-001535.jpg",
            "https://upload.wikimedia.org/wikipedia/commons/c/cb/Salon_de_Madame_Geoffrin.jpg",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Mineralogy_between_its_other_sciences_around.png/1200px-Mineralogy_between_its_other_sciences_around.png",
            "https://upload.wikimedia.org/wikipedia/commons/5/5b/Diamond_cuboctahedron.jpg",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/2/22/Carnot_heat_engine_2.svg/1200px-Carnot_heat_engine_2.svg.png",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Hydrogen_Density_Plots.png/1200px-Hydrogen_Density_Plots.png"
        };
        int _currSourceIndex = -1;
        int _iterationDelay = 2000;
        
        public MainPage()
        {
            InitializeComponent();
            SetNextSource();
        }

        private void SetCurrStatus(string status)
        {
            if (Device.IsInvokeRequired)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    statusLabel.Text = "Status: " + _currSourceIndex + " " + status;
                });
            }
            
            else
            {
                statusLabel.Text = "Status: " + _currSourceIndex + " " + status;
            }
        }

        private void SetNextSource()
        {
            Task.Delay(_iterationDelay).ContinueWith((task) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _currSourceIndex = (_currSourceIndex + 1) % _sources.Count;
                    svgCachedImage.Source = ImageSource.FromUri(new Uri(_sources[_currSourceIndex]));
                    SetCurrStatus("starting");
                });
            });
        }


        private void SvgCachedImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            Trace.WriteLine("error event, " + e.ToString());
            SetCurrStatus("failed");
        }

        private void SvgCachedImage_Success(object sender, FFImageLoading.Forms.CachedImageEvents.SuccessEventArgs e)
        {
            Trace.WriteLine("success event, " + e.ToString());
            SetCurrStatus("success");
        }

        private void SvgCachedImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
            Trace.WriteLine("finished event, " + e.ToString());
            SetNextSource();
        }
    }
}
