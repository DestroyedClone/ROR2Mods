using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

//[assembly: HG.Reflection.SearchableAttribute.OptIn]
namespace ArtiThiccer
{
    [R2APISubmoduleDependency(new string[]
    {
        "LoadoutAPI",
        "LanguageAPI"
    })]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.Fuck.ArtiThiccer", "ArtiThiccer", "1.0.1")]
    public class ArtiThiccerPlugin : BaseUnityPlugin
    {
        private static readonly List<Material> materialsWithRoRShader = new List<Material>();

        public static PluginInfo PInfo { get; private set; }

        private void Start()
        {
            PInfo = Info;
            Assets.Init();
            ArtiThiccerPlugin.AddLanguageTokens();
            On.RoR2.BodyCatalog.Init += BodyCatalog_Init;
            ArtiThiccerPlugin.ReplaceShaders();
        }

        private void BodyCatalog_Init(On.RoR2.BodyCatalog.orig_Init orig)
        {
            orig();
            AddSkins();
        }

        //[RoR2.SystemInitializer(dependencies: typeof(RoR2.BodyCatalog))]
        private static void AddSkins()
        {
            ArtiThiccerPlugin.AddMageBodyArtificerNoCapeSkin();
            ArtiThiccerPlugin.AddMageBodyArtificerFatSkin();
        }

        private static void ReplaceShaders()
        {
            ArtiThiccerPlugin.materialsWithRoRShader.Add(ArtiThiccerPlugin.LoadMaterialWithReplacedShader("Assets/Resources/Arti/MatArti.mat", "Hopoo Games/Deferred/Standard"));
        }

        private static Material LoadMaterialWithReplacedShader(string materialPath, string shaderName)
        {
            Material material = Assets.mainBundle.LoadAsset<Material>(materialPath);
            material.shader = Shader.Find(shaderName);
            return material;
        }

        private static void AddLanguageTokens()
        {
            LanguageAPI.Add("FUCK_SKIN_ARTIFICERNOCAPE_NAME", "ArtificerNoCape");
            LanguageAPI.Add("FUCK_SKIN_ARTIFICERNOCAPE_NAME", "ArtificerNoCape", "en");
            LanguageAPI.Add("FUCK_SKIN_ARTIFICERFAT_NAME", "Canon");
            LanguageAPI.Add("FUCK_SKIN_ARTIFICERFAT_NAME", "Canon", "en");
        }

