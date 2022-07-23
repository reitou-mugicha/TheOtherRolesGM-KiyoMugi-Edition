using System.Linq;
using HarmonyLib;
using System;
using System.Collections.Generic;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;
using System.Reflection;

namespace TheOtherRoles
{
    public enum RoleType
    {
        Crewmate = 0,
        Shifter,
        Engineer,
        Sheriff,
        Lighter,
        Detective,
        TimeMaster,
        Medic,
        Swapper,
        Seer,
        Hacker,
        Tracker,
        Snitch,
        Spy,
        SecurityGuard,
        Bait,
        Medium,
        FortuneTeller,
        Sprinter,
        Portalmaker,
        Chunibyo,
        Boss,
        Staff,
        Gun,
        Mayor,
        Bakery,
        TimeReviver,
<<<<<<< HEAD
        Timer,
=======
        Jammer,
        Immortality,
        //Sunfish,
        Prophet,
        Bat,
>>>>>>> master


        Impostor = 100,
        Godfather,
        Mafioso,
        Janitor,
        Morphling,
        Camouflager,
        Vampire,
        Eraser,
        Trickster,
        Cleaner,
        Warlock,
        BountyHunter,
        Witch,
        Ninja,
        NekoKabocha,
        Madmate,
        SerialKiller,
        EvilHacker,
        Assassin,
        CustomImpostor,
        HawkEye,
        DoubleKiller,
        UnderTaker,
        Randomizer,
        Accelerator,
        Trapper,
        Silencer,
<<<<<<< HEAD
=======
        Eater,
>>>>>>> master


        Lovers = 150,
        EvilGuesser,
        NiceGuesser,
        Jester,
        Arsonist,
        Jackal,
        Sidekick,
        Vulture,
        Lawyer,
        Pursuer,
        PlagueDoctor,
        Fox,
        Immoralist,
        Necromancer,
        Zombie,
<<<<<<< HEAD
=======
        ProTasker,
>>>>>>> master


        GM = 200,


        // don't put anything below this
        NoRole = int.MaxValue
    }

    [HarmonyPatch]
    public static class RoleData
    {
        public static Dictionary<RoleType, Type> allRoleTypes = new Dictionary<RoleType, Type>
        {
            // Crew
            { RoleType.Sheriff, typeof(RoleBase<Sheriff>) },
            { RoleType.Lighter, typeof(RoleBase<Lighter>) },
            { RoleType.FortuneTeller, typeof(RoleBase<FortuneTeller>) },
            { RoleType.Sprinter, typeof(RoleBase<Sprinter>) },
            { RoleType.Mayor, typeof(RoleBase<Mayor>) },
<<<<<<< HEAD
            //{ RoleType.Chunibyo, typeof(RoleBase<Chunibyo>) },
=======
            { RoleType.Chunibyo, typeof(RoleBase<Chunibyo>) },
>>>>>>> master
            { RoleType.Boss, typeof(RoleBase<Boss>) },
            { RoleType.Staff, typeof(RoleBase<Staff>) },
            { RoleType.Gun, typeof(RoleBase<Gun>) },
            { RoleType.Bakery, typeof(RoleBase<Bakery>) },
            //{ RoleType.Creator, typeof(RoleBase<Creator>) },
            { RoleType.TimeReviver, typeof(RoleBase<TimeReviver>) },
<<<<<<< HEAD
=======
            { RoleType.Jammer, typeof(RoleBase<Jammer>) },
            { RoleType.Immortality, typeof(RoleBase<Immortality>) },
            //{ RoleType.Sunfish, typeof(RoleBase<Sunfish>) },
            { RoleType.Bat, typeof(RoleBase<Bat>) },
>>>>>>> master

            // Impostor
            { RoleType.Ninja, typeof(RoleBase<Ninja>) },
            { RoleType.NekoKabocha, typeof(RoleBase<NekoKabocha>) },
            { RoleType.SerialKiller, typeof(RoleBase<SerialKiller>) },
            { RoleType.HawkEye, typeof(RoleBase<HawkEye>) },
            { RoleType.CustomImpostor, typeof(RoleBase<CustomImpostor>) },
            { RoleType.DoubleKiller, typeof(RoleBase<DoubleKiller>) },
            { RoleType.Randomizer, typeof(RoleBase<Randomizer>) },
            { RoleType.Accelerator, typeof(RoleBase<Accelerator>) },
            { RoleType.Trapper, typeof(RoleBase<Trapper>) },
            { RoleType.Silencer, typeof(RoleBase<Silencer>) },
<<<<<<< HEAD
=======
            { RoleType.Eater, typeof(RoleBase<Eater>) },
>>>>>>> master

            // Neutral
            { RoleType.PlagueDoctor, typeof(RoleBase<PlagueDoctor>) },
            { RoleType.Fox, typeof(RoleBase<Fox>) },
            { RoleType.Immoralist, typeof(RoleBase<Immoralist>) },
            //{ RoleType.King, typeof(RoleBase<King>) },
            //{ RoleType.Minions, typeof(RoleBase<Minions>) },
        };
    }

