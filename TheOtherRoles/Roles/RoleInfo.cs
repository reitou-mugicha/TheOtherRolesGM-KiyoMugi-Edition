
using System.Linq;
using System;
using System.Collections.Generic;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;
using UnityEngine;
using TheOtherRoles.Modules;

namespace TheOtherRoles
{
    class RoleInfo
    {
        public Color color;
        public virtual string name { get { return ModTranslation.getString(nameKey); } }
        public virtual string nameColored { get { return Helpers.cs(color, name); } }
        public virtual string introDescription { get { return ModTranslation.getString(nameKey + "IntroDesc"); } }
        public virtual string shortDescription { get { return ModTranslation.getString(nameKey + "ShortDesc"); } }
        public virtual string fullDescription { get { return ModTranslation.getString(nameKey + "FullDesc"); } }
        public virtual string roleOptions
        {
            get
            {
                return GameOptionsDataPatch.optionsToString(baseOption, true);
            }
        }

        public bool enabled { get { return CustomOptionHolder.activateRoles.getBool() && (baseOption == null || baseOption.enabled); } }
        public RoleType roleType;

        private string nameKey;
        private CustomOption baseOption;

        RoleInfo(string name, Color color, CustomOption baseOption, RoleType roleType)
        {
            this.color = color;
            this.nameKey = name;
            this.baseOption = baseOption;
            this.roleType = roleType;
        }

