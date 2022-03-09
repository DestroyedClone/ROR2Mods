using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace WeFixYourMods.Properties
{
    /// <summary>
    /// A helper class for loading embedded resources into the game.
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Loads an embedded asset bundle
        /// </summary>
        /// <param name="resourceBytes">The bytes returned by Properties.Resources.ASSETNAME</param>
        /// <returns>The loaded bundle</returns>
        public static AssetBundle LoadAssetBundle(Byte[] resourceBytes)
        {
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));
            return AssetBundle.LoadFromMemory(resourceBytes);
        }

        /// <summary>
        /// A simple helper to generate a unique mod prefix for you.
        /// </summary>
        /// <param name="plugin">A reference to your plugin. (this.GetModPrefix)</param>
        /// <param name="bundleName">A unique name for the bundle (Unique within your mod)</param>
        /// <returns>The generated prefix</returns>
        public static String GetModPrefix(this BepInEx.BaseUnityPlugin plugin, String bundleName)
        {
            return String.Format("@{0}+{1}", plugin.Info.Metadata.Name, bundleName);
        }


        /// <summary>
        /// Loads an embedded .png or .jpg image as a Texture2D
        /// </summary>
        /// <param name="resourceBytes">The bytes returned by Properties.Resources.ASSETNAME</param>
        /// <returns>The loaded texture</returns>
        public static Texture2D LoadTexture2D(Byte[] resourceBytes)
        {
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));

            var tempTex = new Texture2D(128, 128, TextureFormat.RGBAFloat, false);
            tempTex.LoadImage(resourceBytes, false);

            return tempTex;
        }

        /// <summary>
        /// Loads an embedded managed assembly.
        /// </summary>
        /// <param name="resourceBytes">The bytes returned by Properties.Resources.ASSETNAME</param>
        /// <returns>The loaded managed assembly</returns>
        public static Assembly LoadAssembly(Byte[] resourceBytes)
        {
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));
            return Assembly.Load(resourceBytes);
        }

        /// <summary>
        /// Loads an embedded unmanaged assembly.
        /// </summary>
        /// <param name="resourceDllName">The name of the dll. Must end in .dll</param>
        /// <param name="resourceBytes">The bytes returned by Properties.Resources.ASSETNAME</param>
        /// <returns>The directory that the unmanaged assembly is created in for later deletion</returns>
        public static String LoadUnmanagedAssembly(String resourceDllName, Byte[] resourceBytes)
        {
            if (resourceBytes == null) throw new ArgumentNullException(nameof(resourceBytes));
            if (String.IsNullOrEmpty(resourceDllName)) throw new ArgumentException("Must not be null or empty", nameof(resourceDllName));
            if (!resourceDllName.EndsWith(".dll")) throw new ArgumentException("Must end in .dll", nameof(resourceDllName));

            var assemblyName = Assembly.GetExecutingAssembly()?.GetName();
            if (assemblyName == null) throw new Exception("GetExecutingAssembly returned null... wut?");

            var directory = String.Format("{0}.{1}", assemblyName.Name, assemblyName.Version);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var environmentPath = Environment.GetEnvironmentVariable("PATH");
            if (!environmentPath.Split(';').Contains(directory)) Environment.SetEnvironmentVariable("PATH", String.Format("{0};{1}", directory, environmentPath));

            var dllPath = Path.Combine(directory, resourceDllName);

            if (!File.Exists(dllPath) || !resourceBytes.SequenceEqual(File.ReadAllBytes(dllPath)))
            {
                File.WriteAllBytes(dllPath, resourceBytes);
            }
            return directory;
        }
    }
}