    public abstract class Role
    {
        public static List<Role> allRoles = new List<Role>();
        public PlayerControl player;
        public RoleType roleId;

        public abstract void OnMeetingStart();
        public abstract void OnMeetingEnd();
        public abstract void FixedUpdate();
        public abstract void OnKill(PlayerControl target);
        public abstract void OnDeath(PlayerControl killer = null);
        public abstract void HandleDisconnect(PlayerControl player, DisconnectReasons reason);
        public virtual void ResetRole() { }
        public virtual void PostInit() { }

        public static void ClearAll()
        {
            allRoles = new List<Role>();
        }
    }

    [HarmonyPatch]
    public abstract class RoleBase<T> : Role where T : RoleBase<T>, new()
    {
        public static List<T> players = new();
        public static RoleType RoleType;

        public void Init(PlayerControl player)
        {
            this.player = player;
            players.Add((T)this);
            allRoles.Add(this);
            PostInit();
        }

        public static T local
        {
            get
            {
                return players.FirstOrDefault(x => x.player == PlayerControl.LocalPlayer);
            }
        }

        public static List<PlayerControl> allPlayers
        {
            get
            {
                return players.Select(x => x.player).ToList();
            }
        }

        public static List<PlayerControl> livingPlayers
        {
            get
            {
                return players.Select(x => x.player).Where(x => x.isAlive()).ToList();
            }
        }

        public static List<PlayerControl> deadPlayers
        {
            get
            {
                return players.Select(x => x.player).Where(x => !x.isAlive()).ToList();
            }
        }

        public static bool exists
        {
            get { return Helpers.RolesEnabled && players.Count > 0; }
        }

        public static T getRole(PlayerControl player = null)
        {
            player = player ?? PlayerControl.LocalPlayer;
            return players.FirstOrDefault(x => x.player == player);
        }

        public static bool isRole(PlayerControl player)
        {
            return players.Any(x => x.player == player);
        }

        public static void setRole(PlayerControl player)
        {
            if (!isRole(player))
            {
                T role = new T();
                role.Init(player);
            }
        }

        public static void eraseRole(PlayerControl player)
        {
            players.DoIf(x => x.player == player, x => x.ResetRole());
            players.RemoveAll(x => x.player == player && x.roleId == RoleType);
            allRoles.RemoveAll(x => x.player == player && x.roleId == RoleType);
        }

        public static void swapRole(PlayerControl p1, PlayerControl p2)
        {
            var index = players.FindIndex(x => x.player == p1);
            if (index >= 0)
                players[index].player = p2;
        }
    }