        public static RoleInfo jester = new RoleInfo("jester", Jester.color, CustomOptionHolder.jesterSpawnRate, RoleType.Jester);
        public static RoleInfo mayor = new RoleInfo("mayor", Mayor.color, CustomOptionHolder.mayorSpawnRate, RoleType.Mayor);
        public static RoleInfo engineer = new RoleInfo("engineer", Engineer.color, CustomOptionHolder.engineerSpawnRate, RoleType.Engineer);
        public static RoleInfo sheriff = new RoleInfo("sheriff", Sheriff.color, CustomOptionHolder.sheriffSpawnRate, RoleType.Sheriff);
        public static RoleInfo lighter = new RoleInfo("lighter", Lighter.color, CustomOptionHolder.lighterSpawnRate, RoleType.Lighter);
        public static RoleInfo godfather = new RoleInfo("godfather", Godfather.color, CustomOptionHolder.mafiaSpawnRate, RoleType.Godfather);
        public static RoleInfo mafioso = new RoleInfo("mafioso", Mafioso.color, CustomOptionHolder.mafiaSpawnRate, RoleType.Mafioso);
        public static RoleInfo janitor = new RoleInfo("janitor", Janitor.color, CustomOptionHolder.mafiaSpawnRate, RoleType.Janitor);
        public static RoleInfo morphling = new RoleInfo("morphling", Morphling.color, CustomOptionHolder.morphlingSpawnRate, RoleType.Morphling);
        public static RoleInfo camouflager = new RoleInfo("camouflager", Camouflager.color, CustomOptionHolder.camouflagerSpawnRate, RoleType.Camouflager);
        public static RoleInfo vampire = new RoleInfo("vampire", Vampire.color, CustomOptionHolder.vampireSpawnRate, RoleType.Vampire);
        public static RoleInfo eraser = new RoleInfo("eraser", Eraser.color, CustomOptionHolder.eraserSpawnRate, RoleType.Eraser);
        public static RoleInfo trickster = new RoleInfo("trickster", Trickster.color, CustomOptionHolder.tricksterSpawnRate, RoleType.Trickster);
        public static RoleInfo cleaner = new RoleInfo("cleaner", Cleaner.color, CustomOptionHolder.cleanerSpawnRate, RoleType.Cleaner);
        public static RoleInfo warlock = new RoleInfo("warlock", Warlock.color, CustomOptionHolder.warlockSpawnRate, RoleType.Warlock);
        public static RoleInfo bountyHunter = new RoleInfo("bountyHunter", BountyHunter.color, CustomOptionHolder.bountyHunterSpawnRate, RoleType.BountyHunter);
        public static RoleInfo detective = new RoleInfo("detective", Detective.color, CustomOptionHolder.detectiveSpawnRate, RoleType.Detective);
        public static RoleInfo timeMaster = new RoleInfo("timeMaster", TimeMaster.color, CustomOptionHolder.timeMasterSpawnRate, RoleType.TimeMaster);
        public static RoleInfo medic = new RoleInfo("medic", Medic.color, CustomOptionHolder.medicSpawnRate, RoleType.Medic);
        public static RoleInfo niceShifter = new RoleInfo("niceShifter", Shifter.color, CustomOptionHolder.shifterSpawnRate, RoleType.Shifter);
        public static RoleInfo corruptedShifter = new RoleInfo("corruptedShifter", Shifter.color, CustomOptionHolder.shifterSpawnRate, RoleType.Shifter);
        public static RoleInfo niceSwapper = new RoleInfo("niceSwapper", Swapper.color, CustomOptionHolder.swapperSpawnRate, RoleType.Swapper);
        public static RoleInfo evilSwapper = new RoleInfo("evilSwapper", Palette.ImpostorRed, CustomOptionHolder.swapperSpawnRate, RoleType.Swapper);
        public static RoleInfo seer = new RoleInfo("seer", Seer.color, CustomOptionHolder.seerSpawnRate, RoleType.Seer);
        public static RoleInfo hacker = new RoleInfo("hacker", Hacker.color, CustomOptionHolder.hackerSpawnRate, RoleType.Hacker);
        public static RoleInfo tracker = new RoleInfo("tracker", Tracker.color, CustomOptionHolder.trackerSpawnRate, RoleType.Tracker);
        public static RoleInfo snitch = new RoleInfo("snitch", Snitch.color, CustomOptionHolder.snitchSpawnRate, RoleType.Snitch);
        public static RoleInfo jackal = new RoleInfo("jackal", Jackal.color, CustomOptionHolder.jackalSpawnRate, RoleType.Jackal);
        public static RoleInfo sidekick = new RoleInfo("sidekick", Sidekick.color, CustomOptionHolder.jackalSpawnRate, RoleType.Sidekick);
        public static RoleInfo spy = new RoleInfo("spy", Spy.color, CustomOptionHolder.spySpawnRate, RoleType.Spy);
        public static RoleInfo securityGuard = new RoleInfo("securityGuard", SecurityGuard.color, CustomOptionHolder.securityGuardSpawnRate, RoleType.SecurityGuard);
        public static RoleInfo arsonist = new RoleInfo("arsonist", Arsonist.color, CustomOptionHolder.arsonistSpawnRate, RoleType.Arsonist);
        public static RoleInfo niceGuesser = new RoleInfo("niceGuesser", Guesser.color, CustomOptionHolder.guesserSpawnRate, RoleType.NiceGuesser);
        public static RoleInfo evilGuesser = new RoleInfo("evilGuesser", Palette.ImpostorRed, CustomOptionHolder.guesserSpawnRate, RoleType.EvilGuesser);
        public static RoleInfo bait = new RoleInfo("bait", Bait.color, CustomOptionHolder.baitSpawnRate, RoleType.Bait);
        public static RoleInfo impostor = new RoleInfo("impostor", Palette.ImpostorRed, null, RoleType.Impostor);
        public static RoleInfo lawyer = new RoleInfo("lawyer", Lawyer.color, CustomOptionHolder.lawyerSpawnRate, RoleType.Lawyer);
        public static RoleInfo pursuer = new RoleInfo("pursuer", Pursuer.color, CustomOptionHolder.lawyerSpawnRate, RoleType.Pursuer);
        public static RoleInfo crewmate = new RoleInfo("crewmate", Palette.CrewmateBlue, null, RoleType.Crewmate);
        public static RoleInfo lovers = new RoleInfo("lovers", Lovers.color, CustomOptionHolder.loversSpawnRate, RoleType.Lovers);
        public static RoleInfo gm = new RoleInfo("gm", GM.color, CustomOptionHolder.gmEnabled, RoleType.GM);
        public static RoleInfo witch = new RoleInfo("witch", Witch.color, CustomOptionHolder.witchSpawnRate, RoleType.Witch);
        public static RoleInfo vulture = new RoleInfo("vulture", Vulture.color, CustomOptionHolder.vultureSpawnRate, RoleType.Vulture);
        public static RoleInfo medium = new RoleInfo("medium", Medium.color, CustomOptionHolder.mediumSpawnRate, RoleType.Medium);
        public static RoleInfo ninja = new RoleInfo("ninja", Ninja.color, CustomOptionHolder.ninjaSpawnRate, RoleType.Ninja);
        public static RoleInfo plagueDoctor = new RoleInfo("plagueDoctor", PlagueDoctor.color, CustomOptionHolder.plagueDoctorSpawnRate, RoleType.PlagueDoctor);
        public static RoleInfo nekoKabocha = new RoleInfo("nekoKabocha", NekoKabocha.color, CustomOptionHolder.nekoKabochaSpawnRate, RoleType.NekoKabocha);
        public static RoleInfo serialKiller = new RoleInfo("serialKiller", SerialKiller.color, CustomOptionHolder.serialKillerSpawnRate, RoleType.SerialKiller);
        public static RoleInfo fox = new RoleInfo("fox", Fox.color, CustomOptionHolder.foxSpawnRate, RoleType.Fox);
        public static RoleInfo immoralist = new RoleInfo("immoralist", Immoralist.color, CustomOptionHolder.foxSpawnRate, RoleType.Immoralist);
        public static RoleInfo fortuneTeller = new RoleInfo("fortuneTeller", FortuneTeller.color, CustomOptionHolder.fortuneTellerSpawnRate, RoleType.FortuneTeller);
        public static RoleInfo sprinter = new RoleInfo("sprinter", Sprinter.color, CustomOptionHolder.sprinterSpawnRate, RoleType.Sprinter);
        public static RoleInfo evilHacker = new RoleInfo("evilHacker", EvilHacker.color, CustomOptionHolder.evilHackerSpawnRate, RoleType.EvilHacker);
        public static RoleInfo portalmaker = new RoleInfo("portalmaker", Portalmaker.color, CustomOptionHolder.portalmakerSpawnRate, RoleType.Portalmaker);
        public static RoleInfo assassin = new RoleInfo("assassin", Assassin.color, CustomOptionHolder.assassinSpawnRate, RoleType.Assassin);
        public static RoleInfo customImpostor = new RoleInfo("customImpostor", CustomImpostor.color, CustomOptionHolder.customImpostorSpawnRate, RoleType.CustomImpostor);
        public static RoleInfo doubleKiller = new RoleInfo("doubleKiller", DoubleKiller.color, CustomOptionHolder.doubleKillerSpawnRate, RoleType.DoubleKiller);
        //public static RoleInfo chunibyo = new RoleInfo("chunibyo", Chunibyo.color, CustomOptionHolder.chunibyoSpawnRate, RoleType.Chunibyo);
        public static RoleInfo boss = new RoleInfo("boss", Boss.color, CustomOptionHolder.yakuzaSpawnRate, RoleType.Boss);
        public static RoleInfo staff = new RoleInfo("staff", Staff.color, CustomOptionHolder.yakuzaSpawnRate, RoleType.Staff);
        public static RoleInfo gun = new RoleInfo("gun", Gun.color, CustomOptionHolder.yakuzaSpawnRate, RoleType.Gun);
        public static RoleInfo underTaker = new RoleInfo("underTaker", UnderTaker.color, CustomOptionHolder.underTakerSpawnRate, RoleType.UnderTaker);
        public static RoleInfo bakery = new RoleInfo("bakery", Bakery.color, CustomOptionHolder.bakerySpawnRate, RoleType.Bakery);
        public static RoleInfo hawkEye = new RoleInfo("hawkEye", HawkEye.color, CustomOptionHolder.hawkEyeSpawnRate, RoleType.HawkEye);/*
        public static RoleInfo creator = new RoleInfo("creator", Creator.color, CustomOptionHolder.creatorSpawnRate, RoleType.Creator);
        public static RoleInfo student = new RoleInfo("student", Student.color, CustomOptionHolder.sheriffSpawnRate, RoleType.Student);*/
        public static RoleInfo randomizer = new RoleInfo("randomizer", Randomizer.color, CustomOptionHolder.randomizerSpawnRate, RoleType.Randomizer);
        public static RoleInfo accelerator = new RoleInfo("accelerator", Accelerator.color, CustomOptionHolder.acceleratorSpawnRate, RoleType.Accelerator);
        public static RoleInfo trapper = new RoleInfo("trapper", Trapper.color, CustomOptionHolder.trapperSpawnRate, RoleType.Trapper);
        public static RoleInfo timeReviver = new RoleInfo("timeReviver", TimeReviver.color, CustomOptionHolder.timeReviverSpawnRate, RoleType.TimeReviver);
        public static RoleInfo silencer = new RoleInfo("silencer", Silencer.color, CustomOptionHolder.silencerSpawnRate, RoleType.Silencer);
        public static RoleInfo jammer = new RoleInfo("jammer", Jammer.color, CustomOptionHolder.jammerSpawnRate, RoleType.Jammer);
        public static RoleInfo immortality = new RoleInfo("immortality", Immortality.color, CustomOptionHolder.immortalitySpawnRate, RoleType.Immortality);
        //public static RoleInfo sunfish = new RoleInfo("sunfish", Sunfish.color, CustomOptionHolder.sunfishSpawnRate, RoleType.Sunfish);
        public static RoleInfo eater = new RoleInfo("eater", Eater.color, CustomOptionHolder.eaterSpawnRate, RoleType.Eater);
        public static RoleInfo darkHero = new RoleInfo("darkHero", DarkHero.color, CustomOptionHolder.darkHeroSpawnRate, RoleType.DarkHero);
        public static RoleInfo prophet = new RoleInfo("prophet", Prophet.color, CustomOptionHolder.prophetSpawnRate, RoleType.Prophet);


