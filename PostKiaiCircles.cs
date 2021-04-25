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
    public class PostKiaiCircles : StoryboardObjectGenerator{

        // Basic setup for 42735 - 62840
        StoryboardLayer layer;
        double beatduration;
        OsbEasing outEase = OsbEasing.None;
        OsbEasing inEase = OsbEasing.InCirc;

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        public override void Generate(){

            layer = GetLayer("Main");
            beatduration = Beatmap.GetTimingPointAt(62840).BeatDuration;

            circleCircles("sb/heart2.png",StartTime,EndTime, new Vector2(320,240), 170, Math.PI, -Math.PI, 0.15, 8*6, true, 4);
            
        }

        public void circleCircles(String path, int startTime, int endTime, Vector2 position, int radius, double startAngle, double endAngle, double scale, double steps, bool up, int beatsToMove){
            int cpt = 0;
            bool big = true;
            //Initial Circles

            for(double angle = startAngle; angle > endAngle; angle -= (startAngle - endAngle)/steps){
                oneCircle(path, startTime, endTime, cpt*20, position, radius, startAngle, angle, endAngle, scale, steps, up, big, beatsToMove, cpt);
                big = !big;
                cpt++;
            }
        }

        //Creates one element of a circle chain
        public void oneCircle(String path,int startTime, int endTime, int fadeOffset, Vector2 position, int radius, double startAngle, double initialAngle, double endAngle, double scale, double steps, bool up,  bool big, int beatsToMove, int cpt){
            double angleOffset = (startAngle - endAngle)/steps;


            var circle = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2((float) (position.X + radius*Math.Cos(initialAngle)),(float) (position.Y + radius*Math.Sin(initialAngle))));
            circle.Fade((OsbEasing)6, startTime + fadeOffset - 70, startTime + fadeOffset+500 , 0, 1);
            circle.Fade(endTime, 0);
            circle.Scale(startTime+fadeOffset, scale);
            circle.Rotate(startTime, MathHelper.DegreesToRadians(90)+initialAngle);
            
            for(double time = startTime; time < endTime-5; time += beatsToMove*beatduration){
                if((initialAngle <= startAngle) && (initialAngle > endAngle)){
                    if(up){
                        circle.Move(outEase, time, time+beatsToMove*beatduration, (position.X + radius*Math.Cos(initialAngle)),(float) (position.Y + radius*Math.Sin(initialAngle)), (position.X + radius*Math.Cos(initialAngle + angleOffset)),(float) (position.Y + radius*Math.Sin(initialAngle + angleOffset)));
                        circle.Rotate(outEase, time, time+beatsToMove*beatduration, initialAngle - Math.PI/2, initialAngle+angleOffset - Math.PI/2);
                    }
                    else{
                        circle.Rotate(outEase, time, time+beatsToMove*beatduration, initialAngle - Math.PI/2, initialAngle-angleOffset - Math.PI/2);
                        circle.Move(outEase, time, time+beatsToMove*beatduration, (position.X + radius*Math.Cos(initialAngle)),(float) (position.Y + radius*Math.Sin(initialAngle)), (position.X + radius*Math.Cos(initialAngle - angleOffset)),(float) (position.Y + radius*Math.Sin(initialAngle - angleOffset)));

                    }
                }
            }
        }
    }
}
