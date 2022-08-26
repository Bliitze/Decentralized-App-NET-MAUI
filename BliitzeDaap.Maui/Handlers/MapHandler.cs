using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace BliitzeDaap.Maui.Handlers
{
    partial class MapHandler
    {
        public static IPropertyMapper<MapView, MapHandler> MapMapper = new PropertyMapper<MapView, MapHandler>(ViewHandler.ViewMapper)
        { };

        public MapHandler() : base(MapMapper)
        { }
    }
}