        public static List<RoleInfo> allRoleInfos = new List<RoleInfo>() {
                impostor,
                godfather,
                mafioso,
                janitor,
                morphling,
                camouflager,
                vampire,
                eraser,
                trickster,
                cleaner,
                warlock,
                bountyHunter,
                witch,
                ninja,
                serialKiller,
                niceGuesser,
                evilGuesser,
                mayor,
                lovers,
                jester,
                arsonist,
                jackal,
                sidekick,
                vulture,
                pursuer,
                lawyer,
                crewmate,
                niceShifter,
                corruptedShifter,
                engineer,
                sheriff,
                lighter,
                detective,
                timeMaster,
                medic,
                niceSwapper,
                evilSwapper,
                seer,
                hacker,
                tracker,
                snitch,
                spy,
                securityGuard,
                bait,
                gm,
                medium,
                plagueDoctor,
                nekoKabocha,
                fox,
                immoralist,
                fortuneTeller,
                sprinter,
                evilHacker,
                portalmaker,
                assassin,
                customImpostor,
                doubleKiller,
                //chunibyo,
                boss,
                staff,
                gun,
                hawkEye,
                underTaker,
                bakery,
                //student,
                //creator
                randomizer,
                accelerator,
                trapper,
                timeReviver,
                silencer,
                jammer,
                immortality,
                //sunfish,
                eater,
                darkHero,
                prophet,
            };

