using UnityEngine;

namespace COM3D2.MotionTimelineEditor.Plugin
{
    public abstract class SubWindowUIBase
    {
        public SubWindow subWindow { get; private set; }
        public abstract string title { get; }

        public static int WINDOW_WIDTH
        {
            get
            {
                return SubWindow.WINDOW_WIDTH;
            }
        }

        public static int WINDOW_HEIGHT
        {
            get
            {
                return SubWindow.WINDOW_HEIGHT;
            }
        }

        protected static TimelineManager timelineManager
        {
            get
            {
                return TimelineManager.instance;
            }
        }

        protected static TimelineData timeline
        {
            get
            {
                return timelineManager.timeline;
            }
        }

        protected static MaidManager maidManager
        {
            get
            {
                return MaidManager.instance;
            }
        }

        protected static StudioModelManager modelManager
        {
            get
            {
                return StudioModelManager.instance;
            }
        }

        protected static StudioHackBase studioHack
        {
            get
            {
                return StudioHackManager.studioHack;
            }
        }
        
        protected static ModelHackManager modelHackManager
        {
            get
            {
                return ModelHackManager.instance;
            }
        }

        protected static WindowManager windowManager
        {
            get
            {
                return WindowManager.instance;
            }
        }
        
        protected static ITimelineLayer currentLayer
        {
            get
            {
                return timelineManager.currentLayer;
            }
        }

        protected static Config config
        {
            get
            {
                return ConfigManager.config;
            }
        }

        protected Rect windowRect
        {
            get
            {
                return subWindow.windowRect;
            }
        }

        public SubWindowUIBase(SubWindow subWindow)
        {
            this.subWindow = subWindow;
        }

        private GUIView headerView = null;
        private GUIView contentView = null;
        private ComboBoxValue<SubWindowType> subWindowTypeComboBox = null;

        public static Texture2D texLock = null;

        public virtual void InitWindow()
        {
            if (headerView == null)
            {
                headerView = new GUIView(0, 0, WINDOW_WIDTH, 20);
                headerView.padding = Vector2.zero;
                headerView.margin = 0;
            }

            if (contentView == null)
            {
                contentView = new GUIView(0, 20, WINDOW_WIDTH, WINDOW_HEIGHT - 20);
            }

            if (subWindowTypeComboBox == null)
            {
                subWindowTypeComboBox = new ComboBoxValue<SubWindowType>
                {
                    label = "切替",
                    items = SubWindow.SubWindowTypes,
                    getName = (type, index) => subWindow.GetSubWindowUI(type).title,
                    onSelected = (type, index) => subWindow.subWindowType = type,
                };
            }

            if (texLock == null)
            {
                texLock = new Texture2D(0, 0);
                texLock.LoadImage(PluginUtils.LockIcon);
            }

            headerView.ResetLayout();
            headerView.SetEnabled(!IsComboBoxFocused());

            contentView.ResetLayout();
            contentView.SetEnabled(!IsComboBoxFocused());
            contentView.padding = GUIView.defaultPadding;
            contentView.margin = GUIView.defaultMargin;
        }

        public virtual void DrawWindow(int id)
        {
            InitWindow();

            DrawHeader(headerView);
            DrawContent(contentView);
            DrawComboBox(contentView);

            if (!IsDragging())
            {
                GUI.DragWindow();
            }
        }

        public virtual void DrawHeader(GUIView view)
        {
            view.BeginLayout(GUIView.LayoutDirection.Horizontal);
            {
                view.currentPos.x = 140;

                subWindowTypeComboBox.currentIndex = (int) subWindow.subWindowType;
                view.DrawComboBoxButton(subWindowTypeComboBox, 80, 20, true);

                if (view.DrawButton("+", 20, 20))
                {
                    windowManager.AddSubWindow();
                }

                var lockColor = subWindow.isPositionLocked ? Color.white : Color.gray;
                if (view.DrawTextureButton(texLock, 20, 20, lockColor))
                {
                    subWindow.isPositionLocked = !subWindow.isPositionLocked;
                }

                if (view.DrawButton("x", 20, 20))
                {
                    subWindow.isShowWnd = false;
                }
            }
            view.EndLayout();
        }

        public abstract void DrawContent(GUIView view);

        public virtual void DrawComboBox(GUIView view)
        {
            view.SetEnabled(true);

            view.DrawComboBoxContent(
                subWindowTypeComboBox,
                180, 200,
                windowRect.width, windowRect.height,
                20);
        }

        public virtual bool IsComboBoxFocused()
        {
            return subWindowTypeComboBox.focused;
        }

        public virtual bool IsDragging()
        {
            return false;
        }
    }
}