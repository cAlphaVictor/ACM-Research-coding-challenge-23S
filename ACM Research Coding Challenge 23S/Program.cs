//Programmed by Ciaran Austin Vaille (Netid: Cav200002) on 2/1/2023.
using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using System.Collections.Generic;

namespace ACM_Research_Coding_Challenge_23S
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.Remove(System.AppDomain.CurrentDomain.BaseDirectory.Length - 11), @"6 class csv.csv")))
            {
                using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    //Reads in the star data from the CSV file.
                    csvReader.Context.RegisterClassMap<StarDataClassMap>();
                    List<StarData> records = csvReader.GetRecords<StarData>().ToList();

                    //Compiles data by star type.
                    Dictionary<int, StarTypeData> starTypesData = null;
                    foreach(StarData starData in records)
                    {
                        if (starTypesData == null)
                            starTypesData = new Dictionary<int, StarTypeData>();

                        if (starTypesData.ContainsKey(starData.StarType))
                            starTypesData[starData.StarType].AddEntry(starData);
                        else
                        {
                            StarTypeData starTypeData = new StarTypeData(starData.StarType);
                            starTypeData.AddEntry(starData);
                            starTypesData.Add(starData.StarType, starTypeData);
                        }
                    }

                    //Prints out the information header.
                    Console.WriteLine("Star Type  |  Average Temperature | Average Luminosity | Average Radius | Average Absolute Magnitude | Average Star Color | Average Spectral Class");
                    //Loops through and prints out the average information for each star type.
                    foreach(int starType in starTypesData.Keys)
                        Console.WriteLine(starTypesData[starType].starType + " | " + starTypesData[starType].averageTemperature + " | " + starTypesData[starType].averageLuminosity + " |  " + starTypesData[starType].averageRadius + "  | " + starTypesData[starType].averageAbsoluteMagnitude + "  |  " + starTypesData[starType].averageStarColor + "  |  " + starTypesData[starType].averageSpectralClass);
                }
            }
            Console.WriteLine("\nPress Enter to Exit");
            Console.ReadLine();
        }
    }

    public class StarTypeData
    {
        //Variables.
        public int starType;

        private List<int> temperatures = null;
        private List<float> luminosities = null;
        private List<float> radii = null;
        private List<float> absoluteMagnitudes = null;
        private List<string> starColors = null;
        private List<char> spectralClasses = null;

        //Properties.
        /// <summary>
        /// Public property that should be accessed in order to obtain a computed average temperature for the star type.
        /// </summary>
        public int averageTemperature
        {
            get
            {
                if (temperatures == null || temperatures.Count == 0)
                    return 0;
                int temperatureSum = 0;
                foreach (int temperature in temperatures)
                    temperatureSum += temperature;
                return temperatureSum / temperatures.Count;
            }
        }
        /// <summary>
        /// Public property that should be accessed in order to obtain a computed average luminosity for the star type.
        /// </summary>
        public float averageLuminosity
        {
            get
            {
                if (luminosities == null || luminosities.Count == 0)
                    return 0;
                float luminositySum = 0;
                foreach (float luminosity in luminosities)
                    luminositySum += luminosity;
                return luminositySum / luminosities.Count;
            }
        }
        /// <summary>
        /// Public property that should be accessed in order to obtain a computed average radius for the star type.
        /// </summary>
        public float averageRadius
        {
            get
            {
                if (radii == null || radii.Count == 0)
                    return 0;
                float radiusSum = 0;
                foreach (float radius in radii)
                    radiusSum += radius;
                return radiusSum / radii.Count;
            }
        }
        /// <summary>
        /// Public property that should be accessed in order to obtain a computed average absolute magnitude for the star type.
        /// </summary>
        public float averageAbsoluteMagnitude
        {
            get
            {
                if (absoluteMagnitudes == null || absoluteMagnitudes.Count == 0)
                    return 0;
                float absoluteMagnitudeSum = 0;
                foreach (float absoluteMagnitude in absoluteMagnitudes)
                    absoluteMagnitudeSum += absoluteMagnitude;
                return absoluteMagnitudeSum / absoluteMagnitudes.Count;
            }
        }
        /// <summary>
        /// Public property that should be accessed in order to obtain a computed average star color for the star type. If there are multiple star colors that are equally common, a concatenation of the star colors seperated by a " / " will be returned.
        /// </summary>
        public string averageStarColor
        {
            get
            {
                if (starColors == null || starColors.Count == 0)
                    return null;
                Dictionary<string, int> starColorAmounts = new Dictionary<string, int>();
                foreach(string starColor in starColors)
                {
                    if (!starColorAmounts.ContainsKey(starColor.ToLower().Replace(" ", "").Replace("-", "")))
                        starColorAmounts.Add(starColor.ToLower().Replace(" ", "").Replace("-", ""), 1);
                    else
                        starColorAmounts[starColor.ToLower().Replace(" ", "").Replace("-", "")] += 1;
                }
                List<string> averageStarColors = new List<string>();
                foreach(string starColor in starColorAmounts.Keys)
                {
                    if(averageStarColors.Count == 0 || starColorAmounts[starColor] == starColorAmounts[averageStarColors[0]])
                        averageStarColors.Add(starColor);
                    else if (starColorAmounts[starColor] > starColorAmounts[averageStarColors[0]])
                    {
                        averageStarColors.Clear();
                        averageStarColors.Add(starColor);
                    }
                }
                string averageStarColor = averageStarColors[0];
                for (int averageStarColorIndex = 1; averageStarColorIndex < averageStarColors.Count; averageStarColorIndex++)
                    averageStarColor += " / " + averageStarColors[averageStarColorIndex];
                return averageStarColor;
            }
        }
        /// <summary>
        /// Public property that should be accessed in order to obtain a computed average spectral class for the star type. If there are multiple spectral classes that are equally common, a concatenation of the spectral classes seperated by a " / " will be returned.
        /// </summary>
        public string averageSpectralClass
        {
            get
            {
                if (spectralClasses == null || spectralClasses.Count == 0)
                    return null;
                Dictionary<char, int> spectralClassAmounts = new Dictionary<char, int>();
                foreach (char spectralClass in spectralClasses)
                {
                    if (!spectralClassAmounts.ContainsKey(spectralClass))
                        spectralClassAmounts.Add(spectralClass, 1);
                    else
                        spectralClassAmounts[spectralClass] += 1;
                }
                List<char> averageSpectralClasses = new List<char>();
                foreach (char spectralClass in spectralClassAmounts.Keys)
                {
                    if (averageSpectralClasses.Count == 0 || spectralClassAmounts[spectralClass] == spectralClassAmounts[averageSpectralClasses[0]])
                        averageSpectralClasses.Add(spectralClass);
                    else if (spectralClassAmounts[spectralClass] > spectralClassAmounts[averageSpectralClasses[0]])
                    {
                        averageSpectralClasses.Clear();
                        averageSpectralClasses.Add(spectralClass);
                    }
                }
                string averageSpectralClass = averageSpectralClasses[0].ToString();
                for (int averageSpectralClassIndex = 1; averageSpectralClassIndex < averageSpectralClasses.Count; averageSpectralClassIndex++)
                    averageSpectralClass += " / " + averageSpectralClasses[averageSpectralClassIndex];
                return averageSpectralClass;
            }
        }

        public StarTypeData(int starType)
        {
            this.starType = starType;
        }

        /// <summary>
        /// Public method that should be called in order to add a new entry of data in for the star type belonging to this object.
        /// </summary>
        /// <param name="starData"></param>
        public void AddEntry(StarData starData)
        {
            //Returns if the star data belongs to a star of another type.
            if (starData.StarType != starType)
                return;
            //Temperature.
            if (temperatures == null)
                temperatures = new List<int>();
            temperatures.Add(starData.Temperature);
            //Luminosity.
            if (luminosities == null)
                luminosities = new List<float>();
            luminosities.Add(starData.Luminosity);
            //Radius.
            if (radii == null)
                radii = new List<float>();
            radii.Add(starData.Radius);
            //Absolute Magnitude.
            if (absoluteMagnitudes == null)
                absoluteMagnitudes = new List<float>();
            absoluteMagnitudes.Add(starData.AbsoluteMagnitude);
            //Star Color.
            if (starColors == null)
                starColors = new List<string>();
            starColors.Add(starData.StarColor.ToLower());
            //Spectral Class.
            if (spectralClasses == null)
                spectralClasses = new List<char>();
            spectralClasses.Add(starData.SpectralClass);
        }

        /// <summary>
        /// Public method that should be called in order to remove an entry of data for the star type belonging to this object.
        /// </summary>
        /// <param name="starData"></param>
        public void RemoveEntry(StarData starData)
        {
            //Returns if the star data belongs to a star of another type.
            if (starData.StarType != starType)
                return;
            //Temperature.
            if (temperatures != null && temperatures.Contains(starData.Temperature))
                temperatures.Remove(starData.Temperature);
            //Luminosity.
            if (luminosities != null && luminosities.Contains(starData.Luminosity))
                luminosities.Remove(starData.Luminosity);
            //Radius.
            if (radii != null && radii.Contains(starData.Radius))
                radii.Remove(starData.Radius);
            //Absolute Magnitude.
            if (absoluteMagnitudes != null && absoluteMagnitudes.Contains(starData.AbsoluteMagnitude))
                absoluteMagnitudes.Remove(starData.AbsoluteMagnitude);
            //Star Color.
            if (starColors != null && starColors.Contains(starData.StarColor.ToLower()))
                starColors.Remove(starData.StarColor.ToLower());
            //Spectral Class.
            if (spectralClasses != null && spectralClasses.Contains(starData.SpectralClass))
                spectralClasses.Remove(starData.SpectralClass);
        }
    }

    public class StarDataClassMap : ClassMap<StarData>
    {
        public StarDataClassMap()
        {
            Map(m => m.Temperature).Name("Temperature (K)");
            Map(m => m.Luminosity).Name("Luminosity(L/Lo)");
            Map(m => m.Radius).Name("Radius(R/Ro)");
            Map(m => m.AbsoluteMagnitude).Name("Absolute magnitude(Mv)");
            Map(m => m.StarType).Name("Star type");
            Map(m => m.StarColor).Name("Star color");
            Map(m => m.SpectralClass).Name("Spectral Class");
        }
    }

    public class StarData
    {
        public int Temperature { get; set; }
        public float Luminosity { get; set; }
        public float Radius { get; set; }
        public float AbsoluteMagnitude { get; set; }
        public int StarType { get; set; }
        public string StarColor { get; set; }
        public char SpectralClass { get; set; }
    }
}
