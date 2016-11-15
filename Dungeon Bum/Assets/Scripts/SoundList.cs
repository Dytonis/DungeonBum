using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundList
    {
        public static class UI
        {
            public static AudioClip MenuOpen = Resources.Load("Sound/UI/MenuOpen") as AudioClip;
            public static AudioClip MenuOpenLow = Resources.Load("Sound/UI/MenuOpenLow") as AudioClip;
            public static AudioClip MenuClosed = Resources.Load("Sound/UI/MenuClosed") as AudioClip;
            public static AudioClip MenuClosedLow = Resources.Load("Sound/UI/MenuClosedLow") as AudioClip;
        }
    }
}
