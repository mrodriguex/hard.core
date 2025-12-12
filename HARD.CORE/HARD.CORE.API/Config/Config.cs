
// using HARD.CORE.API.Helpers;
// using Microsoft.Extensions.Configuration;
// using System;
// using System.IO;

// namespace HARD.CORE.API.Configuration
// {
//     public static class Config
//     {
//         //private static IWebHostEnvironment _webHostEnvironment;
//         private static IConfiguration _configuration;
//         private static string _contentRootPath;

//         public static bool IsLinux
//         {
//             get
//             {
//                 int p = (int)Environment.OSVersion.Platform;
//                 return (p == 4) || (p == 6) || (p == 128);
//             }
//         }
//         private static string ContentRootPath
//         {
//             get
//             {
//                 if (string.IsNullOrEmpty(_contentRootPath))
//                 {
//                     _contentRootPath = "";
//                 }
//                 return (_contentRootPath);
//             }
//             set
//             {
//                 _contentRootPath = value;
//                 Configuration = ConfigurationHelper.ResolveConfiguration(_contentRootPath);
//             }
//         }

//         public static IConfiguration Configuration
//         {
//             get
//             {
//                 if (_configuration is null)
//                 {
//                     throw new Exception("La propiedad Configuration no ha sido inicializada en la clase estática Config.cs");
//                 }
//                 return (_configuration);
//             }
//             private set
//             {
//                 _configuration = value;
//             }
//         }

//         public static string ReportDesign
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportDesign").Value;
//             }
//         }

//         public static string ReportPublish
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportPublish").Value;
//             }
//         }

//         public static string ReportHistory
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportHistory").Value;
//             }
//         }

//         public static string ReportThumbsSource
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportThumbsSource").Value;
//             }
//         }

//         public static string ReportTarget
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportTarget").Value;
//             }
//         }

//         public static string ReportTargetBooked
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportTargetBooked").Value;
//             }
//         }

//         public static string ReportTargetSingle
//         {
//             get
//             {
//                 return Configuration.GetSection("ReportTargetSingle").Value;
//             }
//         }

//         public static void Configure(string contentRootPath)
//         {
//             ContentRootPath = contentRootPath;
//         }

//         public static string URL(string connectionString)
//         {
//             ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
//             string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
//             configurationBuilder.AddJsonFile(path, false);

//             var root = configurationBuilder.Build();
//             return root.GetSection("URLs").GetSection(connectionString).Value;
//         }

//         public static string ClaveUsuarios(string claveUsuarioTipo)
//         {
//             ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
//             string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
//             configurationBuilder.AddJsonFile(path, false);

//             var root = configurationBuilder.Build();
//             return root.GetSection("ClaveUsuarios").GetSection(claveUsuarioTipo).Value;
//         }

//         public static string ConnectionStrings(string connectionString)
//         {
//             ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
//             string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
//             configurationBuilder.AddJsonFile(path, false);

//             var root = configurationBuilder.Build();
//             return root.GetSection("ConnectionStrings").GetSection(connectionString).Value;
//         }

//     }

// }

