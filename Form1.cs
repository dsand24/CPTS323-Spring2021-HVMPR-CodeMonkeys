using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;


namespace CodeMonkeys___HVMPR_Project
{
    public partial class Form1 : Form
    {
        private List<PointLatLng> _points;
        public Form1()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            // put key here
            gMapControl1.Position = new PointLatLng(46.271701, -119.194216);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 22;
            gMapControl1.Zoom = 12;
            gMapControl1.ShowCenter = false;

            _points = new List<PointLatLng>();
        }

        //public GMapOverlay markersOverlay { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            _points.Add(new PointLatLng(46.19584834687376, -119.19698148649577)); // home
            _points.Add(new PointLatLng(46.21425799472811, -119.1058117569244)); // lampson


            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(_points[0], GMarkerGoogleType.green);   // home
            GMarkerGoogle marker2 = new GMarkerGoogle(_points[1], GMarkerGoogleType.green); // lampson
            markersOverlay.Markers.Add(marker);
            markersOverlay.Markers.Add(marker2);
            gMapControl1.Overlays.Add(markersOverlay); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _points.Add(new PointLatLng(46.19584834687376, -119.19698148649577)); // home
            _points.Add(new PointLatLng(46.21425799472811, -119.1058117569244)); // lampson

            GMapOverlay routeOverlay = new GMapOverlay("routes");              // create route overlay called routeOverlay

            var route = GoogleMapProvider.Instance.GetRoute(_points[0], _points[1], false, false, 10);   // generate route 
            var r = new GMapRoute(route.Points, "Test Route");                                          // pass generated route into gmap

            r.IsHitTestVisible = true;
            r.Stroke = new Pen(Color.Red, 3);

            routeOverlay.Routes.Add(r);                  // add route to overlay
            gMapControl1.Overlays.Add(routeOverlay);    // turn route overlay on
        }
    }
}