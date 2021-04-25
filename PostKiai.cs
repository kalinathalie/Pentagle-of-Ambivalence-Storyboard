using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts{
    public class PostKiai : StoryboardObjectGenerator{

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string Head = "sb/head.png";

        [Configurable]
        public string HeadBlur = "sb/head.png";

        [Configurable]
        public string CircleHead = "sb/c2.png";

        [Configurable]
        public string CircleBeat = "sb/to.png";

        [Configurable]
        public string BgSad = "sb/bg_sad.png";

        [Configurable]
        public string BgBlur = "sb/bg_BLUR.png";

        [Configurable]
        public string Line = "sb/particle.png";

        [Configurable]
        public string HeartGrow = "sb/heart2.png";

        public override void Generate(){
		    var layer = GetLayer("PostKiai");
            var HitObjectlayer = GetLayer("HitObject");
            var BGbitmap = GetMapsetBitmap(BgSad);

            var line = layer.CreateSprite(Line, OsbOrigin.Centre);
            line.Fade(OsbEasing.OutCubic, EndTime-tick(0,1), EndTime, 0, 0.6);
            line.Rotate(EndTime, EndTime+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(63), MathHelper.DegreesToRadians(-30));
            line.Rotate(EndTime+tick(0,(double)1/(double)4), MathHelper.DegreesToRadians(270));
            line.ScaleVec(OsbEasing.OutQuad, EndTime-tick(0,1), EndTime, 0, 5, 5, 70);
            line.ScaleVec(OsbEasing.InQuad, EndTime, EndTime+tick(0,1), 5, 70, 70, 70);
            line.ScaleVec(OsbEasing.OutQuint, EndTime+tick(0,(double)1/(double)4), EndTime+tick(0,(double)1/(double)6), 70, 70, 0, 70);
            line.Fade(EndTime, EndTime+tick(0,(double)1/(double)4), 0.6, 0.9);

            var backgroundSad = layer.CreateSprite(BgSad,OsbOrigin.Centre);
            backgroundSad.Scale(StartTime, 480.0f / BGbitmap.Height);
            backgroundSad.Fade(StartTime, StartTime+tick(0,(double)1/(double)8), 0, 0.35);
            backgroundSad.Fade(StartTime+tick(0,(double)1/(double)16), EndTime, 0.35, 0);

            var backgroundBlur = layer.CreateSprite(BgBlur,OsbOrigin.Centre);
            backgroundBlur.Scale(StartTime, 480.0f / BGbitmap.Height);
            backgroundBlur.Fade(StartTime+tick(0,(double)1/(double)16), EndTime, 0, 0.35);
            backgroundBlur.Fade(EndTime, EndTime+tick(0,1), 0.35, 0);

            var circleBeatPump = layer.CreateSprite(CircleBeat,OsbOrigin.Centre);
            for(double circlePump = StartTime+tick(0,(double)1/(double)2); circlePump <= StartTime+tick(0,(double)1/(double)16); circlePump+=tick(0,(double)1/(double)4)){
                circleBeatPump.Fade(OsbEasing.InCirc, circlePump, circlePump+tick(0,0.75), 0.7, 0);
                circleBeatPump.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 1, 1.35);
            }
            
            for(double circlePump = StartTime+tick(0,(double)1/(double)17); circlePump <= EndTime; circlePump+=tick(0,(double)1/(double)2)){
                circleBeatPump.Fade(OsbEasing.InCirc, circlePump, circlePump+tick(0,1), 0.7, 0);
                circleBeatPump.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,2), 1, 1.35);
            }

            var headSpinBlur = layer.CreateSprite(HeadBlur,OsbOrigin.Centre);
            headSpinBlur.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,(double)1/(double)16), 0, 1);
            headSpinBlur.Fade(StartTime+tick(0,(double)1/(double)16), EndTime, 1, 0);
            headSpinBlur.Scale(StartTime, 0.59);
            headSpinBlur.Rotate(StartTime,EndTime, MathHelper.DegreesToRadians(15),  MathHelper.DegreesToRadians(-15));

            var headSpin = layer.CreateSprite(Head,OsbOrigin.Centre);
            headSpin.Fade(StartTime+tick(0,(double)1/(double)16), EndTime, 0, 1);
            headSpin.Scale(StartTime, 0.59);
            headSpin.Rotate(StartTime,EndTime, MathHelper.DegreesToRadians(15),  MathHelper.DegreesToRadians(-15));
            headSpin.Rotate(EndTime,EndTime+tick(0,1), MathHelper.DegreesToRadians(-15),  MathHelper.DegreesToRadians(30));
            

            var circleSpin = layer.CreateSprite(CircleHead,OsbOrigin.Centre);
            circleSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,(double)1/(double)16), 0, 1);
            circleSpin.Fade(StartTime+tick(0,(double)1/(double)16), EndTime-tick(0,(double)1/(double)2), 1, 1);
            circleSpin.Scale(StartTime, 0.45);

            for(double circlePump = StartTime+tick(0,(double)1/(double)2); circlePump <= EndTime; circlePump+=tick(0,(double)1/(double)4)){
                headSpinBlur.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.59, 0.59*1.1);
                circleSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.45, 0.45*1.1);

                headSpinBlur.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.59*1.1, 0.59);
                circleSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.45*1.1, 0.45);
            }

            for(double circlePump = StartTime+tick(0,(double)1/(double)17); circlePump <= EndTime; circlePump+=tick(0,(double)1/(double)2)){
                headSpinBlur.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.59, 0.59*1.1);
                headSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.59, 0.59*1.1);
                circleSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.45, 0.45*1.1);

                headSpinBlur.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.59*1.1, 0.59);
                headSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.59*1.1, 0.59);
                circleSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.45*1.1, 0.45);
            }

            headSpin.Fade(EndTime, EndTime+tick(0,1), 1, 0);
            circleSpin.Fade(EndTime, EndTime+tick(0,1), 1, 0);

            var heartBeat = new List<int>();
            heartBeat.Add(105934);
            heartBeat.Add(106199);
            heartBeat.Add(106463);
            heartBeat.Add(106728);
            heartBeat.Add(106993);
            heartBeat.Add(107169);

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if(hitobject.StartTime >= 105934 && hitobject.StartTime < 107346){
                    if(heartBeat.Contains((int)hitobject.StartTime) || heartBeat.Contains((int)hitobject.StartTime+1) || heartBeat.Contains((int)hitobject.StartTime-1)){
                        var heartSprite = HitObjectlayer.CreateSprite(HeartGrow, OsbOrigin.Centre, hitobject.Position);
                        heartSprite.Scale(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)1.5), 0.2, 20);
                        heartSprite.Color(hitobject.StartTime, hitobject.Color);
                        heartSprite.Fade(hitobject.StartTime, 107346, 0.3, 0.5);
                        heartSprite.Fade(107346, 107699, 0.5, 0);
                        heartBeat.Remove((int)hitobject.StartTime);
                    }
                }
            }
            foreach(int beat in heartBeat){
                var heartSprite = HitObjectlayer.CreateSprite(HeartGrow, OsbOrigin.Centre, new Vector2(320,240));
                heartSprite.Scale(beat, beat+tick(0,(double)1/(double)1.5), 0.2, 20);
                heartSprite.ColorHsb(beat, Random(0,360), 1, 1);
                heartSprite.Fade(beat, 107346, 0.3, 0.5);
                heartSprite.Fade(107346, 107699, 0.5, 0);
            }
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
