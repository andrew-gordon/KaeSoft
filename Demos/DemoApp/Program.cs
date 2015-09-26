using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Kae.GraphLibrary;

namespace DemoApp
{
    public static class Program
    {
        public static void Main()
        {
            Place placeA, placeB, placeC, placeD, placeE, placeF;

            IEnumerable<Place> places = new[]
            {
                placeA = new Place { Name = "Wheathampstead" },
                placeB = new Place { Name = "Sutton" },
                placeC = new Place { Name = "St. Albans" },
                placeD = new Place { Name = "Manchester" },
                placeE = new Place { Name = "Harpenden" },
                placeF = new Place { Name = "Oxford" }
            };

            IEnumerable<PlaceLink> placeLinks = new[] 
            {

                new PlaceLink(placeB, placeC, 52.6F , new TimeSpan(1, 7, 0) ),
                new PlaceLink(placeA, placeC, 4.6F  , TimeSpan.FromMinutes(12)  ),
                new PlaceLink(placeD, placeE, 173.7F, new TimeSpan(3, 6, 0)),
                new PlaceLink(placeA, placeE, 3.7F  , TimeSpan.FromMinutes(9)  ),
                new PlaceLink(placeC, placeE, 4.7F  , TimeSpan.FromMinutes(9)  ),
                new PlaceLink(placeB, placeF, 74 , TimeSpan.FromMinutes(90) ),
                new PlaceLink(placeF, placeD, 157, new TimeSpan(2, 51, 0)),
            };

            IGraph<Place, PlaceLink> graph = new UndirectedGraph<Place, PlaceLink>(places, placeLinks);

            Console.WriteLine();
            Console.WriteLine();

            if (true & graph.Edges != null)
            {
                CalculateShortestRoute(graph, placeB, placeD);
                Console.WriteLine();
                Console.WriteLine();
                CalculateQuickestRoute(graph, placeB, placeD);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        private static void CalculateShortestRoute(
            IGraph<Place, PlaceLink> graph,
            Place placeA, 
            Place placeB)
        {
            Contract.Requires<ArgumentNullException>(graph != null);
            Contract.Requires<ArgumentNullException>(graph.Edges != null);

            var routeFinder = new Dijkstra<Place, PlaceLink, float>(graph, n => n.Distance,
                    new WeightComparer());

            Console.WriteLine("Calculating the shortest route from {0} to {1}", placeA, placeB);

            IEnumerable<Place> routePlaces =
                routeFinder.CalculateRoute(placeA, placeB);

            Place previousPlace = null;
            float totalDist = 0;

            foreach (Place p in routePlaces)
            {
                Console.Write("Place: {0}          ", p.Name);

                if (previousPlace != null)
                {
                    var dist = routeFinder.DistanceBetween(p, previousPlace);
                    totalDist += dist;

                    Console.WriteLine("Distance: {0}   Cumulative Distance: {1}", dist, totalDist);
                }
                else
                {
                    Console.WriteLine();
                }

                previousPlace = p;
            }

            Console.WriteLine();
        }

        private static void CalculateQuickestRoute(
            IGraph<Place, PlaceLink> graph,
            Place placeA,
            Place placeB)
        {
            Contract.Requires<ArgumentNullException>(graph != null);
            Contract.Requires<ArgumentNullException>(graph.Edges != null);

            var routeFinder = new Dijkstra<Place, PlaceLink, TimeSpan>(graph,  n => n.TravelTime,
                    new WeightComparer());

            Console.WriteLine("Calculating the quickest route from {0} to {1}", placeA, placeB);

            IEnumerable<Place> routePlaces =
                routeFinder.CalculateRoute(placeA, placeB);

            Place previousPlace = null;
            TimeSpan totalTime = TimeSpan.Zero;

            foreach (Place p in routePlaces)
            {
                Console.Write("Place: {0}          ", p.Name);

                if (previousPlace != null)
                {
                    TimeSpan timeSpan = routeFinder.DistanceBetween(p, previousPlace);
                    totalTime += timeSpan;

                    Console.WriteLine("Time: {0}   Cumulative Time: {1}", timeSpan, totalTime);
                }
                else
                {
                    Console.WriteLine();
                }

                previousPlace = p;
            }

            Console.WriteLine();
        }
    }
}
