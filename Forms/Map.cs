﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        readonly GMapOverlay top = new GMapOverlay();
        internal readonly GMapOverlay objects = new GMapOverlay("objects");
        internal readonly GMapOverlay routes = new GMapOverlay("routes");
        internal readonly GMapOverlay polygons = new GMapOverlay("polygons");
        GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
        GDirections routedir;
        GMapOverlay tablerouteoverlay;
        PointLatLng start;
        PointLatLng end;
        static GMapMarkerVan vanMarker;
        static GMapRoute tablemaproute;
        private List<PointLatLng> _points;
        public Form1()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            // put key here
            GoogleMapProvider.Instance.ApiKey = "AIzaSyC2V-ZH_EDYWXJ2l82ZRB1OV4Pd95k1YuI";
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
            var start = new PointLatLng(46.299106, -119.295999); //current van
            var end = new PointLatLng(46.276860, -119.291511);//appoinmet
            GDirections route;
            GMapOverlay markersList = new GMapOverlay("markers");

            var xx = GMap.NET.MapProviders.GMapProviders.GoogleMap.GetDirections(out route, start, end, false, false, false, false, false);
            tablemaproute = new GMapRoute(route.Route, "Ruta ubication");
            tablerouteoverlay = new GMapOverlay("Capa de la ruta");
            tablerouteoverlay.Routes.Add(tablemaproute);
            gMapControl1.Overlays.Add(tablerouteoverlay);

            GMap.NET.WindowsForms.Markers.GMarkerGoogle startPoint = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(start, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            GMap.NET.WindowsForms.Markers.GMarkerGoogle endPoint = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(end, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            vanMarker = new GMapMarkerVan(start);

            //markersList.Markers.Add(startPoint);
            //markersList.Markers.Add(endPoint);
            markersList.Markers.Add(vanMarker);
            gMapControl1.Overlays.Add(markersList);
            gMapControl1.Zoom = 14;

           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();                // minimizes current page
            Form4 DriversForm = new Form4();  // creates new add driver
            DriversForm.ShowDialog();         // shows adddriver
            this.Close();               // exits menu page

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();                // minimizes current page
            Form3 VansForm = new Form3();  // creates new addvan page
            VansForm.ShowDialog();         // shows addvan
            this.Close();               // exits menu page
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < tablemaproute.Points.Count; i++)
            {
                animate(1000);
            }
        }
        public void animate(int Timeout)
        {
            Thread t = new Thread(() =>
            {
                for (int i = 1; i < tablemaproute.Points.Count; i++)
                {
                    var degree = Bearing(tablemaproute.Points[i - 1], tablemaproute.Points[i]);
                    degree = (degree * 180 / Math.PI + 360) % 360;
                    vanMarker.Rotate((float)degree);
                    for (double s = 0; s < 1; s = s + 0.05)
                    {
                        var dlat = s * (tablemaproute.Points[i].Lat) + (1 - s) * (tablemaproute.Points[i - 1].Lat);
                        var dlng = s * (tablemaproute.Points[i].Lng) + (1 - s) * (tablemaproute.Points[i - 1].Lng);
                        var M_point = new PointLatLng(dlat, dlng);
                        Thread.Sleep((int)(Timeout * 0.05));
                        vanMarker.Position = M_point;
                    }
                }
            }
            );
            t.Start();
        }

        public double Bearing(PointLatLng p1, PointLatLng p2)
        {
            //Convert input values to radians   
            var lat1 = DegreeToRadian(p1.Lat);
            var long1 = DegreeToRadian(p1.Lng);
            var lat2 = DegreeToRadian(p2.Lat);
            var long2 = DegreeToRadian(p2.Lng);

            double deltaLong = long2 - long1;
            double y = Math.Cos(lat2) * Math.Sin(deltaLong);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLong); ;

            double bearing = Math.Atan2(y, x);

            return bearing;
        }

        public class GMapMarkerVan : GMapMarker
        {

            private readonly Bitmap icon = new Bitmap(Path.Combine(Application.StartupPath, @"..\..\img\van_xs_y.png"));
            float heading = 0;

            public GMapMarkerVan(PointLatLng p) : base(p)
            {
                this.heading = 0;
                Size = icon.Size;
            }
            public void Rotate(float degre)
            {
                this.heading = degre;
            }

            public override void OnRender(Graphics g)
            {
                Matrix temp = g.Transform;
                g.TranslateTransform(LocalPosition.X, LocalPosition.Y);
                g.RotateTransform(-Overlay.Control.Bearing);

                try
                {
                    g.RotateTransform(heading);
                }
                catch { }

                g.DrawImageUnscaled(icon, -icon.Width / 4, -icon.Height / 4);
                g.Transform = temp;
            }
        }

        public double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}