    public static class RoleHelpers
    {
        public static bool isRole(this PlayerControl player, RoleType role)
        {
            foreach (var t in RoleData.allRoleTypes)
                if (role == t.Key)
                    return (bool)t.Value.GetMethod("isRole", BindingFlags.Public | BindingFlags.Static)?.Invoke(null, new object[] { player });

            switch (role)
            {
                case RoleType.Jester:
                    return Jester.jester == player;
                case RoleType.Engineer:
                    return Engineer.engineer == player;
                case RoleType.Godfather:
                    return Godfather.godfather == player;
                case RoleType.Mafioso:
                    return Mafioso.mafioso == player;
                case RoleType.Janitor:
                    return Janitor.janitor == player;
                case RoleType.Detective:
                    return Detective.detective == player;
                case RoleType.TimeMaster:
                    return TimeMaster.timeMaster == player;
                case RoleType.Medic:
                    return Medic.medic == player;
                case RoleType.Shifter:
                    return Shifter.shifter == player;
                case RoleType.Swapper:
                    return Swapper.swapper == player;
                case RoleType.Seer:
                    return Seer.seer == player;
                case RoleType.Morphling:
                    return Morphling.morphling == player;
                case RoleType.Camouflager:
                    return Camouflager.camouflager == player;
                case RoleType.EvilHacker:
                    return EvilHacker.evilHacker == player;
                case RoleType.Hacker:
                    return Hacker.hacker == player;
                case RoleType.Tracker:
                    return Tracker.tracker == player;
                case RoleType.Vampire:
                    return Vampire.vampire == player;
                case RoleType.Snitch:
                    return Snitch.snitch == player;
                case RoleType.Jackal:
                    return Jackal.jackal == player;
                case RoleType.Sidekick:
                    return Sidekick.sidekick == player;
                case RoleType.Eraser:
                    return Eraser.eraser == player;
                case RoleType.Spy:
                    return Spy.spy == player;
                case RoleType.Trickster:
                    return Trickster.trickster == player;
                case RoleType.Cleaner:
                    return Cleaner.cleaner == player;
                case RoleType.Warlock:
                    return Warlock.warlock == player;
                case RoleType.SecurityGuard:
                    return SecurityGuard.securityGuard == player;
                case RoleType.Arsonist:
                    return Arsonist.arsonist == player;
                case RoleType.EvilGuesser:
                    return Guesser.evilGuesser == player;
                case RoleType.NiceGuesser:
                    return Guesser.niceGuesser == player;
                case RoleType.BountyHunter:
                    return BountyHunter.bountyHunter == player;
                case RoleType.Bait:
                    return Bait.bait == player;
                case RoleType.GM:
                    return GM.gm == player;
                case RoleType.Vulture:
                    return Vulture.vulture == player;
                case RoleType.Medium:
                    return Medium.medium == player;
                case RoleType.Witch:
                    return Witch.witch == player;
                case RoleType.Lawyer:
                    return Lawyer.lawyer == player;
                case RoleType.Pursuer:
                    return Pursuer.pursuer == player;
                case RoleType.Portalmaker:
                    return Portalmaker.portalmaker == player;
                case RoleType.Assassin:
                    return Assassin.assassin == player;
                case RoleType.UnderTaker:
                    return UnderTaker.underTaker == player;
                default:
                    TheOtherRolesPlugin.Logger.LogError("isRole: no method found for role type {role}");
                    break;
            }

            return false;
        }

