using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace jp.ootr.UdonLZ4
{
    public class UdonLZ4AsyncTest : UdonSharpBehaviour
    {
        public VRCUrl url;
        public UdonLZ4 lz4;
        
        private void Start()
        {
            SendCustomEventDelayedSeconds(nameof(Load), 1f);
        }

        public void Load()
        {
            VRCStringDownloader.LoadUrl(url, (IUdonEventReceiver)this);
        }
        
        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            Debug.Log($"ZipLoader: text-zip loaded successfully from {result.Url}.");
            byte[] data = Convert.FromBase64String(result.Result);
            lz4.DecompressAsync((ILZ4CallbackReceiver)this, data);
        }
        
        public void OnLZ4Decompress(byte[] data)
        {
            Debug.Log($"UdonLZ4: decompressed data: {data.Length} bytes.");
        }
        
        public void OnLZ4DecompressError(DecompressError error)
        {
            Debug.LogError($"UdonLZ4: decompression error: {error}.");
        }
    }
}
