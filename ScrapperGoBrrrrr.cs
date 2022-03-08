using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using MonoMod.Cil;
using UnityEngine;
using UnityEngine.Networking;

namespace ScrapperGoBrrrrr
{
	//This is an example plugin that can be put in BepInEx/plugins/ExamplePlugin/ExamplePlugin.dll to test out.
    //It's a small plugin that adds a relatively simple item to the game, and gives you that item whenever you press F2.

    //This attribute specifies that we have a dependency on R2API, as we're using it to add our item to the game.
    //You don't need this if you're not using R2API in your plugin, it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    //[BepInDependency(R2API.R2API.PluginGUID)]
	
	//This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	
	//This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    public class ScrapperGoBrrrrr : BaseUnityPlugin
	{
        //The Plugin GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config).
        //If we see this PluginGUID as it is on thunderstore, we will deprecate this mod. Change the PluginAuthor and the PluginName !
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "bottleOfun";
        public const string PluginName = "ScrapperGoBrrrrr";
        public const string PluginVersion = "1.0.0";

		//The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            //Init our logging class so that we can properly log for debugging
            Log.Init(Logger);
            
            On.RoR2.Stage.Start += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    typeof(EntityStates.Scrapper.ScrappingToIdle).SetFieldValue("duration", 0f);
                    typeof(EntityStates.Scrapper.WaitToBeginScrapping).SetFieldValue("duration", 0f);
                    typeof(EntityStates.Scrapper.Scrapping).SetFieldValue("duration", 0f);
                    //typeof(EntityStates.Duplicator.Duplicating).SetFieldValue("timeBetweenStartAndDropDroplet", 0f);
                }
            };
           
        }

    }
}
