using System;
using TheOtherRoles.Utilities;

namespace TheOtherRoles
{
    public static class MorphHandler
    {
        public static void morphToPlayer(this PlayerControl pc, PlayerControl target)
        {
            setOutfit(pc, target.Data.DefaultOutfit, target.Visible);
        }
        public static void setOutfit(this PlayerControl pc, GameData.PlayerOutfit outfit, bool visible = true)
        {
            pc.Data.Outfits[PlayerOutfitType.Shapeshifted] = outfit;
            pc.CurrentOutfitType = PlayerOutfitType.Shapeshifted;
            pc.RawSetName(outfit.PlayerName);
            pc.RawSetHat(outfit.HatId, outfit.ColorId);
            pc.RawSetVisor(outfit.VisorId, outfit.ColorId);
            pc.RawSetColor(outfit.ColorId);
            pc.RawSetPet(outfit.PetId, outfit.ColorId);
            Helpers.setSkinWithAnim(pc.MyPhysics, outfit.SkinId, outfit.ColorId);

            if (pc.cosmetics.currentPet != null)
            {
                pc.cosmetics.currentPet.enabled = false;
                pc.cosmetics.currentPet.Visible = false;
                UnityEngine.GameObject.Destroy(pc.cosmetics.currentPet);
            }

            FastDestroyableSingleton<HatManager>.Instance.StartCoroutine(
                FastDestroyableSingleton<HatManager>.Instance.GetPetById(outfit.PetId).CoLoadViewData(new Action<PetBehaviour>((data) =>
                {
                    if (!pc.Data.IsDead)
                    {
                        pc.cosmetics.currentPet = UnityEngine.GameObject.Instantiate(data);
                        pc.cosmetics.currentPet.transform.position = pc.transform.position;
                        pc.cosmetics.currentPet.Source = pc;
                        pc.cosmetics.currentPet.Visible = visible;
                        pc.SetPlayerMaterialColors(pc.cosmetics.currentPet.rend);
                    }
                }))
            );
        }

        public static void resetMorph(this PlayerControl pc)
        {
            morphToPlayer(pc, pc);
            pc.CurrentOutfitType = PlayerOutfitType.Default;
        }
    }
}