        public static string tl(string key)
        {
            return ModTranslation.getString(key);
        }

        public static List<RoleInfo> getRoleInfoForPlayer(PlayerControl p, RoleType[] excludeRoles = null, bool includeHidden = false)
        {
            List<RoleInfo> infos = new List<RoleInfo>();
            if (p == null) return infos;

            // Special roles
            if (p.isRole(RoleType.Jester)) infos.Add(jester);
            if (p.isRole(RoleType.Mayor)) infos.Add(mayor);
            if (p.isRole(RoleType.Engineer)) infos.Add(engineer);
            if (p.isRole(RoleType.Sheriff)) infos.Add(sheriff);
            if (p.isRole(RoleType.Lighter)) infos.Add(lighter);
            if (p.isRole(RoleType.Godfather)) infos.Add(godfather);
            if (p.isRole(RoleType.Mafioso)) infos.Add(mafioso);
            if (p.isRole(RoleType.Janitor)) infos.Add(janitor);
            if (p.isRole(RoleType.Morphling)) infos.Add(morphling);
            if (p.isRole(RoleType.Camouflager)) infos.Add(camouflager);
            if (p.isRole(RoleType.EvilHacker)) infos.Add(evilHacker);
            if (p.isRole(RoleType.Vampire)) infos.Add(vampire);
            if (p.isRole(RoleType.Eraser)) infos.Add(eraser);
            if (p.isRole(RoleType.Trickster)) infos.Add(trickster);
            if (p.isRole(RoleType.Cleaner)) infos.Add(cleaner);
            if (p.isRole(RoleType.Warlock)) infos.Add(warlock);
            if (p.isRole(RoleType.Witch)) infos.Add(witch);
            if (p.isRole(RoleType.Detective)) infos.Add(detective);
            if (p.isRole(RoleType.TimeMaster)) infos.Add(timeMaster);
            if (p.isRole(RoleType.Medic)) infos.Add(medic);
            if (p.isRole(RoleType.Shifter)) infos.Add(Shifter.isNeutral ? corruptedShifter : niceShifter);
            if (p.isRole(RoleType.Swapper)) infos.Add(p.Data.Role.IsImpostor ? evilSwapper : niceSwapper);
            if (p.isRole(RoleType.Seer)) infos.Add(seer);
            if (p.isRole(RoleType.Hacker)) infos.Add(hacker);
            if (p.isRole(RoleType.Tracker)) infos.Add(tracker);
            if (p.isRole(RoleType.Snitch)) infos.Add(snitch);
            if (p.isRole(RoleType.Jackal) || (Jackal.formerJackals != null && Jackal.formerJackals.Any(x => x.PlayerId == p.PlayerId))) infos.Add(jackal);
            if (p.isRole(RoleType.Sidekick)) infos.Add(sidekick);
            if (p.isRole(RoleType.Spy)) infos.Add(spy);
            if (p.isRole(RoleType.SecurityGuard)) infos.Add(securityGuard);
            if (p.isRole(RoleType.Arsonist)) infos.Add(arsonist);
            if (p.isRole(RoleType.NiceGuesser)) infos.Add(niceGuesser);
            if (p.isRole(RoleType.EvilGuesser)) infos.Add(evilGuesser);
            if (p.isRole(RoleType.BountyHunter)) infos.Add(bountyHunter);
            if (p.isRole(RoleType.Bait)) infos.Add(bait);
            if (p.isRole(RoleType.GM)) infos.Add(gm);
            if (p.isRole(RoleType.Vulture)) infos.Add(vulture);
            if (p.isRole(RoleType.Medium)) infos.Add(medium);
            if (p.isRole(RoleType.Lawyer)) infos.Add(lawyer);
            if (p.isRole(RoleType.Pursuer)) infos.Add(pursuer);
            if (p.isRole(RoleType.Ninja)) infos.Add(ninja);
            if (p.isRole(RoleType.HawkEye)) infos.Add(hawkEye);
            if (p.isRole(RoleType.Portalmaker)) infos.Add(portalmaker);
            if (p.isRole(RoleType.CustomImpostor)) infos.Add(customImpostor);
            if (p.isRole(RoleType.DoubleKiller)) infos.Add(doubleKiller);
            //if (p.isRole(RoleType.Chunibyo)) infos.Add(chunibyo);
            if (p.isRole(RoleType.Boss)) infos.Add(boss);
            if (p.isRole(RoleType.Staff)) infos.Add(staff);
            if (p.isRole(RoleType.Gun)) infos.Add(gun);
            if (p.isRole(RoleType.Assassin)) infos.Add(assassin);
            if (p.isRole(RoleType.PlagueDoctor)) infos.Add(plagueDoctor);
            if (p.isRole(RoleType.NekoKabocha)) infos.Add(nekoKabocha);
            if (p.isRole(RoleType.UnderTaker)) infos.Add(underTaker);
            if (p.isRole(RoleType.Bakery)) infos.Add(bakery);
            if (p.isRole(RoleType.SerialKiller)) infos.Add(serialKiller);/*
            if (p.isRole(RoleType.Student)) infos.Add(student);
            if (p.isRole(RoleType.Creator)) infos.Add(creator);*/
            if (p.isRole(RoleType.Fox)) infos.Add(fox);
            if (p.isRole(RoleType.Immoralist)) infos.Add(immoralist);
            if (p.isRole(RoleType.FortuneTeller))
            {
                if (includeHidden || PlayerControl.LocalPlayer.Data.IsDead)
                {
                    infos.Add(fortuneTeller);
                }
                else
                {
                    var info = FortuneTeller.isCompletedNumTasks(p) ? fortuneTeller : crewmate;
                    infos.Add(info);
                }
            }
            if (p.isRole(RoleType.Sprinter)) infos.Add(sprinter);
            if (p.isRole(RoleType.Randomizer)) infos.Add(randomizer);
            if (p.isRole(RoleType.Accelerator)) infos.Add(accelerator);
            if (p.isRole(RoleType.Trapper)) infos.Add(trapper);
            if (p.isRole(RoleType.TimeReviver)) infos.Add(timeReviver);
            if (p.isRole(RoleType.Silencer)) infos.Add(silencer);
            if (p.isRole(RoleType.Jammer)) infos.Add(jammer);
            if (p.isRole(RoleType.Immortality)) infos.Add(immortality);
            /*if (p.isRole(RoleType.Sunfish))
            {
                if(includeHidden || PlayerControl.LocalPlayer.isDead())
                {
                    infos.Add(sunfish);
                }
                else 
                {
                    var info = Sunfish.isSunfishCompletedTasks() ? sunfish : crewmate;
                    infos.Add(info);
                }
            }*/
            if (p.isRole(RoleType.Eater)) infos.Add(eater);
            if (p.isRole(RoleType.DarkHero)) infos.Add(darkHero);
            if (p.isRole(RoleType.Prophet)) infos.Add(prophet);

            // Default roles
            if (infos.Count == 0 && p.Data.Role.IsImpostor) infos.Add(impostor); // Just Impostor
            if (infos.Count == 0 && !p.Data.Role.IsImpostor) infos.Add(crewmate); // Just Crewmate

            // Modifier
            if (p.isLovers()) infos.Add(lovers);

            if (excludeRoles != null)
                infos.RemoveAll(x => excludeRoles.Contains(x.roleType));

            return infos;
        }

