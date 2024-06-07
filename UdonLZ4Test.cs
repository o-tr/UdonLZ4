using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace jp.ootr.UdonLZ4
{
    public class UdonLZ4Test : UdonSharpBehaviour
    {
        public VRCUrl url;
        public UdonLZ4 lz4;
        
        private void Start()
        {
            VRCStringDownloader.LoadUrl(url, (IUdonEventReceiver)this);
        }
        
        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            Debug.Log($"ZipLoader: text-zip loaded successfully from {result.Url}.");
            byte[] data = Convert.FromBase64String(result.Result);
            Debug.Log($"decompress: {lz4.Decompress(data).Length}");
        }
    }
}