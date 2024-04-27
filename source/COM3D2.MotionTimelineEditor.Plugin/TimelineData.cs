using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    using MTE = MotionTimelineEditor;

    public class TimelineData
    {
        public static readonly int CurrentVersion = 4;
        public static readonly TimelineData DefaultTimeline = new TimelineData();

        public int version = 0;

        public List<ITimelineLayer> layers = new List<ITimelineLayer>();

        public List<TrackData> tracks = new List<TrackData>();

        private int _maxFrameNo = 30;
        public int maxFrameNo
        {
            get
            {
                return _maxFrameNo;
            }
            set
            {
                if (_maxFrameNo == value)
                {
                    return;
                }

                _maxFrameNo = Mathf.Max(value, maxExistFrameNo, 1);
            }
        }

        private float _frameRate = 30f;
        private float _frameDuration = 1f / 30f;

        public float frameRate
        {
            get
            {
                return _frameRate;
            }
            set
            {
                if (_frameRate == value)
                {
                    return;
                }
                _frameRate = Mathf.Max(1f, value);
                _frameDuration = 1f / _frameRate;
            }
        }

        public float frameDuration
        {
            get
            {
                return _frameDuration;
            }
        }

        public string anmName = "";

        public string directoryName = "";

        public bool[] isHoldList = new bool[(int) IKHoldType.Max]
        {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
        };

        private bool _useMuneKeyL = false;
        public bool useMuneKeyL
        {
            get
            {
                return _useMuneKeyL;
            }
            set
            {
                if (_useMuneKeyL == value)
                {
                    return;
                }

                _useMuneKeyL = value;
                studioHack.useMuneKeyL = value;
            }
        }

        private bool _useMuneKeyR = false;
        public bool useMuneKeyR
        {
            get
            {
                return _useMuneKeyR;
            }
            set
            {
                if (_useMuneKeyR == value)
                {
                    return;
                }

                _useMuneKeyR = value;
                studioHack.useMuneKeyR = value;
            }
        }

        public int maxExistFrameNo
        {
            get
            {
                var frameNo = 0;
                foreach (var layer in layers)
                {
                    frameNo = Math.Max(frameNo, layer.maxExistFrameNo);
                }
                return frameNo;
            }
        }

        public bool isLoopAnm = true;
        public float startOffsetTime = 0.5f;
        public float endOffsetTime = 0.5f;
        public float startFadeTime = 0.1f;
        public float endFadeTime = 0f;
        public Maid.EyeMoveType eyeMoveType = Maid.EyeMoveType.無し;

        public int activeTrackIndex = -1;

        public string bgmPath = "";

        public bool videoEnabled = true;
        public VideoDisplayType videoDisplayType = VideoDisplayType.GUI;
        public string videoPath = "";
        public Vector3 videoPosition = new Vector3(0, 0, 0);
        public Vector3 videoRotation = new Vector3(0, 0, 0);
        public float videoScale = 1f;
        public float videoStartTime = 0f;
        public float videoVolume = 0.5f;
        public Vector2 videoGUIPosition = new Vector2(0, 0);
        public float videoGUIScale = 1f;
        public float videoGUIAlpha = 1f;
        public Vector2 videoBackmostPosition = new Vector2(0, 0);

        public int maxFrameCount
        {
            get
            {
                return maxFrameNo + 1;
            }
        }

        public bool isHoldActive
        {
            get
            {
                return isHoldList.Any(b => b);
            }
        }

        private static TimelineManager timelineManager
        {
            get
            {
                return TimelineManager.instance;
            }
        }

        private static MaidManager maidManager
        {
            get
            {
                return MaidManager.instance;
            }
        }

        private static StudioHackBase studioHack
        {
            get
            {
                return MTE.studioHack;
            }
        }

        public TrackData activeTrack
        {
            get
            {
                if (activeTrackIndex < 0 || activeTrackIndex >= tracks.Count)
                {
                    return null;
                }
                return tracks[activeTrackIndex];
            }
        }

        public ITimelineLayer defaultLayer { get; private set; }

        public string timelinePath
        {
            get
            {
                return PluginUtils.GetTimelinePath(anmName, directoryName);
            }
        }

        public string thumPath
        {
            get
            {
                return PluginUtils.ConvertThumPath(timelinePath);
            }
        }

        public string dcmSongName
        {
            get
            {
                return "【MTE】" + anmName;
            }
        }

        public string dcmSongListPath
        {
            get
            {
                return PluginUtils.GetDcmSongListFilePath(dcmSongName);
            }
        }

        public TimelineData()
        {
        }

        public void Dispose()
        {
            foreach (var layer in layers)
            {
                layer.Dispose();
            }
            layers.Clear();
        }

        public float GetFrameTimeSeconds(int frameNo)
        {
            return frameNo * _frameDuration;
        }

        public bool IsValidData(out string message)
        {
            message = "";

            if (maidManager.maid == null)
            {
                message = "メイドを配置してください";
                return false;
            }

            if (anmName.Length == 0)
            {
                message = "アニメ名を入力してください";
                return false;
            }

            var activeTrack = this.activeTrack;
            if (activeTrack != null && !IsValidTrack(activeTrack))
            {
                message = "トラックの範囲が不正です";
                return false;
            }

            foreach (var layer in layers)
            {
                if (!layer.IsValidData())
                {
                    message = layer.errorMessage;
                    return false;
                }
            }

            return true;
        }

        public bool IsValidTrack(TrackData track)
        {
            if (track == null)
            {
                return false;
            }

            if (track.startFrameNo < 0 ||
                track.endFrameNo > maxFrameNo ||
                track.startFrameNo >= activeTrack.endFrameNo - 1)
            {
                return false;
            }

            return true;
        }

        public Texture2D CreateBGTexture(
            int frameWidth,
            int frameHeight,
            int width,
            int height,
            Color bgColor1,
            Color bgColor2,
            Color frameLineColor1,
            Color frameLineColor2,
            int frameNoInterval)
        {
            var tex = new Texture2D(width, height);
            var pixels = new Color[width * height];

            for (int x = 0; x < width; x++)
            {
                var frameNo = x / frameWidth;
                var framePos = x - frameNo * frameWidth;
                bool isSecondColorLine = frameNo % frameNoInterval == 0;
                bool isCenterLine = framePos == frameWidth / 2 ||
                        (isSecondColorLine && framePos == frameWidth / 2 + 1);

                for (int y = 0; y < height; y++)
                {
                    if (isCenterLine)
                    {
                        pixels[y * width + x] = isSecondColorLine ? frameLineColor2 : frameLineColor1;
                    }
                    else
                    {
                        bool isSecondColorBg = (y / frameHeight) % 2 == 1;
                        pixels[y * width + x] = isSecondColorBg ? bgColor2 : bgColor1;
                    }
                }
            }
            tex.SetPixels(pixels);
            tex.Apply();
            return tex;
        }

        // ひし形のテクスチャを作成
        public static Texture2D CreateKeyFrameTexture(int size, Color color)
        {
            var tex = new Texture2D(size, size);
            var pixels = new Color[size * size];
            var bgColor = new Color(0, 0, 0, 0);
            int halfSize = size / 2;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    int distanceX = Math.Abs(x - halfSize);
                    int distanceY = Math.Abs(y - halfSize);
                    if (distanceX + distanceY <= halfSize)
                    {
                        pixels[y * size + x] = color;
                    }
                    else
                    {
                        pixels[y * size + x] = bgColor;
                    }
                }
            }

            tex.SetPixels(pixels);
            tex.Apply();
            return tex;
        }

        public void Initialize()
        {
            if (layers.Count() == 0)
            {
                var layer = timelineManager.CreateLayer(typeof(MotionTimelineLayer).Name, 0);
                layers.Add(layer);
                layer.Init();
            }

            defaultLayer = layers[0];

            version = CurrentVersion;
        }

        public void ResetSettings()
        {
            //maxFrameNo = DefaultTimeline.maxFrameNo;
            frameRate = DefaultTimeline.frameRate;
            isHoldList = DefaultTimeline.isHoldList.ToArray();
            useMuneKeyL = DefaultTimeline.useMuneKeyL;
            useMuneKeyR = DefaultTimeline.useMuneKeyR;
            isLoopAnm = DefaultTimeline.isLoopAnm;
            startOffsetTime = DefaultTimeline.startOffsetTime;
            endOffsetTime = DefaultTimeline.endOffsetTime;
            startFadeTime = DefaultTimeline.startFadeTime;
            endFadeTime = DefaultTimeline.endFadeTime;
        }

        public string GetDcmSongFilePath(string fileName)
        {
            return PluginUtils.GetDcmSongFilePath(dcmSongName, fileName);
        }

        public void OnSave()
        {
            foreach (var layer in layers)
            {
                layer.OnSave();
            }
        }

        public void OnLoad()
        {
            foreach (var layer in layers)
            {
                layer.OnLoad();
            }
        }

        public void OnPluginEnable()
        {
            foreach (var layer in layers)
            {
                layer.OnPluginEnable();
            }
        }

        public void OnPluginDisable()
        {
            foreach (var layer in layers)
            {
                layer.OnPluginDisable();
            }
        }

        public void FromXml(TimelineXml xml)
        {
            version = xml.version;

            layers = new List<ITimelineLayer>(xml.layers.Count);
            foreach (var layerXml in xml.layers)
            {
                var layer = timelineManager.CreateLayer(layerXml.className, layerXml.slotNo);
                if (layer != null)
                {
                    layers.Add(layer);
                    layer.FromXml(layerXml);
                    layer.Init();
                }
            }

            tracks = new List<TrackData>(xml.tracks.Count);
            foreach (var trackXml in xml.tracks)
            {
                var track = new TrackData(trackXml);
                tracks.Add(track);
            }

            maxFrameNo = xml.maxFrameNo;
            frameRate = xml.frameRate;
            anmName = xml.anmName;
            directoryName = xml.directoryName;
            isHoldList = xml.isHoldList;
            useMuneKeyL = xml.useMuneKeyL;
            useMuneKeyR = xml.useMuneKeyR;
            isLoopAnm = xml.isLoopAnm;
            startOffsetTime = xml.startOffsetTime;
            endOffsetTime = xml.endOffsetTime;
            startFadeTime = xml.startFadeTime;
            endFadeTime = xml.endFadeTime;
            activeTrackIndex = xml.activeTrackIndex;
            bgmPath = xml.bgmPath;
            videoEnabled = xml.videoEnabled;
            videoDisplayType = xml.videoDisplayType;
            videoPath = xml.videoPath;
            videoPosition = xml.videoPosition;
            videoRotation = xml.videoRotation;
            videoScale = xml.videoScale;
            videoStartTime = xml.videoStartTime;
            videoVolume = xml.videoVolume;
            videoGUIPosition = xml.videoGUIPosition;
            videoGUIScale = xml.videoGUIScale;
            videoGUIAlpha = xml.videoGUIAlpha;
            videoBackmostPosition = xml.videoBackmostPosition;
        }

        public TimelineXml ToXml()
        {
            var xml = new TimelineXml();
            xml.version = version;

            xml.layers = new List<TimelineLayerXml>(layers.Count);
            foreach (var layer in layers)
            {
                var layerXml = layer.ToXml();
                xml.layers.Add(layerXml);
            }

            xml.tracks = new List<TrackXml>(tracks.Count);
            foreach (var track in tracks)
            {
                var trackXml = track.ToXml();
                xml.tracks.Add(trackXml);
            }

            xml.maxFrameNo = maxFrameNo;
            xml.frameRate = frameRate;
            xml.anmName = anmName;
            xml.directoryName = directoryName;
            xml.isHoldList = isHoldList;
            xml.useMuneKeyL = useMuneKeyL;
            xml.useMuneKeyR = useMuneKeyR;
            xml.isLoopAnm = isLoopAnm;
            xml.startOffsetTime = startOffsetTime;
            xml.endOffsetTime = endOffsetTime;
            xml.startFadeTime = startFadeTime;
            xml.endFadeTime = endFadeTime;
            xml.activeTrackIndex = activeTrackIndex;
            xml.bgmPath = bgmPath;
            xml.videoEnabled = videoEnabled;
            xml.videoDisplayType = videoDisplayType;
            xml.videoPath = videoPath;
            xml.videoPosition = videoPosition;
            xml.videoRotation = videoRotation;
            xml.videoScale = videoScale;
            xml.videoStartTime = videoStartTime;
            xml.videoVolume = videoVolume;
            xml.videoGUIPosition = videoGUIPosition;
            xml.videoGUIScale = videoGUIScale;
            xml.videoGUIAlpha = videoGUIAlpha;
            xml.videoBackmostPosition = videoBackmostPosition;

            return xml;
        }
    }
}