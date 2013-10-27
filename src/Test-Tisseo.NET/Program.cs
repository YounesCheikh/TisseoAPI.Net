using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TisseoAPI.Net.Core;
using TisseoAPI.Net.Objects;

namespace Test_Tisseo.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            /******************************************************/
            /********************** Places ************************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO PLACES */
            //TisseoPlaces tisseoPlaces = new TisseoPlaces() { Term = "Tissié", DisplayBestPlace = true };
            //PlacesData tisseoPlacesData = tisseoPlaces.GetPlacesData();

            /******************************************************/
            /*********************** Lines ************************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO LINES */
            TisseoLines tisseoLines = new TisseoLines() { DisplayTerminus = true };
            LinesData tisseoLinesData = tisseoLines.GetLinesData();
            Line ligneMetroA = tisseoLinesData.Lines.Where(l => l.ShortName.Equals("A")).FirstOrDefault();

            /******************************************************/
            /********************* STOP Areas *********************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO STOP_AREAS */
            //TisseoStopAreas tisseoStopAreas = new TisseoStopAreas() { DisplayLines = true, DisplayCoordXY = true };
            //StopAreasData stopAreasData = tisseoStopAreas.GetStopAreasData();

            /******************************************************/
            /********************** STOP Points *******************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO STOP_POINTS */
            //TesterTisseoArrets(ligneMetroA);

            /******************************************************/
            /****************** Departures Board ******************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO DEPATURE BOARD */
            //TesterTisseoProchainDepart();

            /******************************************************/
            /********************** MESSAGES **********************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO DEPATURE BOARD */
            //TesterTisseoMessages();


            /******************************************************/
            /****************** Lines Disrupted *******************/
            /******************************************************/
            /* UNCOMMENT THE FOLLOWING LINES TO TEST TISSEO LINES DISRUPTED */
            //TesterLesMessagesDePannes();
        }

        private static void TesterTisseoArrets(Line ligneMetroA)
        {
            TisseoStopPoints tisseoStopPoints = new TisseoStopPoints()
            {
                DisplayCoordXY = true,
                DisplayDestinations = true,
                DisplayLines = true,
                SortByDistance = true,
                LineId = ligneMetroA.Id.Value
            };
            StopPointsData stopPointsData = tisseoStopPoints.GetStopPontsData();
            foreach (var stpPnt in stopPointsData.StopPoints)
            {
                Console.WriteLine("Nom: {0}\nDestinations:", stpPnt.Name);
                foreach (var des in stpPnt.Destinations)
                {
                    Console.WriteLine("\t# {0}", des.Name);
                }
                Console.WriteLine("\n\nTaper la touch Q pour quitter ou une autre touche pour afficher le message suivant.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.Clear();
            }
        }

        private static void TesterTisseoProchainDepart()
        {
            // Exemple:
            // L'arret Tissie: OperatorCode = 941, stopPointId=1970324837184875
            TisseoDepartureBoard tisseoDepartureBoard = new TisseoDepartureBoard(941, 1970324837184875, "None") { DisplayRealTime = true };
            DepartureBoardData departureBoardData = tisseoDepartureBoard.GetDepartureBoardData();
            foreach (var dep in departureBoardData.Departures)
            {
                Console.WriteLine("Time: {0}\nTemps Réel: {1}\n\nDestinations: ",
                    dep.DateTime.ToString("dd/MM/yyyy hh:mm"),
                    dep.RealTime ? "OUI" : "NON");
                foreach (var des in dep.Destinations)
                {
                    Console.WriteLine(des.Name + " | " + des.CityName);
                }
                Console.WriteLine("Taper la touch Q pour quitter ou une autre touche pour afficher le message suivant.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.Clear();
            }
        }

        private static void TesterLesMessagesDePannes()
        {
            TisseoLinesDisrupted tisseoLinesDisrupted = new TisseoLinesDisrupted() { ContentFormat = "Text" };
            LinesDisruptedData linesDisruptedData = tisseoLinesDisrupted.GetMessagesData();
            // Afficher les informations des pannes pour la ligne A
            foreach (var ligne in linesDisruptedData.Lines)
            {
                Console.WriteLine("La ligne: {0}\nLe Message: {1}",
                    ligne.ShortName,
                    ligne.DisturbMessage.Content);
                var key = Console.ReadKey();
                Console.WriteLine("Taper la touch Q pour quitter ou une autre touche pour afficher le message suivant.");
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.Clear();
            }
        }

        private static void TesterTisseoMessages()
        {
            TisseoMessages tisseoMessages = new TisseoMessages() { ContentFormat = "Text", DisplayImportantOnly = false };
            MessagesData messagesData = tisseoMessages.GetMessagesData();
            Console.WriteLine("Les messages reçus: {0} messages\nLa date d'expiration de ces messages: {1}",
                messagesData.Messages.Count,
                messagesData.ExpirationDate.ToString("dd/MM/yyyy hh:mm")
                );
            Console.WriteLine("Taper la touch Q pour quitter");
            foreach (Message message in messagesData.Messages)
            {
                Console.WriteLine("N° {0} --------------\nTitre: {1}\nMessage:{2}\nLes lignes concernées:\n",
                    messagesData.Messages.IndexOf(message) + 1, message.Title, message.Content);
                foreach (Line line in message.Lines)
                    Console.Write("{0} {1} ", line.ShortName, line.Equals(message.Lines.Last()) ? "" : " - ");
                
                Console.WriteLine("Taper la touch Q pour quitter ou une autre touche pour afficher le message suivant.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.Clear();
            }
            Console.WriteLine("Taper n'importe quelle touche pour quitter.");
            Console.ReadKey();
        }
    }
}
