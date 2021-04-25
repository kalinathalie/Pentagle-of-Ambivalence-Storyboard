using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class IntroHit : StoryboardObjectGenerator{
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public int FadeTime = 200;

        [Configurable]
        public string Line = "sb/pixel.png";

        [Configurable]
        public double SpriteScale = 1;

        [Configurable]
        private const int trailCount = 14;

        public override void Generate()
        {
            var hitobjectLayer = GetLayer("HitObject");
            var startPos = Beatmap.HitObjects.Where((o) => o.EndTime > StartTime).First().Position;

            var hSprite = hitobjectLayer.CreateSprite(Line, OsbOrigin.Centre, new Vector2(320, startPos.Y));

            hSprite.ScaleVec(StartTime, 854/2, 15);
            hSprite.ScaleVec(OsbEasing.InCirc, 22640, EndTime, 854/2, 15, 854/2, 400);
            hSprite.Fade(StartTime,0.1);
            hSprite.Fade(OsbEasing.InCirc, 22640, 24052, 0.1, 0.6);
            
            OsuHitObject prevObject = null;
            
            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
                if(prevObject == null){
                    prevObject = hitobject;
                }
                var heartCircle = hitobjectLayer.CreateSprite("sb/heart.png", OsbOrigin.Centre, hitobject.Position);
                heartCircle.Scale(hitobject.StartTime, hitobject.StartTime+2000, 1, 1.3);
                if(hitobject.StartTime>=22111){
                    heartCircle.Fade(hitobject.StartTime, 24052, 1, 0);
                }else{
                    heartCircle.Fade(hitobject.StartTime, hitobject.StartTime+2000, 1, 0);
                }
                heartCircle.Rotate(hitobject.StartTime, MathHelper.DegreesToRadians(Random(-25,25)));
                heartCircle.Color(hitobject.StartTime, hitobject.Color);

                hSprite.MoveY(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition.Y, hitobject.Position.Y);                

                if (hitobject is OsuSlider){
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    var slider = (OsuSlider) hitobject;                    
                    
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = hSprite.PositionAt(startTime);
                        hSprite.MoveY(startTime, endTime, startPosition.Y, hitobject.PositionAtTime(endTime).Y);

                        if (complete) break;
                        startTime += timestep;
                    }
                }
                prevObject = hitobject;
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
