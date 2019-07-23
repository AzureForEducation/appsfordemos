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
            //Connecting to AU tenant
            string tenantDomain = "your-tenant-domain";
            string restApiUrl = "your-custom-restapi-endpoint";
            string clientId = "your-client-id";
            string clientSecret = "your-client-secret";

            //Getting authenticated within the API
            MediaServices mediaService = new MediaServices(tenantDomain, restApiUrl, clientId, clientSecret);
            await mediaService.InitializeAccessTokenAsync();

            //Creating asset, access policy and locator
            string accessPolicyId = await mediaService.GenerateAccessPolicy("your-access-policy-name", 100, 2);
            Asset asset = await mediaService.GenerateAsset("your-asset-name", "your-storage-account-name");
            Locator locator = await mediaService.GenerateLocator(accessPolicyId, asset.Id, DateTime.Now.AddMinutes(-5), 1);

            //Generate a file stream for a video
            FileStream fileStream = new FileStream("path-to-your-video-file", FileMode.Open);
            StreamContent content = new StreamContent(fileStream);

            //Uploads the file to azure and generate the asset's file info
            await mediaService.UploadBlobToLocator(content, locator, "your-video-file-name");
            await mediaService.GenerateFileInfo(asset.Id);

            //Run an encoding job on the uploaded asset
            string mediaProcessorId = await mediaService.GetMediaProcessorId("Media Encoder Standard");
            var result = await mediaService.CreateJob("Job - Media Enconder", asset.Uri, mediaProcessorId, "H264 Multiple Bitrate 720p");

        }
    }
}
