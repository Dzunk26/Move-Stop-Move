using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache {
    private static Dictionary<AnimState, string> dictAnimName = new Dictionary<AnimState, string>();

    private static Dictionary<Collider, Character> dictCharacter = new Dictionary<Collider, Character>();
    private static Dictionary<Collider, Weapon> dictWeapon = new Dictionary<Collider, Weapon>();
    private static Dictionary<Collider, BaseProjectile> dictProjectile = new Dictionary<Collider, BaseProjectile>();

    public static string GetAnimName(AnimState animState) {
        if (!dictAnimName.ContainsKey(animState)) {
            dictAnimName[animState] = animState.ToString();
        }

        return dictAnimName[animState];
    }

    public static Character GetCharacter(Collider collider) {
        if (!dictCharacter.ContainsKey(collider)) {
            Character character = collider.GetComponent<Character>();
            dictCharacter[collider] = character;
        }

        return dictCharacter[collider];
    }

    public static Weapon GetWeapon(Collider collider) {
        if (!dictWeapon.ContainsKey(collider)) {
            Weapon weapon = collider.GetComponent<Weapon>();
            dictWeapon[collider] = weapon;
        }

        return dictWeapon[collider];
    }

    public static BaseProjectile GetProjectile(Collider collider) {
        if (!dictProjectile.ContainsKey(collider)) {
            BaseProjectile projectile = collider.GetComponent<BaseProjectile>();
            dictProjectile[collider] = projectile;
        }

        return dictProjectile[collider];
    }

    public static void Reset() {
        dictAnimName.Clear();
        dictCharacter.Clear();
        dictWeapon.Clear();
        dictProjectile.Clear();
    }
}