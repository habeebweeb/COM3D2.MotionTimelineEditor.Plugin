using System.Reflection;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    public static class Extensions
    {
        public static void ResizeTexture(
            this Texture2D sourceTexture,
            int targetWidth,
            int targetHeight)
        {
            float sourceAspect = (float)sourceTexture.width / sourceTexture.height;
            float targetAspect = (float)targetWidth / targetHeight;

            int width, height;
            if (sourceAspect > targetAspect)
            {
                width = targetWidth;
                height = (int)(width / sourceAspect);
            }
            else
            {
                height = targetHeight;
                width = (int)(height * sourceAspect);
            }
            TextureScale.Bilinear(sourceTexture, width, height);
        }

        public static Texture2D ResizeAndCropTexture(
            this Texture2D sourceTexture,
            int targetWidth,
            int targetHeight)
        {
            float sourceAspect = (float)sourceTexture.width / sourceTexture.height;
            float targetAspect = (float)targetWidth / targetHeight;

            int width, height;
            if (sourceAspect > targetAspect)
            {
                height = targetHeight;
                width = (int)(sourceTexture.width * ((float)targetHeight / sourceTexture.height));
            }
            else
            {
                width = targetWidth;
                height = (int)(sourceTexture.height * ((float)targetWidth / sourceTexture.width));
            }
            TextureScale.Bilinear(sourceTexture, width, height);

            var pixels = new Color[targetWidth * targetHeight];

            int x = (width - targetWidth) / 2;
            int y = (height - targetHeight) / 2;

            for (int i = 0; i < targetHeight; i++)
            {
                for (int j = 0; j < targetWidth; j++)
                {
                    pixels[i * targetWidth + j] = sourceTexture.GetPixel(x + j, y + i);
                }
            }

            var resultTexture = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);
            resultTexture.SetPixels(pixels);
            resultTexture.Apply();

            return resultTexture;
        }

        private static FieldInfo fieldPathDic = null;

        public static Dictionary<string, CacheBoneDataArray.BoneData> GetPathDic(
            this CacheBoneDataArray cacheBoneDataArray)
        {
            if (fieldPathDic == null)
            {
                fieldPathDic = typeof(CacheBoneDataArray).GetField("path_dic_", BindingFlags.Instance | BindingFlags.NonPublic);
                PluginUtils.AssertNull(fieldPathDic != null, "fieldPathDic is null");
            }
            return (Dictionary<string, CacheBoneDataArray.BoneData>) fieldPathDic.GetValue(cacheBoneDataArray);
        }

        public static CacheBoneDataArray.BoneData GetBoneData(
            this CacheBoneDataArray cacheBoneDataArray,
            string path)
        {
            var pathDic = cacheBoneDataArray.GetPathDic();
            CacheBoneDataArray.BoneData boneData;
            if (pathDic.TryGetValue(path, out boneData))
            {
                return boneData;
            }
            return null;
        }

        public static CacheBoneDataArray.BoneData GetBoneData(
            this CacheBoneDataArray cacheBoneDataArray,
            IKManager.BoneType boneType)
        {
            return cacheBoneDataArray.GetBoneData(BoneUtils.GetBonePath(boneType));
        }

        private static FieldInfo fieldIkFabrik = null;

        public static FABRIK GetIkFabrik(this LimbControl limbControl)
        {
            if (limbControl == null)
            {
                return null;
            }
            if (fieldIkFabrik == null)
            {
                fieldIkFabrik = typeof(LimbControl).GetField("ik_fabrik_", BindingFlags.NonPublic | BindingFlags.Instance);
                PluginUtils.AssertNull(fieldIkFabrik != null, "fieldIkFabrik is null");
            }
            return (FABRIK) fieldIkFabrik.GetValue(limbControl);
        }

        private static FieldInfo fieldJointDragPoint = null;

        public static IKDragPoint GetJointDragPoint(
            this LimbControl limbControl)
        {
            if (fieldJointDragPoint == null)
            {
                fieldJointDragPoint = typeof(LimbControl).GetField("joint_drag_point_", BindingFlags.NonPublic | BindingFlags.Instance);
                PluginUtils.AssertNull(fieldJointDragPoint != null, "fieldJointDragPoint is null");
            }
            return (IKDragPoint) fieldJointDragPoint.GetValue(limbControl);
        }

        private static FieldInfo fieldTipDragPoint = null;

        public static IKDragPoint GetTipDragPoint(
            this LimbControl limbControl)
        {
            if (fieldTipDragPoint == null)
            {
                fieldTipDragPoint = typeof(LimbControl).GetField("tip_drag_point_", BindingFlags.NonPublic | BindingFlags.Instance);
                PluginUtils.AssertNull(fieldTipDragPoint != null, "fieldTipDragPoint is null");
            }
            return (IKDragPoint) fieldTipDragPoint.GetValue(limbControl);
        }

        private static FieldInfo fieldBackupLocalPos = null;

        public static Vector3 GetBackupLocalPos(
            this IKDragPoint ikDragPoint)
        {
            if (fieldBackupLocalPos == null)
            {
                fieldBackupLocalPos = typeof(IKDragPoint).GetField("backup_local_pos_", BindingFlags.NonPublic | BindingFlags.Instance);
                PluginUtils.AssertNull(fieldBackupLocalPos != null, "fieldBackupLocalPos is null");
            }
            return (Vector3) fieldBackupLocalPos.GetValue(ikDragPoint);
        }

        public static void PositonCorrection(this IKDragPoint ikDragPoint)
        {
            if (ikDragPoint.PositonCorrectionEnabled)
            {
                ikDragPoint.target_ik_point_trans.localPosition = ikDragPoint.GetBackupLocalPos();
            }
        }

        public static T GetCustomAttribute<T>(
            this System.Type type)
            where T : System.Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                return (T) attributes[0];
            }
            return default(T);
        }

        public static Vector3 ToVector3(this ValueData[] values)
        {
            if (values.Length != 3)
            {
                PluginUtils.LogError("ToVector3: 不正なValueData配列です length={0}", values.Length);
                return Vector3.zero;
            }
            return new Vector3(values[0].value, values[1].value, values[2].value);
        }

        public static void FromVector3(this ValueData[] values, Vector3 vector)
        {
            if (values.Length != 3)
            {
                PluginUtils.LogError("FromVector3: 不正なValueData配列です length={0}", values.Length);
                return;
            }
            values[0].value = vector.x;
            values[1].value = vector.y;
            values[2].value = vector.z;
        }

        public static Quaternion ToQuaternion(this ValueData[] values)
        {
            if (values.Length != 4)
            {
                PluginUtils.LogError("ToQuaternion: 不正なValueData配列です length={0}", values.Length);
                return Quaternion.identity;
            }
            return new Quaternion(values[0].value, values[1].value, values[2].value, values[3].value);
        }

        public static void FromQuaternion(this ValueData[] values, Quaternion quaternion)
        {
            if (values.Length != 4)
            {
                PluginUtils.LogError("FromQuaternion: 不正なValueData配列です length={0}", values.Length);
                return;
            }
            values[0].value = quaternion.x;
            values[1].value = quaternion.y;
            values[2].value = quaternion.z;
            values[3].value = quaternion.w;
        }

        private static FieldInfo _targetListField = null;

        public static List<PhotoTransTargetObject> GetTargetList(this ObjectManagerWindow self)
        {
            if (_targetListField == null)
            {
                _targetListField = typeof(ObjectManagerWindow).GetField("target_list_",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            }

            return (List<PhotoTransTargetObject>) _targetListField.GetValue(self);
        }

        public static bool IsLock(this FingerBlend.BaseFinger baseFinger, int index)
        {
            var armFinger = baseFinger as FingerBlend.ArmFinger;
            if (armFinger != null)
            {
                switch (index)
                {
                    case 0:
                        return armFinger.lock_enabled0;
                    case 1:
                        return armFinger.lock_enabled1;
                    case 2:
                        return armFinger.lock_enabled2;
                    case 3:
                        return armFinger.lock_enabled3;
                    case 4:
                        return armFinger.lock_enabled4;
                }
            }

            var legFinger = baseFinger as FingerBlend.LegFinger;
            if (legFinger != null)
            {
                switch (index)
                {
                    case 0:
                        return legFinger.lock_enabled0;
                    case 1:
                        return legFinger.lock_enabled1;
                    case 2:
                        return legFinger.lock_enabled2;
                }
            }

            return false;
        }

        public static void CopyFrom(
            this FingerBlend.BaseFinger baseFinger,
            FingerBlend.BaseFinger source)
        {
            baseFinger.enabled = source.enabled;

            var armFinger = baseFinger as FingerBlend.ArmFinger;
            var sourceArmFinger = source as FingerBlend.ArmFinger;
            if (armFinger != null && sourceArmFinger != null)
            {
                armFinger.lock_enabled0 = sourceArmFinger.lock_enabled0;
                armFinger.lock_enabled1 = sourceArmFinger.lock_enabled1;
                armFinger.lock_enabled2 = sourceArmFinger.lock_enabled2;
                armFinger.lock_enabled3 = sourceArmFinger.lock_enabled3;
                armFinger.lock_enabled4 = sourceArmFinger.lock_enabled4;

                armFinger.lock_value0 = sourceArmFinger.lock_value0;
                armFinger.lock_value1 = sourceArmFinger.lock_value1;
                armFinger.lock_value2 = sourceArmFinger.lock_value2;
                armFinger.lock_value3 = sourceArmFinger.lock_value3;
                armFinger.lock_value4 = sourceArmFinger.lock_value4;
            }

            var legFinger = baseFinger as FingerBlend.LegFinger;
            var sourceLegFinger = source as FingerBlend.LegFinger;
            if (legFinger != null && sourceLegFinger != null)
            {
                legFinger.lock_enabled0 = sourceLegFinger.lock_enabled0;
                legFinger.lock_enabled1 = sourceLegFinger.lock_enabled1;
                legFinger.lock_enabled2 = sourceLegFinger.lock_enabled2;

                legFinger.lock_value0 = sourceLegFinger.lock_value0;
                legFinger.lock_value1 = sourceLegFinger.lock_value1;
                legFinger.lock_value2 = sourceLegFinger.lock_value2;
            }

            baseFinger.value_open = source.value_open;
            baseFinger.value_fist = source.value_fist;
        }
    }

}