        public static void setRole(this PlayerControl player, RoleType role)
        {
            foreach (var t in RoleData.allRoleTypes)
            {
                if (role == t.Key)
                {
                    t.Value.GetMethod("setRole", BindingFlags.Public | BindingFlags.Static)?.Invoke(null, new object[] { player });
                    return;
                }
            }

            switch (role)
            {
                case RoleType.Jester:
                    Jester.jester = player;
                    break;
                case RoleType.Engineer:
                    Engineer.engineer = player;
                    break;
                case RoleType.Godfather:
                    Godfather.godfather = player;
                    break;
                case RoleType.Mafioso:
                    Mafioso.mafioso = player;
                    break;
                case RoleType.Janitor:
                    Janitor.janitor = player;
                    break;
                case RoleType.Detective:
                    Detective.detective = player;
                    break;
                case RoleType.TimeMaster:
                    TimeMaster.timeMaster = player;
                    break;
                case RoleType.Medic:
                    Medic.medic = player;
                    break;
                case RoleType.Shifter:
                    Shifter.shifter = player;
                    break;
                case RoleType.Swapper:
                    Swapper.swapper = player;
                    break;
                case RoleType.Seer:
                    Seer.seer = player;
                    break;
                case RoleType.Morphling:
                    Morphling.morphling = player;
                    break;
                case RoleType.Camouflager:
                    Camouflager.camouflager = player;
                    break;
                case RoleType.EvilHacker:
                    EvilHacker.evilHacker = player;
                    break;
                case RoleType.Hacker:
                    Hacker.hacker = player;
                    break;
                case RoleType.Tracker:
                    Tracker.tracker = player;
                    break;
                case RoleType.Vampire:
                    Vampire.vampire = player;
                    break;
                case RoleType.Snitch:
                    Snitch.snitch = player;
                    break;
                case RoleType.Jackal:
                    Jackal.jackal = player;
                    break;
                case RoleType.Sidekick:
                    Sidekick.sidekick = player;
                    break;
                case RoleType.Eraser:
                    Eraser.eraser = player;
                    break;
                case RoleType.Spy:
                    Spy.spy = player;
                    break;
                case RoleType.Trickster:
                    Trickster.trickster = player;
                    break;
                case RoleType.Cleaner:
                    Cleaner.cleaner = player;
                    break;
                case RoleType.Warlock:
                    Warlock.warlock = player;
                    break;
                case RoleType.SecurityGuard:
                    SecurityGuard.securityGuard = player;
                    break;
                case RoleType.Arsonist:
                    Arsonist.arsonist = player;
                    break;
                case RoleType.EvilGuesser:
                    Guesser.evilGuesser = player;
                    break;
                case RoleType.NiceGuesser:
                    Guesser.niceGuesser = player;
                    break;
                case RoleType.BountyHunter:
                    BountyHunter.bountyHunter = player;
                    break;
                case RoleType.Bait:
                    Bait.bait = player;
                    break;
                case RoleType.GM:
                    GM.gm = player;
                    break;
                case RoleType.Vulture:
                    Vulture.vulture = player;
                    break;
                case RoleType.Medium:
                    Medium.medium = player;
                    break;
                case RoleType.Witch:
                    Witch.witch = player;
                    break;
                case RoleType.Lawyer:
                    Lawyer.lawyer = player;
                    break;
                case RoleType.Pursuer:
                    Pursuer.pursuer = player;
                    break;
                case RoleType.Portalmaker:
                    Portalmaker.portalmaker = player;
                    break;
                case RoleType.Assassin:
                    Assassin.assassin = player;
                    break;
                case RoleType.UnderTaker:
                    UnderTaker.underTaker = player;
                    break;
                default:
                    TheOtherRolesPlugin.Logger.LogError("setRole: no method found for role type {role}");
                    return;
            }
        }

        public static void eraseRole(this PlayerControl player, RoleType role)
        {
            if (isRole(player, role))
            {
                foreach (var t in RoleData.allRoleTypes)
                {
                    if (role == t.Key)
                    {
                        t.Value.GetMethod("eraseRole", BindingFlags.Public | BindingFlags.Static)?.Invoke(null, new object[] { player });
                        return;
                    }
                }
                TheOtherRolesPlugin.Logger.LogError("eraseRole: no method found for role type {role}");
            }
        }

