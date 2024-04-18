using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    public enum KeyBindType
    {
        PluginToggle,
        AddKeyFrame,
        RemoveKeyFrame,
        Play,
        EditMode,
        Copy,
        Paste,
        FlipPaste,
        PrevFrame,
        NextFrame,
        PrevKeyFrame,
        NextKeyFrame,
        MultiSelect,
        Undo,
        Redo,
    }

    public class Config
    {
        public static readonly int CurrentVersion = 2;

        [XmlAttribute]
        public int version = 0;

        [XmlIgnore]
        public Dictionary<KeyBindType, KeyBind> keyBinds = new Dictionary<KeyBindType, KeyBind>
        {
            { KeyBindType.PluginToggle, new KeyBind("Ctrl+M") },
            { KeyBindType.AddKeyFrame, new KeyBind("Return") },
            { KeyBindType.RemoveKeyFrame, new KeyBind("Backspace") },
            { KeyBindType.Play, new KeyBind("Space") },
            { KeyBindType.EditMode, new KeyBind("F1") },
            { KeyBindType.Copy, new KeyBind("Ctrl+C") },
            { KeyBindType.Paste, new KeyBind("Ctrl+V") },
            { KeyBindType.FlipPaste, new KeyBind("Ctrl+Shift+V") },
            { KeyBindType.PrevFrame, new KeyBind("A") },
            { KeyBindType.NextFrame, new KeyBind("D") },
            { KeyBindType.PrevKeyFrame, new KeyBind("Ctrl+A") },
            { KeyBindType.NextKeyFrame, new KeyBind("Ctrl+D") },
            { KeyBindType.MultiSelect, new KeyBind("Shift") },
            { KeyBindType.Undo, new KeyBind("Ctrl+Z") },
            { KeyBindType.Redo, new KeyBind("Ctrl+X") },
        };

        public struct KeyBindPair
        {
            public KeyBindType key;
            public string value;
        }

        [XmlElement("keyBind")]
        public KeyBindPair[] keyBindsXml
        {
            get
            {
                var result = new List<KeyBindPair>(keyBinds.Count);
                foreach (var pair in keyBinds)
                {
                    result.Add(new KeyBindPair { key = pair.Key, value = pair.Value.ToString() });
                }
                return result.ToArray();
            }
            set
            {
                foreach (var pair in value)
                {
                    //PluginUtils.LogDebug("keyBind: " + pair.key + " = " + pair.value);
                    keyBinds[pair.key] = new KeyBind(pair.value);
                }
            }
        }

        // 動作設定
        public bool pluginEnabled = true;
        public bool isEasyEdit = false;
        public bool isAutoScroll = false;
        public TangentType defaultTangentType = TangentType.Smooth;
        public int detailTransformCount = 16;
        public int detailTangentCount = 32;
        public bool disablePoseHistory = true;
        public int historyLimit = 20;

        // 表示設定
        public int frameWidth = 11;
        public int frameHeight = 20;
        public int frameNoInterval = 5;
        public int thumWidth = 256;
        public int thumHeight = 192;
        public int windowPosX = -1;
        public int windowPosY = -1;

        // 色設定
        public Color timelineBgColor1 = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        public Color timelineBgColor2 = new Color(64 / 255f, 64 / 255f, 72 / 255f);
        public Color timelineLineColor1 = new Color(127 / 255f, 127 / 255f, 127 / 255f);
        public Color timelineLineColor2 = new Color(70 / 255f, 93 / 255f, 170 / 255f);
        public Color timelineMenuBgColor = new Color(105 / 255f, 28 / 255f, 42 / 255f);
        public Color timelineMenuSelectBgColor = new Color(255 / 255f, 0 / 255f, 0 / 255f, 0.2f);
        public Color timelineMenuSelectTextColor = new Color(249 / 255f, 193 / 255f, 207/ 255f);
        public float timelineBgAlpha = 0.5f;
        public Color curveLineColor = new Color(101 / 255f, 154 / 255f, 210 / 255f);
        public Color curveLineSmoothColor = new Color(90 / 255f, 255 / 255f, 25 / 255f);
        public Color curveBgColor = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0.3f);
        public Color windowHoverColor = new Color(48 / 255f, 48 / 255f, 48 / 255f, 224 / 255f);

        [XmlIgnore]
        public bool dirty = false;

        public TangentPair defaultTangentPair
        {
            get
            {
                return TangentPair.GetDefault(defaultTangentType);
            }
        }

        public void ConvertVersion()
        {
            version = CurrentVersion;
        }

        public bool GetKey(KeyBindType keyBindType)
        {
            return keyBinds[keyBindType].GetKey();
        }

        public bool GetKeyDown(KeyBindType keyBindType)
        {
            return keyBinds[keyBindType].GetKeyDown();
        }

        public bool GetKeyUp(KeyBindType keyBindType)
        {
            return keyBinds[keyBindType].GetKeyUp();
        }

        public string GetKeyName(KeyBindType keyBindType)
        {
            return keyBinds[keyBindType].ToString();
        }
    }
}