        private static void AddMageBodyArtificerNoCapeSkin()
        {
            string text = "MageBody";
            string text2 = "ArtificerNoCape";
            try
            {
                GameObject gameObject = BodyCatalog.FindBodyPrefab(text);
                Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>(true);
                ModelSkinController componentInChildren = gameObject.GetComponentInChildren<ModelSkinController>();
                GameObject gameObject2 = componentInChildren.gameObject;
                LoadoutAPI.SkinDefInfo skinDefInfo = default(LoadoutAPI.SkinDefInfo);
                skinDefInfo.Icon = Assets.mainBundle.LoadAsset<Sprite>("Assets\\SkinMods\\ArtiThiccer\\Icons\\ArtificerNoCapeIcon.png");
                skinDefInfo.Name = text2;
                skinDefInfo.NameToken = "FUCK_SKIN_ARTIFICERNOCAPE_NAME";
                skinDefInfo.RootObject = gameObject2;
                skinDefInfo.BaseSkins = Array.Empty<SkinDef>();
                skinDefInfo.GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>();
                CharacterModel.RendererInfo[] array = new CharacterModel.RendererInfo[2];
                int num = 0;
                CharacterModel.RendererInfo rendererInfo = default(CharacterModel.RendererInfo);
                rendererInfo.defaultMaterial = Assets.mainBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
                rendererInfo.defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                rendererInfo.ignoreOverlays = false;
                rendererInfo.renderer = componentsInChildren[7];
                array[num] = rendererInfo;
                int num2 = 1;
                rendererInfo = default(CharacterModel.RendererInfo);
                rendererInfo.defaultMaterial = Assets.mainBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
                rendererInfo.defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                rendererInfo.ignoreOverlays = false;
                rendererInfo.renderer = componentsInChildren[6];
                array[num2] = rendererInfo;
                skinDefInfo.RendererInfos = array;
                SkinDef.MeshReplacement[] array2 = new SkinDef.MeshReplacement[2];
                int num3 = 0;
                SkinDef.MeshReplacement meshReplacement = default(SkinDef.MeshReplacement);
                meshReplacement.mesh = Assets.mainBundle.LoadAsset<Mesh>("Assets\\SkinMods\\ArtiThiccer\\Meshes\\ThickMageMesh.mesh");
                meshReplacement.renderer = componentsInChildren[7];
                array2[num3] = meshReplacement;
                int num4 = 1;
                meshReplacement = default(SkinDef.MeshReplacement);
                meshReplacement.mesh = null;
                meshReplacement.renderer = componentsInChildren[6];
                array2[num4] = meshReplacement;
                skinDefInfo.MeshReplacements = array2;
                skinDefInfo.MinionSkinReplacements = Array.Empty<SkinDef.MinionSkinReplacement>();
                skinDefInfo.ProjectileGhostReplacements = Array.Empty<SkinDef.ProjectileGhostReplacement>();
                LoadoutAPI.SkinDefInfo skinDefInfo2 = skinDefInfo;
                Array.Resize<SkinDef>(ref componentInChildren.skins, componentInChildren.skins.Length + 1);
                componentInChildren.skins[componentInChildren.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skinDefInfo2);
                Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins")[(int)BodyCatalog.FindBodyIndex(gameObject)] = componentInChildren.skins;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(string.Concat(new string[]
                {
                    "Failed to add \"",
                    text2,
                    "\" skin to \"",
                    text,
                    "\""
                }));
                Debug.LogError(ex);
            }
        }

        private static void AddMageBodyArtificerFatSkin()
        {
            string text = "MageBody";
            string text2 = "Canon";
            try
            {
                GameObject gameObject = BodyCatalog.FindBodyPrefab(text);
                Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>(true);
                ModelSkinController componentInChildren = gameObject.GetComponentInChildren<ModelSkinController>();
                GameObject gameObject2 = componentInChildren.gameObject;
                LoadoutAPI.SkinDefInfo skinDefInfo = default(LoadoutAPI.SkinDefInfo);
                skinDefInfo.Icon = Assets.mainBundle.LoadAsset<Sprite>("Assets\\SkinMods\\ArtiThiccer\\Icons\\ArtificerFatIcon.png");
                skinDefInfo.Name = text2;
                skinDefInfo.NameToken = "FUCK_SKIN_ARTIFICERFAT_NAME";
                skinDefInfo.RootObject = gameObject2;
                skinDefInfo.BaseSkins = Array.Empty<SkinDef>();
                skinDefInfo.GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>();
                CharacterModel.RendererInfo[] array = new CharacterModel.RendererInfo[2];
                int num = 0;
                CharacterModel.RendererInfo rendererInfo = default(CharacterModel.RendererInfo);
                rendererInfo.defaultMaterial = Assets.mainBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
                rendererInfo.defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                rendererInfo.ignoreOverlays = false;
                rendererInfo.renderer = componentsInChildren[7];
                array[num] = rendererInfo;
                int num2 = 1;
                rendererInfo = default(CharacterModel.RendererInfo);
                rendererInfo.defaultMaterial = Assets.mainBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
                rendererInfo.defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                rendererInfo.ignoreOverlays = false;
                rendererInfo.renderer = componentsInChildren[6];
                array[num2] = rendererInfo;
                skinDefInfo.RendererInfos = array;
                SkinDef.MeshReplacement[] array2 = new SkinDef.MeshReplacement[2];
                int num3 = 0;
                SkinDef.MeshReplacement meshReplacement = default(SkinDef.MeshReplacement);
                meshReplacement.mesh = Assets.mainBundle.LoadAsset<Mesh>("Assets\\SkinMods\\ArtiThiccer\\Meshes\\MageMesh.mesh");
                meshReplacement.renderer = componentsInChildren[7];
                array2[num3] = meshReplacement;
                int num4 = 1;
                meshReplacement = default(SkinDef.MeshReplacement);
                meshReplacement.mesh = null;
                meshReplacement.renderer = componentsInChildren[6];
                array2[num4] = meshReplacement;
                skinDefInfo.MeshReplacements = array2;
                skinDefInfo.MinionSkinReplacements = Array.Empty<SkinDef.MinionSkinReplacement>();
                skinDefInfo.ProjectileGhostReplacements = Array.Empty<SkinDef.ProjectileGhostReplacement>();
                LoadoutAPI.SkinDefInfo skinDefInfo2 = skinDefInfo;
                Array.Resize<SkinDef>(ref componentInChildren.skins, componentInChildren.skins.Length + 1);
                componentInChildren.skins[componentInChildren.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skinDefInfo2);
                Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins")[(int)BodyCatalog.FindBodyIndex(gameObject)] = componentInChildren.skins;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(string.Concat(new string[]
                {
                    "Failed to add \"",
                    text2,
                    "\" skin to \"",
                    text,
                    "\""
                }));
                Debug.LogError(ex);
            }
        }
    }

    public static class Assets
    {
        //The mod's AssetBundle
        public static AssetBundle mainBundle;

        //A constant of the AssetBundle's name.
        public const string bundleName = "ArtiThiccer.fuckartithiccer";

        // Not necesary, but useful if you want to store the bundle on its own folder.
        // public const string assetBundleFolder = "AssetBundles";

        //The direct path to your AssetBundle
        public static string AssetBundlePath
        {
            get
            {
                var indexOfLastSlash = ArtiThiccerPlugin.PInfo.Location.LastIndexOf('\\');
                //Debug.Log(System.IO.Path.Combine(ArtiThiccerPlugin.PInfo.Location.Substring(0, ArtiThiccerPlugin.PInfo.Location.LastIndexOf('\\')), bundleName));
                //146 146 -1 160

                //This returns the path to your assetbundle assuming said bundle is on the same folder as your DLL. If you have your bundle in a folder, you can uncomment the statement below this one.
                return System.IO.Path.Combine(ArtiThiccerPlugin.PInfo.Location.Substring(0, indexOfLastSlash), bundleName);
                //return Path.Combine(MainClass.PInfo.Location, assetBundleFolder, myBundle);
            }
        }

        public static void Init()
        {
            //Loads the assetBundle from the Path, and stores it in the static field.
            mainBundle = AssetBundle.LoadFromFile(AssetBundlePath);
        }
    }
}