        public static void eraseAllRoles(this PlayerControl player)
        {
            foreach (var t in RoleData.allRoleTypes)
            {
                t.Value.GetMethod("eraseRole", BindingFlags.Public | BindingFlags.Static)?.Invoke(null, new object[] { player });
            }

            // Crewmate roles
            if (player.isRole(RoleType.Engineer)) Engineer.clearAndReload();
            if (player.isRole(RoleType.Detective)) Detective.clearAndReload();
            if (player.isRole(RoleType.TimeMaster)) TimeMaster.clearAndReload();
            if (player.isRole(RoleType.Medic)) Medic.clearAndReload();
            if (player.isRole(RoleType.Shifter)) Shifter.clearAndReload();
            if (player.isRole(RoleType.Seer)) Seer.clearAndReload();
            if (player.isRole(RoleType.Hacker)) Hacker.clearAndReload();
            if (player.isRole(RoleType.Tracker)) Tracker.clearAndReload();
            if (player.isRole(RoleType.Snitch)) Snitch.clearAndReload();
            if (player.isRole(RoleType.Swapper)) Swapper.clearAndReload();
            if (player.isRole(RoleType.Spy)) Spy.clearAndReload();
            if (player.isRole(RoleType.SecurityGuard)) SecurityGuard.clearAndReload();
            if (player.isRole(RoleType.Bait)) Bait.clearAndReload();
            if (player.isRole(RoleType.Medium)) Medium.clearAndReload();
            if (player.isRole(RoleType.Portalmaker)) Portalmaker.clearAndReload();

            // Impostor roles
            if (player.isRole(RoleType.Morphling)) Morphling.clearAndReload();
            if (player.isRole(RoleType.Camouflager)) Camouflager.clearAndReload();
            if (player.isRole(RoleType.Godfather)) Godfather.clearAndReload();
            if (player.isRole(RoleType.Mafioso)) Mafioso.clearAndReload();
            if (player.isRole(RoleType.Janitor)) Janitor.clearAndReload();
            if (player.isRole(RoleType.Vampire)) Vampire.clearAndReload();
            if (player.isRole(RoleType.Eraser)) Eraser.clearAndReload();
            if (player.isRole(RoleType.Trickster)) Trickster.clearAndReload();
            if (player.isRole(RoleType.Cleaner)) Cleaner.clearAndReload();
            if (player.isRole(RoleType.Warlock)) Warlock.clearAndReload();
            if (player.isRole(RoleType.Witch)) Witch.clearAndReload();
            if (player.isRole(RoleType.EvilHacker)) EvilHacker.clearAndReload();
            if (player.isRole(RoleType.Assassin)) Assassin.clearAndReload();
            if (player.isRole(RoleType.UnderTaker)) UnderTaker.clearAndReload();

            // Other roles
            if (player.isRole(RoleType.Jester)) Jester.clearAndReload();
            if (player.isRole(RoleType.Arsonist)) Arsonist.clearAndReload();
            if (player.isRole(RoleType.Sidekick)) Sidekick.clearAndReload();
            if (player.isRole(RoleType.BountyHunter)) BountyHunter.clearAndReload();
            if (player.isRole(RoleType.Vulture)) Vulture.clearAndReload();
            if (player.isRole(RoleType.Lawyer)) Lawyer.clearAndReload();
            if (player.isRole(RoleType.Pursuer)) Pursuer.clearAndReload();
            if (Guesser.isGuesser(player.PlayerId)) Guesser.clear(player.PlayerId);


            if (player.isRole(RoleType.Jackal))
            { // Promote Sidekick and hence override the the Jackal or erase Jackal
                if (Sidekick.promotesToJackal && Sidekick.sidekick != null && Sidekick.sidekick.isAlive())
                {
                    RPCProcedure.sidekickPromotes();
                }
                else
                {
                    Jackal.clearAndReload();
                }
            }
        }

