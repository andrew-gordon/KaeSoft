using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kae.GraphLibrary;

namespace DemoApp
{
    public class PlaceLink : Edge<Place>
    {
        /// <summary>
        /// The distance between the places.
        /// </summary> 
        public float Distance { get; set; }
        public TimeSpan TravelTime { get; set; }

        public PlaceLink(Place place1, Place place2, float distance, TimeSpan travelTime)
        {
            EndPoint1 = place1;
            EndPoint2 = place2;
            Distance = distance;
            TravelTime = travelTime;
        }
    }
}
