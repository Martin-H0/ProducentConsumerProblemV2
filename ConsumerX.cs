using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducentConsumerProblem
{
    public class ConsumerX
    {
        private readonly BlockingCollection<object> _queue;
        private readonly CancellationToken _cancellationToken;

        AllianceHandler allianceHandler = new AllianceHandler();
        CountryHandler countryHandler = new CountryHandler();
        ContinentHandler continentHandler = new ContinentHandler();
        CountryDAO countryDAO = new CountryDAO();

        public ConsumerX(BlockingCollection<object> queue, CancellationToken cancellationToken)
        {
            _queue = queue;
            _cancellationToken = cancellationToken;
        }

        public void StartConsuming()
        {
            try
            {
                while (true)
                {
                    foreach (var data in _queue.GetConsumingEnumerable(_cancellationToken))
                    {
                        var dataType = data.GetType();
                        if (dataType.GetProperty("ContinentId") != null)
                        {
                            int continentId = (int)dataType.GetProperty("ContinentId").GetValue(data);
                            int allianceId = (int)dataType.GetProperty("AllianceId").GetValue(data);
                            string name = (string)dataType.GetProperty("Name").GetValue(data);
                            //int nameId = countryDAO.GetId(name);
                            int population = (int)dataType.GetProperty("Population").GetValue(data);
                            int area = (int)dataType.GetProperty("Area").GetValue(data);
                            int gdp = (int)dataType.GetProperty("Gdp").GetValue(data);
                            int operation = (int)dataType.GetProperty("Operation").GetValue(data);
                            switch (operation)
                            {
                                case 1:
                                    {
                                        countryHandler.DeleteCountryData(continentId, name);
                                    };
                                    break;

                                case 2:
                                    {
                                        countryHandler.SaveCountryData(continentId, allianceId, name, population, area, gdp);

                                    };
                                    break;

                                default:
                                    {
                                        countryHandler.SaveCountryData(continentId, allianceId, name, population, area, gdp);

                                    };
                                    break;
                            }

                            Console.WriteLine($"[Konzument] Zpracovává Country: ContinentId={continentId}, AllianceId={allianceId}, Name={name}, Population={population}, Area={area}, GDP={gdp}, Delete={operation}");
                        }
                        else if (dataType.GetProperty("Name") != null && !dataType.GetProperties().Any(p => p.Name == "Type"))
                        {
                            string name = (string)dataType.GetProperty("Name").GetValue(data);
                            Console.WriteLine($"[Konzument] Zpracovává Continent: Name={name}");
                        }
                        else if (dataType.GetProperty("Name") != null && dataType.GetProperties().Any(p => p.Name == "Type"))
                        {
                            string name = (string)dataType.GetProperty("Name").GetValue(data);
                            string type = (string)dataType.GetProperty("Type").GetValue(data);
                            Console.WriteLine($"[Konzument] Zpracovává Alliance: Name={name}, Type={type}");
                        }
                        Thread.Sleep(1000);
                    }
					Thread.Sleep(100);
				}
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[Konzument] Ukončení konzumenta.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Konzument] Chyba: {ex.Message}");
            }
            
        }
    }
}
