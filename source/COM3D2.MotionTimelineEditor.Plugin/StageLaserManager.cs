using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    public class StageLaserManager : MonoBehaviour
    {
        public List<StageLaserController> controllers = new List<StageLaserController>();
        public Dictionary<string, StageLaserController> controllerMap = new Dictionary<string, StageLaserController>();
        public List<string> controllerNames = new List<string>();
        public List<StageLaser> lasers = new List<StageLaser>();
        public Dictionary<string, StageLaser> laserMap = new Dictionary<string, StageLaser>();
        public List<string> laserNames = new List<string>();

        public static event UnityAction onSetup;
        public static event UnityAction<string> onControllerAdded;
        public static event UnityAction<string> onControllerRemoved;
        public static event UnityAction<string> onLaserAdded;
        public static event UnityAction<string> onLaserRemoved;

        private static StageLaserManager _instance = null;
        public static StageLaserManager instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("StageLaserManager");
                    _instance = go.AddComponent<StageLaserManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private static StudioHackBase studioHack => StudioHackManager.studioHack;
        private static TimelineData timeline => TimelineManager.instance.timeline;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            SceneManager.sceneLoaded += OnChangedSceneLevel;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnChangedSceneLevel;

            if (_instance == this)
            {
                _instance = null;
            }
        }

        public StageLaserController GetController(string name)
        {
            StageLaserController controller;
            if (controllerMap.TryGetValue(name, out controller))
            {
                return controller;
            }
            return null;
        }

        public StageLaser GetLaser(string name)
        {
            StageLaser laser;
            if (laserMap.TryGetValue(name, out laser))
            {
                return laser;
            }
            return null;
        }

        public void Update()
        {
            if (studioHack == null || !studioHack.IsValid())
            {
                return;
            }
        }

        public void UpdateLasers()
        {
            controllerMap.Clear();
            controllerNames.Clear();
            lasers.Clear();
            laserMap.Clear();
            laserNames.Clear();

            foreach (var controller in controllers)
            {
                controllerMap.Add(controller.name, controller);
                controllerNames.Add(controller.name);

                foreach (var laser in controller.lasers)
                {
                    lasers.Add(laser);
                    laserMap.Add(laser.name, laser);
                    laserNames.Add(laser.name);
                }
            }

            PluginUtils.Log("StageLaserManager: Laser list updated");

            foreach (var laser in lasers)
            {
                PluginUtils.LogDebug("laser: displayName={0} name={1}",
                    laser.displayName, laser.name);
            }

            UpdateLaserCount();
        }

        private void UpdateLaserCount()
        {
            if (timeline == null)
            {
                return;
            }

            timeline.stageLaserCountList.Clear();

            foreach (var controller in controllers)
            {
                timeline.stageLaserCountList.Add(controller.lasers.Count);
            }
        }

        public void SetupLasers(List<int> laserCounts)
        {
            laserCounts = new List<int>(laserCounts);

            for (var i = 0; i < laserCounts.Count; i++)
            {
                PluginUtils.LogDebug("StageLaser.SetupLasers: [{0}]={1}", i, laserCounts[i]);
            }

            while (controllers.Count < laserCounts.Count)
            {
                AddController(false);
            }

            while (controllers.Count > laserCounts.Count)
            {
                RemoveController(false);
            }

            for (int i = 0; i < laserCounts.Count; i++)
            {
                var controller = controllers[i];
                var laserCount = laserCounts[i];

                while (controller.lasers.Count < laserCount)
                {
                    AddLaser(i, false);
                }

                while (controller.lasers.Count > laserCount)
                {
                    RemoveLaser(i, false);
                }
            }

            UpdateLasers();

            if (onSetup != null)
            {
                onSetup.Invoke();
            }
        }

        public void OnPluginDisable()
        {
            Reset();
        }

        public void OnPluginEnable()
        {
            // SetupLasersが呼ばれるので不要
        }

        public void Reset()
        {
            foreach (var controller in controllers)
            {
                GameObject.Destroy(controller.gameObject);
            }
            controllers.Clear();

            UpdateLasers();
        }

        public void AddController(bool notify)
        {
            var groupIndex = controllers.Count;
            var go = new GameObject("StageLaserController");
            go.transform.SetParent(transform, false);

            var controller = go.AddComponent<StageLaserController>();
            controller.groupIndex = groupIndex;
            controller.isManualUpdate = true;
            controllers.Add(controller);

            if (notify)
            {
                UpdateLasers();

                if (onControllerAdded != null)
                {
                    onControllerAdded.Invoke(controller.name);
                }
            }
            
        }

        public void RemoveController(bool notify)
        {
            if (controllers.Count == 0)
            {
                return;
            }

            var controller = controllers[controllers.Count - 1];
            var controllerName = controller.name;
            controllers.Remove(controller);
            GameObject.Destroy(controller.gameObject);
            
            if (notify)
            {
                UpdateLasers();

                if (onControllerRemoved != null)
                {
                    onControllerRemoved.Invoke(controllerName);
                }
            }
        }

        public void AddLaser(int groupIndex, bool notify)
        {
            var controller = controllers[groupIndex];
            var laserName = controller.AddLaser();
            
            if (notify)
            {
                UpdateLasers();

                if (onLaserAdded != null)
                {
                    onLaserAdded.Invoke(laserName);
                }
            }
        }

        public void RemoveLaser(int groupIndex, bool notify)
        {
            var controller = controllers[groupIndex];
            if (controller.lasers.Count == 0)
            {
                return;
            }

            var laserName = controller.RemoveLaser();

            if (notify)
            {
                UpdateLasers();

                if (onLaserRemoved != null)
                {
                    onLaserRemoved.Invoke(laserName);
                }
            }
        }

        private void OnChangedSceneLevel(Scene sceneName, LoadSceneMode SceneMode)
        {
            Reset();
        }
    }
}