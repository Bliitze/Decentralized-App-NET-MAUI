using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Android.OS;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BliitzeDapp.SmartContract.LocationContract.ContractDefinition;
using Android.Graphics;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Android.Content;
using Microsoft.Maui.Controls;
using Android.Media;
using BliitzeDaap.Maui.Helpers;

namespace BliitzeDaap.Maui.Handlers
{
    
    public partial class MapHandler : ViewHandler<MapView, Android.Gms.Maps.MapView>
    {
        public BlockchainETH blockchainETH = new BlockchainETH();
        private MapHelper _mapHelper;

        internal static Bundle Bundle { get; set; }

        public MapHandler(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        { }

        protected override Android.Gms.Maps.MapView CreatePlatformView()
        {
            return new Android.Gms.Maps.MapView(Context);
        }

        protected override void ConnectHandler(Android.Gms.Maps.MapView platformView)
        {
            base.ConnectHandler(platformView);

            _mapHelper = new MapHelper(Bundle, platformView);
            _mapHelper.MapIsReady += _mapHelper_MapIsReady;
            _mapHelper.CallCreateMap();
        }

        private async void _mapHelper_MapIsReady(object sender, EventArgs e)
        {
            _mapHelper.Map.UiSettings.ZoomControlsEnabled = true;
            _mapHelper.Map.UiSettings.CompassEnabled = true;
            // blockchainETH.GetAllLocations();
            if (App.LocationList.Count >0)
            {
                
                foreach (BliitzeDapp.SmartContract.LocationContract.ContractDefinition.Location locationc in App.LocationList)
                {
                    MarkerOptions markerOpt1 = new MarkerOptions();
                    double lat = Convert.ToDouble(locationc.Latitude);
                    double lng = Convert.ToDouble(locationc.Longitude);
                    LatLng location = new LatLng(lat, lng);
                    markerOpt1.SetPosition(new LatLng(location.Latitude, location.Longitude));
                    markerOpt1.SetTitle(locationc.Label);
                    ImageSource image = ImageSource.FromFile("dotnet_bot.png");

                    var context = Android.App.Application.Context;
                    Bitmap bitmap = await GetImageFromImageSource(image, context);
                    var bmDescriptor = BitmapDescriptorFactory.FromBitmap(bitmap);
                    markerOpt1.SetIcon(bmDescriptor);

                    _mapHelper.Map.AddMarker(markerOpt1);
                }
            }
           
           

        }

        private async Task<Bitmap> GetImageFromImageSource(ImageSource imageSource, Context context)
        {
            IImageSourceHandler handler;

            if (imageSource is FileImageSource)
            {
                handler = new FileImageSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler(); // sic
            }
            else if (imageSource is UriImageSource)
            {
                handler = new ImageLoaderSourceHandler(); // sic
            }
            else
            {
                throw new NotImplementedException();
            }

            var originalBitmap = await handler.LoadImageAsync(imageSource, context);

            //var blurredBitmap = await Task.Run(() => CreateBlurredImage(originalBitmap, 25));

            return originalBitmap;
        }
    }


    class MapHelper : Java.Lang.Object, IOnMapReadyCallback
    {
        private Bundle _bundle;
        private Android.Gms.Maps.MapView _mapView;

        public event EventHandler MapIsReady;

        public GoogleMap Map { get; set; }

        public MapHelper(Bundle bundle, Android.Gms.Maps.MapView mapView)
        {
            _bundle = bundle;
            _mapView = mapView;
        }

        public void CallCreateMap()
        {
            _mapView.OnCreate(_bundle);
            _mapView.OnResume();
            _mapView.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            LatLng location = new LatLng(27.578836, -99.496933);
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(location, 13);
           
            Map = googleMap;
            Map.MoveCamera(cameraUpdate);
          
            MapIsReady?.Invoke(this, EventArgs.Empty);
        }

        
    }
  
}
