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
    public class Head : StoryboardObjectGenerator{
        
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public bool LeftAngle = true;

        [Configurable]
        public string HeadCircle = "sb/head.png";

        [Configurable]
        public string CircleHead = "sb/c2.png";

        [Configurable]
        public string CircleBeat = "sb/to.png";

        public override void Generate(){
		    var layer = GetLayer("Head");
            var headStart = StartTime+tick(0,1);

            var circleBeatPump = layer.CreateSprite(CircleBeat,OsbOrigin.Centre);
            for(double circlePump = StartTime+tick(0,(double)1/(double)2); circlePump <= EndTime; circlePump+=tick(0,(double)1/(double)4)){
                circleBeatPump.Fade(OsbEasing.InCirc, circlePump, circlePump+tick(0,(double)5/(double)6), 0.7, 0);
                circleBeatPump.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 1, 1.35);
            } 

            var headSpin = layer.CreateSprite(HeadCircle,OsbOrigin.Centre);
            headSpin.Fade(OsbEasing.OutCirc, headStart, headStart+tick(0,1), 0, 1);
            headSpin.Fade(headStart+tick(0,1), EndTime-tick(0,(double)1/(double)2), 1, 1);
            headSpin.Scale(headStart, 0.59);
            if(LeftAngle){
                headSpin.Rotate(headStart,EndTime, MathHelper.DegreesToRadians(-12),  MathHelper.DegreesToRadians(12));
            }else{
                headSpin.Rotate(headStart,EndTime, MathHelper.DegreesToRadians(12),  MathHelper.DegreesToRadians(-12));
            }
            

            var circleSpin = layer.CreateSprite(CircleHead,OsbOrigin.Centre);
            circleSpin.Fade(OsbEasing.OutCirc, headStart, headStart+tick(0,1), 0, 1);
            circleSpin.Fade(headStart+tick(0,1), EndTime-tick(0,(double)1/(double)2), 1, 1);
            circleSpin.Scale(headStart, 0.45);

            for(double circlePump = StartTime+tick(0,(double)1/(double)2); circlePump <= EndTime; circlePump+=tick(0,(double)1/(double)4)){
                headSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.59, 0.59*1.1);
                circleSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.45, 0.45*1.1);

                headSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.59*1.1, 0.59);
                circleSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.45*1.1, 0.45);
            }

            headSpin.Fade(EndTime-tick(0,(double)1/(double)2), EndTime, 1, 0.2);
            circleSpin.Fade(EndTime-tick(0,(double)1/(double)2), EndTime, 1, 0.2);
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
