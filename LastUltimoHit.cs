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
    public class LastUltimoHit : StoryboardObjectGenerator{
        
        [Configurable]
        public string Line = "sb/pixel.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 120052;

        public override void Generate(){
            var HitObjectlayer = GetLayer("HitObject");

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                var hSprite = HitObjectlayer.CreateSprite(Line, OsbOrigin.Centre);
                hSprite.ScaleVec(StartTime, 30, 854/2);
                hSprite.Fade(StartTime, 0);
                hSprite.Fade(OsbEasing.OutQuart, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)3), 0.7, 0);
                if(Random(0,2)==1){
                    hSprite.MoveX(OsbEasing.OutQuart, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)3), hitobject.Position.X, hitobject.Position.X-50);
                }else{
                    hSprite.MoveX(OsbEasing.OutQuart, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)3), hitobject.Position.X, hitobject.Position.X+50);
                }
            }
            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime <= 124287 || 125699 <= hitobject.StartTime))
                    continue;
                    var dropLine = HitObjectlayer.CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(hitobject.Position.X, 240));
                    dropLine.ScaleVec(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 858/2, 0, 858/2);
                    dropLine.Color(hitobject.StartTime, Color4.White);
                    dropLine.Fade(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 0);
                    dropLine.Rotate(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(Random(-5,5)), MathHelper.DegreesToRadians(Random(-5,5)));
            }
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
