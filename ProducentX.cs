using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProducentConsumerProblem
{

    public class ProducentX
    {
        private readonly BlockingCollection<object> _queue;
        private readonly CancellationToken _cancellationToken;
        private int tableChoice = 1;  // config

        ContinentDAO continent = new ContinentDAO();
        AllianceDAO alliance = new AllianceDAO();
        CountryDAO country = new CountryDAO();
        GetValueFromConfig getSafeValue = new GetValueFromConfig();



        int minPopulation = 0;  
        int maxPopulation = 0;
        int minArea = 0;
        int maxArea = 0;
        int minGdp = 0;
        int maxGdp  = 0;
        int operationIndex = 3;

        public ProducentX(BlockingCollection<object> queue, CancellationToken cancellationToken)
        {
            _queue = queue;
            _cancellationToken = cancellationToken;

            // Načítání hodnot z App.config
            minPopulation = getSafeValue.GetConfigValue("minPopulation", 50000);
            maxPopulation = getSafeValue.GetConfigValue("maxPopulation", 100000001);
            getSafeValue.ValidateRange(minPopulation, maxPopulation);

            minArea = getSafeValue.GetConfigValue("minArea", 1000);
            maxArea = getSafeValue.GetConfigValue("maxArea", 100000001);
            getSafeValue.ValidateRange(minArea, maxArea);

            minGdp = getSafeValue.GetConfigValue("minGdp", 2000);
            maxGdp = getSafeValue.GetConfigValue("maxGdp", 100001);
            getSafeValue.ValidateRange(minGdp, maxGdp);

        }

        public void StartProducing()
        {
            Random random = new Random();
            try
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    object data;
                    switch (tableChoice)
                    {
                        case 1:
                            int ContinentIdd = random.Next(1, continent.GetIdCount() + 1);

                            data = new
                            {
                                ContinentId = continent.GetRandomID(),
                                AllianceId = alliance.GetRandomID(),
                                Name = country.GetNameByContinent(ContinentIdd),   // choice1 real countries names from DB    DO NOT DELETE
                                //Name = GenerateRandomString(2),  //choice2 100 rand names  DO NOT DELETE
                                Population = random.Next(minPopulation, maxPopulation),
                                Area = random.Next(minArea, maxArea),
                                Gdp = random.Next(minGdp, maxGdp),
                                Operation = operationIndex
                            };
                            break;

                        case 2:
                            data = new
                            {
                                Name = GenerateRandomString(5) //100% random continent   DO NOT USE   DB ALREADY HAVE ALL CONTINENTS
                            };
                            break;

                        default:
                            data = new
                            {
                                Name = GenerateRandomString(5),  //100p rand alli  
                                Type = "Type" + random.Next(1, 4)
                            };
                            break;
                    }
                    /*
                    if (tableChoice == 1)
                    {
                        int ContinentIdd = random.Next(1, continent.GetIdCount() + 1);
                        data = new
                        {
                            ContinentId = ContinentIdd,
                            AllianceId = random.Next(1, alliance.GetIdCount() + 1),
                            Name = country.GetNameByContinent(ContinentIdd),   // choice1 real countries names from DB    DO NOT DELETE
                            //Name = GenerateRandomString(2),  //choice2 100 rand names  DO NOT DELETE
                            Population = random.Next(10, 501),
                            Area = (decimal)random.Next(1000, 500001),
                            Gdp = (decimal)random.Next(10, 2501),
                            Delete = false
                        };
                    }
                    else if (tableChoice == 2)
                    {
                        data = new
                        {
                            Name = GenerateRandomString(5)
                        };
                    }
                    else
                    {
                        data = new
                        {
                            Name = GenerateRandomString(5),
                            Type = "Type" + random.Next(1, 4)
                        };
                    }
                    */

                    if (!_queue.TryAdd(data))
                    {
                        Console.WriteLine("[Producent] Fronta je plná, producent čeká na uvolnění fronty.");
                        while (!_queue.TryAdd(data))
                        {
                            Thread.Sleep(100);
                        }
                    }
                    Console.WriteLine("[Producent] Přidal data do fronty: " + data);
                    Thread.Sleep(500);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[Producent] Ukončení producenta.");
            }
            /*catch (Exception ex)
            {
                Console.WriteLine($"[Producent] Chyba: {ex.Message}");
            }*/
            finally
            {
                _queue.CompleteAdding();
            }
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] buffer = new char[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = chars[random.Next(chars.Length)];
            }
            return new string(buffer);
        }
    }
}
