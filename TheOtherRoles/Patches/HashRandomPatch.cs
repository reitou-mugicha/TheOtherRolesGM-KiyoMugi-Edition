// 参考元
// https://github.com/yukieiji/ExtremeRoles (yukieijiさん制作 ExtremeRoles)
// https://github.com/haoming37/TheOtherRoles-GM-Haoming (haomingさん制作 TheOtherRoles GM HaomingEdition)

using System;
using HarmonyLib;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(HashRandom), nameof(HashRandom.FastNext))]
    public static class HashRandomFastNextPatch
    {
        public static bool Prefix(ref int __result, [HarmonyArgument(0)] int maxInt)
        {
            if (RandomGeneratorPatch.improvementRandom)
            {
                __result = RandomGeneratorPatch.Instance.Next(maxInt);
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(HashRandom), nameof(HashRandom.Next), new Type[] { typeof(int) })]
    public static class HashRandomNextPatch
    {
        public static bool Prefix(ref int __result, [HarmonyArgument(0)] int maxInt)
        {
            if (RandomGeneratorPatch.improvementRandom)
            {
                __result = RandomGeneratorPatch.Instance.Next(maxInt);
                return false;
            }
            return true;
        }
    }
}