        public static void swapRoles(this PlayerControl player, PlayerControl target)
        {
            foreach (var t in RoleData.allRoleTypes)
            {
                if (player.isRole(t.Key))
                {
                    t.Value.GetMethod("swapRole", BindingFlags.Public | BindingFlags.Static)?.Invoke(null, new object[] { player, target });
                }
            }

            if (player.isRole(RoleType.Mayor)) Mayor.mayor = target;
            if (player.isRole(RoleType.Engineer)) Engineer.engineer = target;
            if (player.isRole(RoleType.Detective)) Detective.detective = target;
            if (player.isRole(RoleType.TimeMaster)) TimeMaster.timeMaster = target;
            if (player.isRole(RoleType.Medic)) Medic.medic = target;
            if (player.isRole(RoleType.Swapper)) Swapper.swapper = target;
            if (player.isRole(RoleType.Seer)) Seer.seer = target;
            if (player.isRole(RoleType.Hacker)) Hacker.hacker = target;
            if (player.isRole(RoleType.Tracker)) Tracker.tracker = target;
            if (player.isRole(RoleType.Snitch)) Snitch.snitch = target;
            if (player.isRole(RoleType.Spy)) Spy.spy = target;
            if (player.isRole(RoleType.SecurityGuard)) SecurityGuard.securityGuard = target;
            if (player.isRole(RoleType.Bait))
            {
                Bait.bait = target;
                if (Bait.bait.Data.IsDead) Bait.reported = true;
            }
            if (player.isRole(RoleType.Medium)) Medium.medium = target;
            if (player.isRole(RoleType.Godfather)) Godfather.godfather = target;
            if (player.isRole(RoleType.Mafioso)) Mafioso.mafioso = target;
            if (player.isRole(RoleType.Janitor)) Janitor.janitor = target;
            if (player.isRole(RoleType.Morphling)) Morphling.morphling = target;
            if (player.isRole(RoleType.Camouflager)) Camouflager.camouflager = target;
            if (player.isRole(RoleType.Vampire)) Vampire.vampire = target;
            if (player.isRole(RoleType.Eraser)) Eraser.eraser = target;
            if (player.isRole(RoleType.Trickster)) Trickster.trickster = target;
            if (player.isRole(RoleType.Cleaner)) Cleaner.cleaner = target;
            if (player.isRole(RoleType.Warlock)) Warlock.warlock = target;
            if (player.isRole(RoleType.BountyHunter)) BountyHunter.bountyHunter = target;
            if (player.isRole(RoleType.Witch)) Witch.witch = target;
            if (player.isRole(RoleType.EvilHacker)) EvilHacker.evilHacker = target;
            if (player.isRole(RoleType.EvilGuesser)) Guesser.evilGuesser = target;
            if (player.isRole(RoleType.NiceGuesser)) Guesser.niceGuesser = target;
            if (player.isRole(RoleType.Jester)) Jester.jester = target;
            if (player.isRole(RoleType.Arsonist)) Arsonist.arsonist = target;
            if (player.isRole(RoleType.Jackal)) Jackal.jackal = target;
            if (player.isRole(RoleType.Sidekick)) Sidekick.sidekick = target;
            if (player.isRole(RoleType.Vulture)) Vulture.vulture = target;
            if (player.isRole(RoleType.Lawyer)) Lawyer.lawyer = target;
            if (player.isRole(RoleType.Pursuer)) Pursuer.pursuer = target;
            if (player.isRole(RoleType.UnderTaker)) UnderTaker.underTaker = target;
        }

        public static void OnKill(this PlayerControl player, PlayerControl target)
        {
            Role.allRoles.DoIf(x => x.player == player, x => x.OnKill(target));
            Modifier.allModifiers.DoIf(x => x.player == player, x => x.OnKill(target));
        }

        public static void OnDeath(this PlayerControl player, PlayerControl killer)
        {
            Role.allRoles.DoIf(x => x.player == player, x => x.OnDeath(killer));
            Modifier.allModifiers.DoIf(x => x.player == player, x => x.OnDeath(killer));

            // Lover suicide trigger on exile/death
            if (player.isLovers())
                Lovers.killLovers(player, killer);

            RPCProcedure.updateMeeting(player.PlayerId, true);
        }
    }
}