using System.Collections.Generic;
using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    public class TransformDataParaffin : TransformDataBase
    {
        public static TransformDataParaffin defaultTrans = new TransformDataParaffin();

        public override TransformType type => TransformType.Paraffin;

        public override int valueCount => 24;

        public override bool hasColor => true;
        public override bool hasSubColor => true;
        public override bool hasVisible => true;
        public override bool hasEasing => true;

        public override ValueData[] colorValues
        {
            get => new ValueData[] { values[0], values[1], values[2], values[3] };
        }

        public override ValueData[] subColorValues
        {
            get => new ValueData[] { values[4], values[5], values[6], values[7] };
        }

        public override ValueData visibleValue => values[8];

        public override ValueData easingValue => values[9];

        public override Color initialColor => new Color(1f, 1f, 1f, 1f);

        public override Color initialSubColor => new Color(1f, 1f, 1f, 0f);

        public TransformDataParaffin()
        {
        }

        public override void Initialize(string name)
        {
            base.Initialize(name);
            index = PostEffectUtils.GetEffectIndex(name);
        }

        private readonly static Dictionary<string, CustomValueInfo> CustomValueInfoMap = new Dictionary<string, CustomValueInfo>
        {
            {
                "centerPositionX", new CustomValueInfo
                {
                    index = 10,
                    name = "X",
                    min = -1f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 0.5f,
                }
            },
            {
                "centerPositionY", new CustomValueInfo
                {
                    index = 11,
                    name = "Y",
                    min = -1f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 0.5f,
                }
            },
            {
                "radiusFar", new CustomValueInfo
                {
                    index = 12,
                    name = "外半径",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = 1f,
                }
            },
            {
                "radiusNear", new CustomValueInfo
                {
                    index = 13,
                    name = "内半径",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = 1f,
                }
            },
            {
                "radiusScaleX", new CustomValueInfo
                {
                    index = 14,
                    name = "SX",
                    min = 0f,
                    max = 5f,
                    step = 0.01f,
                    defaultValue = 1f,
                }
            },
            {
                "radiusScaleY", new CustomValueInfo
                {
                    index = 15,
                    name = "SY",
                    min = 0f,
                    max = 5f,
                    step = 0.01f,
                    defaultValue = 1f,
                }
            },
            {
                "useNormal", new CustomValueInfo
                {
                    index = 16,
                    name = "通常",
                    min = 0f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 0f,
                }
            },
            {
                "useAdd", new CustomValueInfo
                {
                    index = 17,
                    name = "加算",
                    min = 0f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 1f,
                }
            },
            {
                "useMultiply", new CustomValueInfo
                {
                    index = 18,
                    name = "乗算",
                    min = 0f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 0f,
                }
            },
            {
                "useOverlay", new CustomValueInfo
                {
                    index = 19,
                    name = "オーバーレイ",
                    min = 0f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 0f,
                }
            },
            {
                "useSubstruct", new CustomValueInfo
                {
                    index = 20,
                    name = "減算",
                    min = 0f,
                    max = 2f,
                    step = 0.01f,
                    defaultValue = 0f,
                }
            },
            {
                "depthMin", new CustomValueInfo
                {
                    index = 21,
                    name = "最小深度",
                    min = 0f,
                    max = 100f,
                    step = 0.1f,
                    defaultValue = 0f,
                }
            },
            {
                "depthMax", new CustomValueInfo
                {
                    index = 22,
                    name = "最大深度",
                    min = 0f,
                    max = 100f,
                    step = 0.1f,
                    defaultValue = 0f,
                }
            },
            {
                "depthFade", new CustomValueInfo
                {
                    index = 23,
                    name = "深度幅",
                    min = 0f,
                    max = 10f,
                    step = 0.01f,
                    defaultValue = 0f,
                }
            }
        };

        public override Dictionary<string, CustomValueInfo> GetCustomValueInfoMap()
        {
            return CustomValueInfoMap;
        }

        public ValueData[] centerPositionValues
        {
            get => new ValueData[] { this["centerPositionX"], this["centerPositionY"] };
        }
        public ValueData radiusFarValue => this["radiusFar"];
        public ValueData radiusNearValue => this["radiusNear"];
        public ValueData[] radiusScaleValues
        {
            get => new ValueData[] { this["radiusScaleX"], this["radiusScaleY"] };
        }
        public ValueData useNormalValue => this["useNormal"];
        public ValueData useAddValue => this["useAdd"];
        public ValueData useMultiplyValue => this["useMultiply"];
        public ValueData useOverlayValue => this["useOverlay"];
        public ValueData useSubstructValue => this["useSubstruct"];
        public ValueData depthMinValue => this["depthMin"];
        public ValueData depthMaxValue => this["depthMax"];
        public ValueData depthFadeValue => this["depthFade"];

        public CustomValueInfo centerPositionXInfo => CustomValueInfoMap["centerPositionX"];
        public CustomValueInfo centerPositionYInfo => CustomValueInfoMap["centerPositionY"];
        public CustomValueInfo radiusFarInfo => CustomValueInfoMap["radiusFar"];
        public CustomValueInfo radiusNearInfo => CustomValueInfoMap["radiusNear"];
        public CustomValueInfo radiusScaleXInfo => CustomValueInfoMap["radiusScaleX"];
        public CustomValueInfo radiusScaleYInfo => CustomValueInfoMap["radiusScaleY"];
        public CustomValueInfo useNormalInfo => CustomValueInfoMap["useNormal"];
        public CustomValueInfo useAddInfo => CustomValueInfoMap["useAdd"];
        public CustomValueInfo useMultiplyInfo => CustomValueInfoMap["useMultiply"];
        public CustomValueInfo useOverlayInfo => CustomValueInfoMap["useOverlay"];
        public CustomValueInfo useSubstructInfo => CustomValueInfoMap["useSubstruct"];
        public CustomValueInfo depthMinInfo => CustomValueInfoMap["depthMin"];
        public CustomValueInfo depthMaxInfo => CustomValueInfoMap["depthMax"];
        public CustomValueInfo depthFadeInfo => CustomValueInfoMap["depthFade"];

        public Vector2 centerPosition
        {
            get => centerPositionValues.ToVector2();
            set => centerPositionValues.FromVector2(value);
        }

        public float radiusFar
        {
            get => radiusFarValue.value;
            set => radiusFarValue.value = value;
        }

        public float radiusNear
        {
            get => radiusNearValue.value;
            set => radiusNearValue.value = value;
        }

        public Vector2 radiusScale
        {
            get => radiusScaleValues.ToVector2();
            set => radiusScaleValues.FromVector2(value);
        }

        public float useNormal
        {
            get => useNormalValue.value;
            set => useNormalValue.value = value;
        }

        public float useAdd
        {
            get => useAddValue.value;
            set => useAddValue.value = value;
        }   

        public float useMultiply
        {
            get => useMultiplyValue.value;
            set => useMultiplyValue.value = value;
        }

        public float useOverlay
        {
            get => useOverlayValue.value;
            set => useOverlayValue.value = value;
        }

        public float useSubstruct
        {
            get => useSubstructValue.value;
            set => useSubstructValue.value = value;
        }

        public float depthMin
        {
            get => depthMinValue.value;
            set => depthMinValue.value = value;
        }

        public float depthMax
        {
            get => depthMaxValue.value;
            set => depthMaxValue.value = value;
        }

        public float depthFade
        {
            get => depthFadeValue.value;
            set => depthFadeValue.value = value;
        }

        public ColorParaffinData paraffin
        {
            get => new ColorParaffinData
            {
                enabled = visible,
                color1 = color,
                color2 = subColor,
                centerPosition = centerPosition,
                radiusFar = radiusFar,
                radiusNear = radiusNear,
                radiusScale = radiusScale,
                depthMin = depthMin,
                depthMax = depthMax,
                depthFade = depthFade,
                useNormal = useNormal,
                useAdd = useAdd,
                useMultiply = useMultiply,
                useOverlay = useOverlay,
                useSubstruct = useSubstruct,
            };
            set
            {
                visible = value.enabled;
                color = value.color1;
                subColor = value.color2;
                centerPosition = value.centerPosition;
                radiusFar = value.radiusFar;
                radiusNear = value.radiusNear;
                radiusScale = value.radiusScale;
                depthMin = value.depthMin;
                depthMax = value.depthMax;
                depthFade = value.depthFade;
                useNormal = value.useNormal;
                useAdd = value.useAdd;
                useMultiply = value.useMultiply;
                useOverlay = value.useOverlay;
                useSubstruct = value.useSubstruct;
            }
        }

        public int index;
    }
}