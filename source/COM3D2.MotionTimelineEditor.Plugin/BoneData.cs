using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{

    public class BoneData
    {
        public FrameData parentFrame { get; set; }

        public ITransformData transform { get; private set; }

        public string name
        {
            get
            {
                return transform.name;
            }
        }

        public IKManager.BoneType boneType
        {
            get
            {
                return BoneUtils.GetBoneTypeByName(transform.name);
            }
        }

        public int frameNo
        {
            get
            {
                return parentFrame != null ? parentFrame.frameNo : -1;
            }
        }

        public ITimelineLayer parentLayer
        {
            get
            {
                return parentFrame.parentLayer;
            }
        }

        public BoneData(FrameData parentFrame)
        {
            this.parentFrame = parentFrame;
        }

        public BoneData(FrameData parentFrame, ITransformData transform)
        {
            this.parentFrame = parentFrame;
            this.transform = transform;
        }

        public void FromXml(BoneXml xml)
        {
            transform = parentLayer.CreateTransformData(xml.transform);
        }

        public BoneXml ToXml()
        {
            var xml = new BoneXml();
            xml.transform = transform.ToXml();
            return xml;
        }
    }
}