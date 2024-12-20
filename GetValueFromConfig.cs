using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducentConsumerProblem
{
    internal class GetValueFromConfig
    {




        /// <summary>
        /// Načte hodnotu z App.config a převede ji na celé číslo.
        /// </summary>
        /// <param name="key">Název klíče z App.config</param>
        /// <param name="defaultValue">Výchozí hodnota, která se použije, pokud je hodnota neplatná</param>
        /// <returns>Načtená hodnota</returns>
        public int GetConfigValue(string key, int defaultValue)
        {


            try
            {
                string value = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"Chybí hodnota pro klíč {key}, používám výchozí hodnotu {defaultValue}.");
                    return defaultValue;
                }

                int parsedValue = int.Parse(value); // Ověření, že jde o číslo
                if (parsedValue < 0)
                {
                    throw new ArgumentOutOfRangeException($"Hodnota pro {key} musí být kladná. Byla zadána: {parsedValue}");
                }

                return parsedValue;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Chyba: Hodnota pro klíč {key} není číslo. Používám výchozí hodnotu {defaultValue}.");
                return defaultValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekávaná chyba při čtení hodnoty pro {key}: {ex.Message}");
                return defaultValue;
            }
            
        }

        /// <summary>
        /// Ověří, že minimální hodnota je menší než maximální hodnota.
        /// </summary>
        /// <param name="min">Minimální hodnota</param>
        /// <param name="max">Maximální hodnota</param>
        public void ValidateRange(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentException($"Minimální hodnota ({min}) musí být menší než maximální hodnota ({max}).");
            }
        }

        /// <summary>
        /// Bezpečně získá hodnotu z funkce. Pokud dojde k výjimce, vrátí výchozí hodnotu.
        /// </summary>
        /// <typeparam name="T">Typ vrácené hodnoty</typeparam>
        /// <param name="valueFunc">Funkce, která generuje hodnotu</param>
        /// <param name="defaultValue">Výchozí hodnota, která se použije v případě chyby</param>
        /// <returns>Vygenerovaná hodnota nebo výchozí hodnota při chybě</returns>
        public T GetSafeRandomValue<T>(Func<T> valueFunc, T defaultValue)
        {
            try
            {
                return valueFunc();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při generování hodnoty: {ex.Message}");
                return defaultValue;
            }
        }



    }
}
