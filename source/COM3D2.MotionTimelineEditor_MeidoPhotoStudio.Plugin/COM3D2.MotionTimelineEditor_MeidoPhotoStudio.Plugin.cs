using System;
using COM3D2.MotionTimelineEditor;
using COM3D2.MotionTimelineEditor.Plugin;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace COM3D2.MotionTimelineEditor_MeidoPhotoStudio.Plugin
{
    [
        PluginFilter("COM3D2x64"),
        PluginName(PluginInfo.PluginFullName),
        PluginVersion(PluginInfo.PluginVersion)
    ]
    public class MotionTimelineEditor_MeidoPhotoStudio : PluginBase
    {
        public void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
        }

        public void Start()
        {
            try
            {
                Initialize();
            }
            catch (Exception e)
            {
                MTEUtils.LogException(e);
            }
        }

        private void Initialize()
        {
            StudioHackManager.instance.Register(new MeidoPhotoStudioHack());
        }
    }
}