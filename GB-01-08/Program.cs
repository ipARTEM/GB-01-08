using System;
using System.Configuration;

namespace GB_01_08
{
    class Program
    {

        static void Main(string[] args)
        {
            NewSettings();
            
        }

        static void NewSettings()
        {
            ReadAllSettings();

            Console.WriteLine("Имя: ");
            AddUpdateAppSettings("Имя: ", Console.ReadLine());
            Console.WriteLine("Возраст: ");
            AddUpdateAppSettings("Возраст: ", Console.ReadLine());
            Console.WriteLine("Род деятельности: ");
            AddUpdateAppSettings("Род деятельности: ", Console.ReadLine());


            ReadSetting("Имя: ");
            ReadSetting("Возраст: ");
            ReadSetting("Род деятельности: ");
        }


        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                Console.WriteLine("Здравствуйте, уважаемый пользователь! ");

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("Залолните, пожалуйста, карточку пользователя: ");
                }
                else
                {
                    appSettings.GetValues(0);

                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine($"{key} {appSettings[key]}");
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}