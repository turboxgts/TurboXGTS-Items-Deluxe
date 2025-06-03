using BepInEx;
using ItemAPI;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TurboItems
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(ETGModMainBehaviour.GUID)]
    public class Module : BaseUnityPlugin
    {
        //public static readonly string MOD_NAME = "TurboXGTSItems";
        //public static readonly string VERSION = "2.1.0";
        //public static readonly string TEXT_COLOR = "#00FFFF";
        public const string GUID = "turbo.etg.turbodeluxe";
        public const string NAME = "TurboXGTS's Items Deluxe";
        public const string VERSION = "1.0.0";

        public void Start()
        {
            ETGModMainBehaviour.WaitForGameManagerStart(GMStart);
        }

        public void GMStart(GameManager manager)
        {
            ItemBuilder.Init();

            //Normal passives (very bad remove or redo)

            DevilsHorns.Register();
            BulletSpeedShift.Register();

            //Actives

            ChoiceBottle.Init();
            KoopaShell.Init();

            //Beam weapons

            //YarnBall.Add(); //unspecified NRE
            //SelfHarmBeamWeaponBecauseNevernamedToldMeItWasOkayTo.Add(); //unspecified NRE
            //DefinitelyNotBrimstone.Add();

            //Melee weapons

            WoodStake.Add(); //fine
            //MirrorSword.Add();
            //MirrorSwordMeleeOnly.Add(); //unspecified NRE
            //MasterSword.Add();

            //Normal weapons

            //HammerBro.Add();
            ClockworkAssaultRifle.Add();
            PhrenicBow.Add();

            //Unfinished stuff

            MirrorSwordBeam.Add(); //will be unused until something fixes the weird beam ammo bug
            MirrorSwordLaser.Add(); //unused 'til I figure out some stuffs
            GargoyleHandLeft.Add();
            GargoyleHandRight.Add();
            Yin.Add();
            Yang.Add();

            //Item bundles (very bad remove or redo)

            SamusHelmet.Register();
            HuntingKit.Register();
            MedicalBox.Register();
            BloodCoveredCloak.Register();
            AC15Pack.Register();
            TrankGunPack.Register();
            GunbowPack.Register();
            IceTray.Register();


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
