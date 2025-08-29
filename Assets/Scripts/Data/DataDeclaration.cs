using UnityEngine;

namespace DataDeclaration
{
    public static class Constants
    {
        public const float MOUSE_SENSITIVITY = 3f;
        public const float MAX_MOUSE_ROT_Y = 60f;
        public const float MIN_MOUSE_ROT_Y = -60f;

        public const float MAX_STAT_VALUE = 100f;
        public const float MIN_STAT_VALUE = 0f;
    }

    public enum BodyPartType
    {
        Head,
        Body,
    }

    public enum BulletImpactType
    {
        Metal,
        Wood,
    }

    public struct DamageInfo
    {
        public float Damage { get; }
        public BodyPartType HitPart { get; }
        public Vector3 HitPoint { get; }
        public Vector3 HitDirection { get; }

        public DamageInfo(float damage, BodyPartType bodyPartType, Vector3 hitPoint, Vector3 hitDirection)
        {
            Damage = damage;
            HitPart = bodyPartType;
            HitPoint = hitPoint;
            HitDirection = hitDirection;
        }
    }
}