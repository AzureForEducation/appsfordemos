using System;
using System.IO;
using System.Net.Http;
using au_mediaservices.entities;
using au_mediaservices.services;

namespace au_mediaservices
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //Constants related to AU's tenant
            string tenantDomain = "";
            string restApiUrl = "";
            string clientId = "";
            string clientSecret = "";
            MediaServices mediaService = new MediaServices(tenantDomain, restApiUrl, clientId, clientSecret);

            //Reading data from users
            Console.WriteLine("\n");
            Console.WriteLine("Please, type Access Policy's name: ");
            string accesPoliceName = Console.ReadLine();

            Console.WriteLine("\n------------------------------------\n");
            Console.WriteLine("Please, type Asset's name: ");
            string assetName = Console.ReadLine();

            Console.WriteLine("\n------------------------------------\n");
            Console.WriteLine("Please, type Primary Storage Account's name: ");
            string storageAccountName = Console.ReadLine();

            Console.WriteLine("\n------------------------------------\n");
            Console.WriteLine("Please, type video's file path followed by file's name and extension: ");
            string videoRelativePath = Console.ReadLine();

            Console.WriteLine("\n------------------------------------\n");
            Console.WriteLine("Please, type video's file name: ");
            string videoFileName = Console.ReadLine();

            Asset asset = new Asset();
            Locator locator = new Locator();

            if(string.IsNullOrEmpty(accesPoliceName) || string.IsNullOrEmpty(assetName) || string.IsNullOrEmpty(storageAccountName) || 
               string.IsNullOrEmpty(videoRelativePath) || string.IsNullOrEmpty(videoFileName))
               {
                   Console.WriteLine("Incorrect data format.");
                   Console.WriteLine("Application will close.");
                   Environment.Exit(-1);                   
               }
               else
               {
                    Console.WriteLine("\n------------------------------------\n");
                    Console.WriteLine("Processing...\n");
                    
                    Console.WriteLine("Getting service principal authenticated. Please wait...");
                    try
                    {
                        await mediaService.InitializeAccessTokenAsync();
                        Console.WriteLine("Done.\n");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Critial Error: Application wasn't able to login within Azure Media Services. \n" + ex.Message);
                        Console.WriteLine("Application will close.");
                        Environment.Exit(-1);
                    }

                    Console.WriteLine("Creating: Access Policy, Asset and Locator. Please, wait...");
                    try
                    {
                        string accessPolicyId = await mediaService.GenerateAccessPolicy(accesPoliceName, 100, 2);
                        asset = await mediaService.GenerateAsset(assetName, storageAccountName);
                        locator = await mediaService.GenerateLocator(accessPolicyId, asset.Id, DateTime.Now.AddMinutes(-5), 1);
                        Console.WriteLine("Done.\n");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Critial Error: Application wasn't able to create the resources. \n" + ex.Message);
                        Console.WriteLine("Application will close.");
                        Environment.Exit(-1);
                    }

                    Console.WriteLine("Uploading video file. Please, wait...");
                    try
                    {
                        FileStream fileStream = new FileStream(videoRelativePath, FileMode.Open);
                        StreamContent content = new StreamContent(fileStream);

                        await mediaService.UploadBlobToLocator(content, locator, videoFileName);
                        await mediaService.GenerateFileInfo(asset.Id);

                        Console.WriteLine("Done.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Critial Error: Application wasn't able to upload the video. \n" + ex.Message);
                        Console.WriteLine("Application will close.");
                        Environment.Exit(-1);
                    }

                    Console.WriteLine("Encoding asset " + assetName + ". Please, wait...");
                    try
                    {
                        string mediaProcessorId = await mediaService.GetMediaProcessorId("Media Encoder Standard");
                        var result = await mediaService.CreateJob("Job - Media Enconder", asset.Uri, mediaProcessorId, "H264 Multiple Bitrate 720p");   
                        Console.WriteLine("Done.\n");
                        Console.WriteLine("To watch your video, please copy belows URL into you prefered browser: \n");
                        Console.WriteLine(locator.BaseUri.ToString());
                        Console.WriteLine("Thanks for using the system. See you next time. Press Enter to close.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Critial Error: Encode's process has failed. \n" + ex.Message);
                        Console.WriteLine("Application will close.");
                        Environment.Exit(-1);
                    }
                }
        }
    }
}