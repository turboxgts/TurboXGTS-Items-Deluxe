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
            DevilsHorns.Register();
            BulletSpeedShift.Register();
            YarnBall.Add();
            SelfHarmBeamWeaponBecauseNevernamedToldMeItWasOkayTo.Add();
            WoodStake.Add();
            HammerBro.Add();
            DefinitelyNotBrimstone.Add();
            //MirrorSword.Add();
            MirrorSwordMeleeOnly.Add();
            //MirrorSwordBeam.Add(); //will be unused until something fixes the weird beam ammo bug
            //MirrorSwordLaser.Add(); //unused 'til I figure out some stuffs
            //GargoyleHandLeft.Add();
            //GargoyleHandRight.Add();
            ClockworkAssaultRifle.Add();
            MasterSword.Add();
            PhrenicBow.Add();
            ChoiceBottle.Init();
            KoopaShell.Init();
            SamusHelmet.Register();
            HuntingKit.Register();
            MedicalBox.Register();
            BloodCoveredCloak.Register();
            AC15Pack.Register();
            TrankGunPack.Register();
            GunbowPack.Register();
            IceTray.Register();
            Yin.Add();
            Yang.Add();
            InitialiseSynergies.DoInitialisation();
            SynergyFormInitialiser.SynergyInitialiser();
            //Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);
            ETGModConsole.Log("TurboXGTS' Items Deluxe is up and running, hopefully.");
        }

        public static void Log(string text, string color="#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public void Awake() { }
    }
}
