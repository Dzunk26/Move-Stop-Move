using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant {
    public static HatType DEAFAULT_HAT_TYPE = HatType.None;
    public static WeaponType DEAFAULT_WEAPON_TYPE = WeaponType.Hammer;
    public static AccessoryType DEAFAULT_ACCESSORY_TYPE = AccessoryType.None;
    public static PantType DEAFAULT_PANT_TYPE = PantType.None;

    public static float ATTACK_RANG_CONVERTER = 0.1f;

    public static string PLAYER_DATA = "Player Data";

    public static string PROJECTILE_TAG = "Projectile";
    public static string CHARACTER_TAG = "Character";
    public static string OBSTACLE_TAG = "Obstacle";
    public static string WALL_TAG = "Wall";

    public static string NONEQUIP_TEXT = "Equip";
    public static string EQUIP_TEXT = "Equipped";
}
