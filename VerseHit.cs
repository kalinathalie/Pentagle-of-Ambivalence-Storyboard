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


namespace StorybrewScripts{
    public class VerseHit : StoryboardObjectGenerator{
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
        public string CursorPath = "sb/pl.png";

        [Configurable]
        public double SpriteScale = 1;

        private const int trailCount = 16;

        public override void Generate(){
            var HitObjectlayer = GetLayer("HitObject");
            var startPos = Beatmap.HitObjects.Where((o) => o.EndTime > StartTime).First().Position;

            ArrayList cursorTrail = new ArrayList();
            for(int i = 0; i < trailCount; i++){
                var trail = HitObjectlayer.CreateSprite("sb/particle.png", OsbOrigin.Centre, startPos);

                //trail.Fade(StartTime, 0.33);
                trail.Additive(StartTime, EndTime);
                trail.Scale(StartTime, 1 - i*0.04);

                cursorTrail.Add(trail);
            }

            var hSprite = HitObjectlayer.CreateSprite(Line, OsbOrigin.Centre, new Vector2(startPos.X, 260));
            var cursor = HitObjectlayer.CreateSprite(CursorPath, OsbOrigin.Centre, startPos);

            hSprite.ScaleVec(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 854/2, 854/2, 15, 854/2);
            hSprite.Fade(OsbEasing.InCubic, StartTime, StartTime+tick(0,0.75), 1, 0.1);
            cursor.Scale(StartTime,SpriteScale);
            cursor.Additive(StartTime, EndTime);


            var lyric_hSprite = HitObjectlayer.CreateSprite(Line, OsbOrigin.Centre, new Vector2(585, 56));
            lyric_hSprite.ScaleVec(OsbEasing.OutCirc, 22640, 24052, 0, 380/2, 35, 380/2);
            lyric_hSprite.Rotate(22640, MathHelper.DegreesToRadians(96));
            lyric_hSprite.Fade(22640, 24052, 0, 0.2);
            lyric_hSprite.Fade(22640, 34640, 0.2, 0.2);
            lyric_hSprite.Fade(34640, 35346, 0.2, 1);
            lyric_hSprite.ScaleVec(OsbEasing.InCirc, 34640, 35346, 35, 380/2, 0, 2000/2);
            
            OsuHitObject prevObject = null;
            var circleArray = new List<int>();
            //circle and line
            circleArray.Add(24052);
            circleArray.Add(25464);
            circleArray.Add(26875);
            circleArray.Add(28287);
            circleArray.Add(28816);
            circleArray.Add(29169);
            circleArray.Add(29699);
            circleArray.Add(31464);
            circleArray.Add(32522);
            circleArray.Add(34287);
            circleArray.Add(34287);

            hSprite.Fade(28993, 28993+400, 0.6, 0.1); //n tem
            hSprite.Fade(29522, 29522+400, 0.6, 0.1); //n tem
            hSprite.Fade(30405, 30405+400, 0.6, 0.1); //n tem
            hSprite.Fade(31816, 31816+400, 0.6, 0.1); //n tem
            hSprite.Fade(33228, 33228+400, 0.6, 0.1); //n tem
            hSprite.Fade(34640, 34640+200, 0.6, 0.2); //n tem
            hSprite.Fade(34905, 34905+200, 0.7, 0.4); //n tem
            hSprite.Fade(35169, 35169+200, 0.8, 1); //n tem
            hSprite.ScaleVec(OsbEasing.InExpo, 34640, 35346, 15, 854/2, 500, 854/2);
            
            
            foreach (OsuHitObject hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
                if(circleArray.Contains((int)hitobject.StartTime) || circleArray.Contains((int)hitobject.StartTime+1) || circleArray.Contains((int)hitobject.StartTime -1)){
                    var dropCircle = HitObjectlayer.CreateSprite("sb/q2.png", OsbOrigin.Centre, hitobject.Position);
                    dropCircle.Scale(hitobject.StartTime, hitobject.StartTime+1000, 0, 1);
                    dropCircle.Fade(hitobject.StartTime, hitobject.StartTime+400, 1, 0);
                }
                if (prevObject != null) 
                {
                    hSprite.MoveX(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition.X, hitobject.Position.X);
                    //outline.Move(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition, hitobject.Position);
                    cursor.Move(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition, hitobject.Position);
                    //hSprite.Scale(OsbEasing.In, hitobject is OsuSlider ? hitobject.StartTime : prevObject.EndTime, hitobject.EndTime, SpriteScale, SpriteScale * 0.6);

                    for(int i = 0; i < trailCount; i++)
                    {
                        OsbSprite trail = (OsbSprite) cursorTrail[i];
                        trail.Move(prevObject.EndTime + (i+1)*20, hitobject.StartTime + (i+1)*20, prevObject.EndPosition, hitobject.Position);
                        trail.Color(hitobject.StartTime + (i+1)*20, hitobject.Color);
                        trail.Fade(hitobject.StartTime, 0.33);
                    }
                
                
                }
                cursor.Color(hitobject.StartTime, hitobject.Color);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    var slider = (OsuSlider) hitobject;                    
                    
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = hSprite.PositionAt(startTime);
                        var cursorPos = cursor.PositionAt(startTime);
                        hSprite.MoveX(startTime, endTime, startPosition.X, hitobject.PositionAtTime(endTime).X);
                        //outline.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));
                        cursor.Move(startTime, endTime, cursorPos, hitobject.PositionAtTime(endTime));

                        for(int i = 0; i < trailCount; i++)
                        {
                            OsbSprite trail = (OsbSprite) cursorTrail[i];
                            trail.Move(startTime + (i+1)*20, endTime + (i+1)*20, cursorPos, hitobject.PositionAtTime(endTime));
                        }

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
