using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using On.RoR2;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;

namespace ArtiThiccer
{
	[R2APISubmoduleDependency(new string[]
	{
		"LoadoutAPI",
		"LanguageAPI"
	})]
	[NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.EveryoneNeedSameModVersion)]
	[BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.SoftDependency)]
	[BepInPlugin("com.Fuck.ArtiThiccer", "ArtiThiccer", "1.0.0")]
	public class ArtiThiccerPlugin : BaseUnityPlugin
	{
		private void Awake()
		{
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ArtiThiccer.fuckartithiccer"))
			{
				ArtiThiccerPlugin.assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);
			}
			BodyCatalog.Init += new BodyCatalog.hook_Init(this.BodyCatalog_Init);
			ArtiThiccerPlugin.ReplaceShaders();
			ArtiThiccerPlugin.AddLanguageTokens();
		}

		private static void ReplaceShaders()
		{
			ArtiThiccerPlugin.materialsWithRoRShader.Add(ArtiThiccerPlugin.LoadMaterialWithReplacedShader("Assets/Resources/Arti/MatArti.mat", "Hopoo Games/Deferred/Standard"));
		}

		private static Material LoadMaterialWithReplacedShader(string materialPath, string shaderName)
		{
			Material material = ArtiThiccerPlugin.assetBundle.LoadAsset<Material>(materialPath);
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
				skinDefInfo.Icon = ArtiThiccerPlugin.assetBundle.LoadAsset<Sprite>("Assets\\SkinMods\\ArtiThiccer\\Icons\\ArtificerNoCapeIcon.png");
				skinDefInfo.Name = text2;
				skinDefInfo.NameToken = "FUCK_SKIN_ARTIFICERNOCAPE_NAME";
				skinDefInfo.RootObject = gameObject2;
				skinDefInfo.BaseSkins = Array.Empty<SkinDef>();
				skinDefInfo.GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>();
				CharacterModel.RendererInfo[] array = new CharacterModel.RendererInfo[2];
				int num = 0;
				CharacterModel.RendererInfo rendererInfo = default(CharacterModel.RendererInfo);
				rendererInfo.defaultMaterial = ArtiThiccerPlugin.assetBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
				rendererInfo.defaultShadowCastingMode = 1;
				rendererInfo.ignoreOverlays = false;
				rendererInfo.renderer = componentsInChildren[7];
				array[num] = rendererInfo;
				int num2 = 1;
				rendererInfo = default(CharacterModel.RendererInfo);
				rendererInfo.defaultMaterial = ArtiThiccerPlugin.assetBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
				rendererInfo.defaultShadowCastingMode = 1;
				rendererInfo.ignoreOverlays = false;
				rendererInfo.renderer = componentsInChildren[6];
				array[num2] = rendererInfo;
				skinDefInfo.RendererInfos = array;
				SkinDef.MeshReplacement[] array2 = new SkinDef.MeshReplacement[2];
				int num3 = 0;
				SkinDef.MeshReplacement meshReplacement = default(SkinDef.MeshReplacement);
				meshReplacement.mesh = ArtiThiccerPlugin.assetBundle.LoadAsset<Mesh>("Assets\\SkinMods\\ArtiThiccer\\Meshes\\ThickMageMesh.mesh");
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
				Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins")[BodyCatalog.FindBodyIndex(gameObject)] = componentInChildren.skins;
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
				skinDefInfo.Icon = ArtiThiccerPlugin.assetBundle.LoadAsset<Sprite>("Assets\\SkinMods\\ArtiThiccer\\Icons\\ArtificerFatIcon.png");
				skinDefInfo.Name = text2;
				skinDefInfo.NameToken = "FUCK_SKIN_ARTIFICERFAT_NAME";
				skinDefInfo.RootObject = gameObject2;
				skinDefInfo.BaseSkins = Array.Empty<SkinDef>();
				skinDefInfo.GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>();
				CharacterModel.RendererInfo[] array = new CharacterModel.RendererInfo[2];
				int num = 0;
				CharacterModel.RendererInfo rendererInfo = default(CharacterModel.RendererInfo);
				rendererInfo.defaultMaterial = ArtiThiccerPlugin.assetBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
				rendererInfo.defaultShadowCastingMode = 1;
				rendererInfo.ignoreOverlays = false;
				rendererInfo.renderer = componentsInChildren[7];
				array[num] = rendererInfo;
				int num2 = 1;
				rendererInfo = default(CharacterModel.RendererInfo);
				rendererInfo.defaultMaterial = ArtiThiccerPlugin.assetBundle.LoadAsset<Material>("Assets/Resources/Arti/MatArti.mat");
				rendererInfo.defaultShadowCastingMode = 1;
				rendererInfo.ignoreOverlays = false;
				rendererInfo.renderer = componentsInChildren[6];
				array[num2] = rendererInfo;
				skinDefInfo.RendererInfos = array;
				SkinDef.MeshReplacement[] array2 = new SkinDef.MeshReplacement[2];
				int num3 = 0;
				SkinDef.MeshReplacement meshReplacement = default(SkinDef.MeshReplacement);
				meshReplacement.mesh = ArtiThiccerPlugin.assetBundle.LoadAsset<Mesh>("Assets\\SkinMods\\ArtiThiccer\\Meshes\\MageMesh.mesh");
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
				Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins")[BodyCatalog.FindBodyIndex(gameObject)] = componentInChildren.skins;
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

		private void BodyCatalog_Init(BodyCatalog.orig_Init orig)
		{
			orig.Invoke();
			ArtiThiccerPlugin.AddMageBodyArtificerNoCapeSkin();
			ArtiThiccerPlugin.AddMageBodyArtificerFatSkin();
		}

		private static AssetBundle assetBundle;

		private static readonly List<Material> materialsWithRoRShader = new List<Material>();
	}
}
