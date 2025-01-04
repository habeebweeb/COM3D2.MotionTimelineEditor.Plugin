using System.Collections.Generic;
using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    public class TransformDataPsylliumArea : TransformDataBase
    {
        public static TransformDataPsylliumArea defaultTrans = new TransformDataPsylliumArea();
        public static PsylliumAreaConfig defaultConfig = new PsylliumAreaConfig();

        public override TransformType type => TransformType.PsylliumArea;

        public override int valueCount => 30;

        public override bool hasPosition => true;
        public override bool hasEulerAngles => true;

        public override ValueData[] positionValues
        {
            get => new ValueData[] { values[0], values[1], values[2] };
        }

        public override ValueData[] eulerAnglesValues
        {
            get => new ValueData[] { values[3], values[4], values[5] };
        }

        public override Vector3 initialPosition => defaultConfig.position;
        public override Vector3 initialEulerAngles => defaultConfig.rotation;

        public TransformDataPsylliumArea()
        {
        }

        private readonly static Dictionary<string, CustomValueInfo> CustomValueInfoMap = new Dictionary<string, CustomValueInfo>
        {
            {
                "sizeX", new CustomValueInfo
                {
                    index = 6,
                    name = "SX",
                    min = 0f,
                    max = 100f,
                    step = 0.1f,
                    defaultValue = defaultConfig.size.x,
                }
            },
            {
                "sizeY", new CustomValueInfo
                {
                    index = 7,
                    name = "SY",
                    min = 0f,
                    max = 100f,
                    step = 0.1f,
                    defaultValue = defaultConfig.size.y,
                }
            },
            {
                "seatDistanceX", new CustomValueInfo
                {
                    index = 8,
                    name = "席幅",
                    min = 0f,
                    max = 10f,
                    step = 0.01f,
                    defaultValue = defaultConfig.seatDistance.x,
                }
            },
            {
                "seatDistanceY", new CustomValueInfo
                {
                    index = 9,
                    name = "列幅",
                    min = 0f,
                    max = 10f,
                    step = 0.01f,
                    defaultValue = defaultConfig.seatDistance.y,
                }
            },
            {
                "randomPositionRangeX", new CustomValueInfo
                {
                    index = 10,
                    name = "X Random",
                    min = 0f,
                    max = 10f,
                    step = 0.01f,
                    defaultValue = defaultConfig.randomPositionRange.x,
                }
            },
            {
                "randomPositionRangeY", new CustomValueInfo
                {
                    index = 11,
                    name = "Y Random",
                    min = 0f,
                    max = 10f,
                    step = 0.01f,
                    defaultValue = defaultConfig.randomPositionRange.y,
                }
            },
            {
                "randomPositionRangeZ", new CustomValueInfo
                {
                    index = 12,
                    name = "Z Random",
                    min = 0f,
                    max = 10f,
                    step = 0.01f,
                    defaultValue = defaultConfig.randomPositionRange.z,
                }
            },
            {
                "barCountWeight0", new CustomValueInfo
                {
                    index = 13,
                    name = "0個",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.barCountWeight0,
                }
            },
            {
                "barCountWeight1", new CustomValueInfo
                {
                    index = 14,
                    name = "1個",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.barCountWeight1,
                }
            },
            {
                "barCountWeight2", new CustomValueInfo
                {
                    index = 15,
                    name = "2個",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.barCountWeight2,
                }
            },
            {
                "barCountWeight3", new CustomValueInfo
                {
                    index = 16,
                    name = "3個",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.barCountWeight3,
                }
            },
            {
                "colorWeight1", new CustomValueInfo
                {
                    index = 17,
                    name = "色1",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.colorWeight1,
                }
            },
            {
                "colorWeight2", new CustomValueInfo
                {
                    index = 18,
                    name = "色2",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.colorWeight2,
                }
            },
            {
                "patternWeight0", new CustomValueInfo
                {
                    index = 19,
                    name = "P0",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight0,
                }
            },
            {
                "patternWeight1", new CustomValueInfo
                {
                    index = 20,
                    name = "P1",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight1,
                }
            },
            {
                "patternWeight2", new CustomValueInfo
                {
                    index = 21,
                    name = "P2",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight2,
                }
            },
            {
                "patternWeight3", new CustomValueInfo
                {
                    index = 22,
                    name = "P3",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight3,
                }
            },
            {
                "patternWeight4", new CustomValueInfo
                {
                    index = 23,
                    name = "P4",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight4,
                }
            },
            {
                "patternWeight5", new CustomValueInfo
                {
                    index = 24,
                    name = "P5",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight5,
                }
            },
            {
                "patternWeight6", new CustomValueInfo
                {
                    index = 25,
                    name = "P6",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight6,
                }
            },
            {
                "patternWeight7", new CustomValueInfo
                {
                    index = 26,
                    name = "P7",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight7,
                }
            },
            {
                "patternWeight8", new CustomValueInfo
                {
                    index = 27,
                    name = "P8",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight8,
                }
            },
            {
                "patternWeight9", new CustomValueInfo
                {
                    index = 28,
                    name = "P9",
                    min = 0f,
                    max = 1f,
                    step = 0.01f,
                    defaultValue = defaultConfig.patternWeight9,
                }
            },
            {
                "randomSeed", new CustomValueInfo
                {
                    index = 29,
                    name = "乱数Seed",
                    min = 0f,
                    max = int.MaxValue,
                    step = 1f,
                    defaultValue = defaultConfig.randomSeed,
                }
            },
        };

        public override Dictionary<string, CustomValueInfo> GetCustomValueInfoMap()
        {
            return CustomValueInfoMap;
        }

        public ValueData[] sizeValues
        {
            get => new ValueData[] { this["sizeX"], this["sizeY"] };
        }
        public ValueData[] seatDistanceValues
        {
            get => new ValueData[] { this["seatDistanceX"], this["seatDistanceY"] };
        }
        public ValueData[] randomPositionRangeValues
        {
            get => new ValueData[] { this["randomPositionRangeX"], this["randomPositionRangeY"], this["randomPositionRangeZ"] };
        }
        public ValueData barCountWeight0Value => this["barCountWeight0"];
        public ValueData barCountWeight1Value => this["barCountWeight1"];
        public ValueData barCountWeight2Value => this["barCountWeight2"];
        public ValueData barCountWeight3Value => this["barCountWeight3"];
        public ValueData colorWeight1Value => this["colorWeight1"];
        public ValueData colorWeight2Value => this["colorWeight2"];
        public ValueData patternWeight0Value => this["patternWeight0"];
        public ValueData patternWeight1Value => this["patternWeight1"];
        public ValueData patternWeight2Value => this["patternWeight2"];
        public ValueData patternWeight3Value => this["patternWeight3"];
        public ValueData patternWeight4Value => this["patternWeight4"];
        public ValueData patternWeight5Value => this["patternWeight5"];
        public ValueData patternWeight6Value => this["patternWeight6"];
        public ValueData patternWeight7Value => this["patternWeight7"];
        public ValueData patternWeight8Value => this["patternWeight8"];
        public ValueData patternWeight9Value => this["patternWeight9"];
        public ValueData randomSeedValue => this["randomSeed"];

        public CustomValueInfo sizeXInfo => CustomValueInfoMap["sizeX"];
        public CustomValueInfo sizeYInfo => CustomValueInfoMap["sizeY"];
        public CustomValueInfo seatDistanceXInfo => CustomValueInfoMap["seatDistanceX"];
        public CustomValueInfo seatDistanceYInfo => CustomValueInfoMap["seatDistanceY"];
        public CustomValueInfo randomPositionRangeXInfo => CustomValueInfoMap["randomPositionRangeX"];
        public CustomValueInfo randomPositionRangeYInfo => CustomValueInfoMap["randomPositionRangeY"];
        public CustomValueInfo randomPositionRangeZInfo => CustomValueInfoMap["randomPositionRangeZ"];
        public CustomValueInfo barCountWeight0Info => CustomValueInfoMap["barCountWeight0"];
        public CustomValueInfo barCountWeight1Info => CustomValueInfoMap["barCountWeight1"];
        public CustomValueInfo barCountWeight2Info => CustomValueInfoMap["barCountWeight2"];
        public CustomValueInfo barCountWeight3Info => CustomValueInfoMap["barCountWeight3"];
        public CustomValueInfo colorWeight1Info => CustomValueInfoMap["colorWeight1"];
        public CustomValueInfo colorWeight2Info => CustomValueInfoMap["colorWeight2"];
        public CustomValueInfo patternWeight0Info => CustomValueInfoMap["patternWeight0"];
        public CustomValueInfo patternWeight1Info => CustomValueInfoMap["patternWeight1"];
        public CustomValueInfo patternWeight2Info => CustomValueInfoMap["patternWeight2"];
        public CustomValueInfo patternWeight3Info => CustomValueInfoMap["patternWeight3"];
        public CustomValueInfo patternWeight4Info => CustomValueInfoMap["patternWeight4"];
        public CustomValueInfo patternWeight5Info => CustomValueInfoMap["patternWeight5"];
        public CustomValueInfo patternWeight6Info => CustomValueInfoMap["patternWeight6"];
        public CustomValueInfo patternWeight7Info => CustomValueInfoMap["patternWeight7"];
        public CustomValueInfo patternWeight8Info => CustomValueInfoMap["patternWeight8"];
        public CustomValueInfo patternWeight9Info => CustomValueInfoMap["patternWeight9"];
        public CustomValueInfo randomSeedInfo => CustomValueInfoMap["randomSeed"];

        public Vector2 size
        {
            get => sizeValues.ToVector2();
            set => sizeValues.FromVector2(value);
        }
        public Vector2 seatDistance
        {
            get => seatDistanceValues.ToVector2();
            set => seatDistanceValues.FromVector2(value);
        }
        public Vector3 randomPositionRange
        {
            get => randomPositionRangeValues.ToVector3();
            set => randomPositionRangeValues.FromVector3(value);
        }
        public float barCountWeight0
        {
            get => barCountWeight0Value.value;
            set => barCountWeight0Value.value = value;
        }
        public float barCountWeight1
        {
            get => barCountWeight1Value.value;
            set => barCountWeight1Value.value = value;
        }
        public float barCountWeight2
        {
            get => barCountWeight2Value.value;
            set => barCountWeight2Value.value = value;
        }
        public float barCountWeight3
        {
            get => barCountWeight3Value.value;
            set => barCountWeight3Value.value = value;
        }
        public float colorWeight1
        {
            get => colorWeight1Value.value;
            set => colorWeight1Value.value = value;
        }
        public float colorWeight2
        {
            get => colorWeight2Value.value;
            set => colorWeight2Value.value = value;
        }
        public float patternWeight0
        {
            get => patternWeight0Value.value;
            set => patternWeight0Value.value = value;
        }
        public float patternWeight1
        {
            get => patternWeight1Value.value;
            set => patternWeight1Value.value = value;
        }
        public float patternWeight2
        {
            get => patternWeight2Value.value;
            set => patternWeight2Value.value = value;
        }
        public float patternWeight3
        {
            get => patternWeight3Value.value;
            set => patternWeight3Value.value = value;
        }
        public float patternWeight4
        {
            get => patternWeight4Value.value;
            set => patternWeight4Value.value = value;
        }
        public float patternWeight5
        {
            get => patternWeight5Value.value;
            set => patternWeight5Value.value = value;
        }
        public float patternWeight6
        {
            get => patternWeight6Value.value;
            set => patternWeight6Value.value = value;
        }
        public float patternWeight7
        {
            get => patternWeight7Value.value;
            set => patternWeight7Value.value = value;
        }
        public float patternWeight8
        {
            get => patternWeight8Value.value;
            set => patternWeight8Value.value = value;
        }
        public float patternWeight9
        {
            get => patternWeight9Value.value;
            set => patternWeight9Value.value = value;
        }
        public int randomSeed
        {
            get => randomSeedValue.intValue;
            set => randomSeedValue.intValue = value;
        }

        public void FromConfig(PsylliumAreaConfig config)
        {
            position = config.position;
            size = config.size;
            seatDistance = config.seatDistance;
            randomPositionRange = config.randomPositionRange;
            barCountWeight0 = config.barCountWeight0;
            barCountWeight1 = config.barCountWeight1;
            barCountWeight2 = config.barCountWeight2;
            barCountWeight3 = config.barCountWeight3;
            colorWeight1 = config.colorWeight1;
            colorWeight2 = config.colorWeight2;
            patternWeight0 = config.patternWeight0;
            patternWeight1 = config.patternWeight1;
            patternWeight2 = config.patternWeight2;
            patternWeight3 = config.patternWeight3;
            patternWeight4 = config.patternWeight4;
            patternWeight5 = config.patternWeight5;
            patternWeight6 = config.patternWeight6;
            patternWeight7 = config.patternWeight7;
            patternWeight8 = config.patternWeight8;
            patternWeight9 = config.patternWeight9;
            randomSeed = config.randomSeed;
        }

        private PsylliumAreaConfig _config = new PsylliumAreaConfig();

        public PsylliumAreaConfig ToConfig()
        {
            _config.position = position;
            _config.size = size;
            _config.seatDistance = seatDistance;
            _config.randomPositionRange = randomPositionRange;
            _config.barCountWeight0 = barCountWeight0;
            _config.barCountWeight1 = barCountWeight1;
            _config.barCountWeight2 = barCountWeight2;
            _config.barCountWeight3 = barCountWeight3;
            _config.colorWeight1 = colorWeight1;
            _config.colorWeight2 = colorWeight2;
            _config.patternWeight0 = patternWeight0;
            _config.patternWeight1 = patternWeight1;
            _config.patternWeight2 = patternWeight2;
            _config.patternWeight3 = patternWeight3;
            _config.patternWeight4 = patternWeight4;
            _config.patternWeight5 = patternWeight5;
            _config.patternWeight6 = patternWeight6;
            _config.patternWeight7 = patternWeight7;
            _config.patternWeight8 = patternWeight8;
            _config.patternWeight9 = patternWeight9;
            _config.randomSeed = randomSeed;
            return _config;
        }
    }
}