        public static String GetRolesString(PlayerControl p, bool useColors, RoleType[] excludeRoles = null, bool includeHidden = false)
        {
            if (p?.Data?.Disconnected != false) return "";

            var roleInfo = getRoleInfoForPlayer(p, excludeRoles, includeHidden);
            string roleName = String.Join(" ", roleInfo.Select(x => useColors ? Helpers.cs(x.color, x.name) : x.name).ToArray());
            if (Lawyer.target != null && p?.PlayerId == Lawyer.target.PlayerId && PlayerControl.LocalPlayer != Lawyer.target) roleName += (useColors ? Helpers.cs(Pursuer.color, " §") : " §");

            if (p.hasModifier(ModifierType.Madmate) || p.hasModifier(ModifierType.CreatedMadmate))
            {
                // Madmate only
                if (roleInfo.Contains(crewmate))
                {
                    roleName = useColors ? Helpers.cs(Madmate.color, Madmate.fullName) : Madmate.fullName;
                }
                else
                {
                    string prefix = useColors ? Helpers.cs(Madmate.color, Madmate.prefix) : Madmate.prefix;
                    roleName = String.Join(" ", roleInfo.Select(x => useColors ? Helpers.cs(Madmate.color, x.name) : x.name).ToArray());
                    roleName = prefix + roleName;
                }
            }
            if (p.hasModifier(ModifierType.AntiTeleport))
            {
                string postfix = useColors ? Helpers.cs(AntiTeleport.color, AntiTeleport.postfix) : AntiTeleport.postfix;
                // roleName = String.Join(" ", roleInfo.Select(x => useColors? Helpers.cs(x.color, x.name)  : x.name).ToArray());
                roleName = roleName + postfix;
            }
            if (p.hasModifier(ModifierType.Opportunist))
            {
                string postfix = useColors ? Helpers.cs(Opportunist.color, Opportunist.postfix) : Opportunist.postfix;
                // roleName = String.Join(" ", roleInfo.Select(x => useColors? Helpers.cs(x.color, x.name)  : x.name).ToArray());
                roleName = roleName + postfix;
            }
            if (p.hasModifier(ModifierType.Watcher))
            {
                string postfix = useColors ? Helpers.cs(Watcher.color, Watcher.postfix) : Watcher.postfix;
                // roleName = String.Join(" ", roleInfo.Select(x => useColors? Helpers.cs(x.color, x.name)  : x.name).ToArray());
                roleName = roleName + postfix;
            }
            if (p.hasModifier(ModifierType.Sunglasses))
            {
                string postfix = useColors ? Helpers.cs(Sunglasses.color, Sunglasses.postfix) : Sunglasses.postfix;
                // roleName = String.Join(" ", roleInfo.Select(x => useColors? Helpers.cs(x.color, x.name)  : x.name).ToArray());
                roleName = roleName + postfix;
            }
            return roleName;
        }
    }
}