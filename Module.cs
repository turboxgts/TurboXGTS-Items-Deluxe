using BepInEx;
using Alexandria.ItemAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace TurboItems
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(ETGModMainBehaviour.GUID)]
    public class Module : BaseUnityPlugin
    {
        public const string GUID = "turbo.etg.turbodeluxe";
        public const string NAME = "TurboXGTS's Items Deluxe";
        public const string VERSION = "1.0.0";

        public void Start()
        {
            ETGMod.Assets.SetupSpritesFromAssembly(Assembly.GetExecutingAssembly(), "TurboItems/Resources/sprites");
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }

        public void GMStart(GameManager manager)
        {
            //TODO: Go through and fix/change item descriptions
            //TODO: check gun classes and qualities

            ItemBuilder.Init();

            //Passives


            //Actives

            //KoopaShell.Init();


            //Beam weapons

            //YarnBall.Add();
            //LaserDisk.Add();


            //Melee weapons


            //Normal weapons

            //HammerBro.Add();
            //TODO: 3 round burst might be unfinished
            ClockworkAssaultRifle.Add();



            DisembodiedHand.Add();


            InitialiseSynergies.DoInitialisation();
            SynergyFormInitialiser.SynergyInitialiser();
            //Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);
            ETGModConsole.Log("TurboXGTS' Items Deluxe is up and running, hopefully.");

        }

        //public static void Log(string text, string color="#FFFFFF")
        //{
        //    ETGModConsole.Log($"<color={color}>{text}</color>");
        //}

        public void Awake() { }
    }
}
