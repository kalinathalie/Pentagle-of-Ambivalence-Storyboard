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
    public class SambaHit : StoryboardObjectGenerator{
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

        [Configurable]
        public string BgBlur = "sb/bg_BLUR.png";

        [Configurable]
        public string CrazyBG = "sb/flash.png";
        
        [Configurable]
        public string BGVig = "sb/vig.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string HeartGrow = "sb/heart.png";
        
        private const int trailCount = 16;

        public override void Generate(){
            var HitObjectlayer = GetLayer("HitObject");
            var layer = GetLayer("CalmPart");

            var BGbitmap = GetMapsetBitmap(BgBlur);

            var backgroundBlur = layer.CreateSprite(BgBlur,OsbOrigin.Centre);
            backgroundBlur.Scale(StartTime, 480.0f / BGbitmap.Height);
            backgroundBlur.Fade(StartTime, 0.8);
            backgroundBlur.Fade(EndTime, EndTime+tick(0,1), 1, 0);

            var crazy = layer.CreateSprite(CrazyBG,OsbOrigin.Centre);
            crazy.Scale(StartTime, 480.0f / BGbitmap.Height);
            var CrazyBeat = new List<int>();
            for(double x = StartTime; x<193460;x+=tick(0,(double)1/(double)4)){
                CrazyBeat.Add((int)x);
                CrazyBeat.Add((int)(x+tick(0,1)));
                CrazyBeat.Add((int)(x+tick(0,1)+tick(0,2)));
                CrazyBeat.Add((int)(x+tick(0,1)+tick(0,2)+tick(0,1)));
                CrazyBeat.Add((int)(x+tick(0,(double)1/(double)3)));
            }
            for(double x = 193464; x<196287;x+=tick(0,2)){
                CrazyBeat.Add((int)x);
            }
            var CrazyBeatQuatro = new List<int>();
            for(double x = 196287; x<=EndTime;x+=tick(0,4)){
                CrazyBeatQuatro.Add((int)x);
            }
            foreach(int time in CrazyBeat){
                crazy.Fade((OsbEasing)2,time, time+tick(0,2), 0.3, 0);
            }
            crazy.Fade(196287, EndTime+tick(0,1), 0.3, 0);

            var flash = layer.CreateSprite(Flash,OsbOrigin.Centre);
            flash.Fade(StartTime, 0.6);
            flash.Fade(EndTime, 0);
            flash.Additive(StartTime, EndTime);

            var vig = layer.CreateSprite(BGVig,OsbOrigin.Centre);
            vig.Fade(StartTime, 1);
            vig.Fade(EndTime, EndTime+tick(0,1), 1, 0);
            var bitmapVig = GetMapsetBitmap(BGVig);
            vig.Scale(StartTime, 540.0f / bitmapVig.Height);

            var startPos = Beatmap.HitObjects.Where((o) => o.EndTime > StartTime).First().Position;

            ArrayList cursorTrail = new ArrayList();
            for(int i = 0; i < trailCount; i++){
                var trail = HitObjectlayer.CreateSprite("sb/particle.png", OsbOrigin.Centre, startPos);

                //trail.Fade(StartTime, 0.33);
                trail.Scale(StartTime, 1 - i*0.04);

                cursorTrail.Add(trail);
            }

            var hSprite = HitObjectlayer.CreateSprite(Line, OsbOrigin.Centre, new Vector2(320, startPos.Y));
            hSprite.Color(StartTime, Color4.Black);
            var cursor = HitObjectlayer.CreateSprite(CursorPath, OsbOrigin.Centre, startPos);

            hSprite.ScaleVec(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 854/2, 854/2, 854/2, 15);
            hSprite.Fade(OsbEasing.InCubic, StartTime, StartTime+tick(0,0.75), 1, 1);
            cursor.Scale(StartTime,SpriteScale);
            cursor.Additive(StartTime, EndTime);
            
            OsuHitObject prevObject = null;
            var circleArray = new List<int>();
            /*
            hSprite.Fade(28993, 28993+400, 0.6, 0.1); //n tem
            hSprite.Fade(29522, 29522+400, 0.6, 0.1); //n tem
            hSprite.Fade(30405, 30405+400, 0.6, 0.1); //n tem
            hSprite.Fade(31816, 31816+400, 0.6, 0.1); //n tem
            hSprite.Fade(33228, 33228+400, 0.6, 0.1); //n tem
            hSprite.Fade(34640, 34640+200, 0.6, 0.2); //n tem
            hSprite.Fade(34905, 34905+200, 0.7, 0.4); //n tem
            hSprite.Fade(35169, 35169+200, 0.8, 1); //n tem
            hSprite.ScaleVec(OsbEasing.InExpo, 34640, 35346, 15, 854/2, 500, 854/2);*/
            
            
            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
                if(CrazyBeat.Contains((int)hitobject.StartTime) || CrazyBeat.Contains((int)hitobject.StartTime+1) || CrazyBeat.Contains((int)hitobject.StartTime -1)|| CrazyBeat.Contains((int)hitobject.StartTime -2)|| CrazyBeat.Contains((int)hitobject.StartTime +2)){
                    var dropCircle = HitObjectlayer.CreateSprite("sb/q2.png", OsbOrigin.Centre, hitobject.Position);
                    dropCircle.Color(hitobject.StartTime, Color4.Black);
                    dropCircle.Scale(hitobject.StartTime, hitobject.StartTime+1000, 0, 1);
                    dropCircle.Fade(hitobject.StartTime, hitobject.StartTime+400, 1, 0);
                }
                if(CrazyBeatQuatro.Contains((int)hitobject.StartTime) || CrazyBeatQuatro.Contains((int)hitobject.StartTime+1) || CrazyBeatQuatro.Contains((int)hitobject.StartTime -1) || CrazyBeatQuatro.Contains((int)hitobject.StartTime -2) || CrazyBeatQuatro.Contains((int)hitobject.StartTime +2)){
                    var dropLine = HitObjectlayer.CreateSprite("sb/pixel.png", OsbOrigin.Centre, hitobject.Position);
                    dropLine.ScaleVec(hitobject.StartTime, hitobject.StartTime+tick(0,1), 2, 854/2, 0, 854/2);
                    dropLine.Color(hitobject.StartTime, Color4.Black);
                    dropLine.Fade(hitobject.StartTime, hitobject.StartTime+tick(0,1), 1, 0);
                    dropLine.Rotate(hitobject.StartTime, hitobject.StartTime+tick(0,1), MathHelper.DegreesToRadians(Random(-7,7)), MathHelper.DegreesToRadians(Random(-7,7)));
                }
                if (prevObject != null) {
                    hSprite.MoveY(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition.Y, hitobject.Position.Y);
                    //outline.Move(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition, hitobject.Position);
                    cursor.Move(prevObject.EndTime, hitobject.StartTime, prevObject.EndPosition, hitobject.Position);
                    //hSprite.Scale(OsbEasing.In, hitobject is OsuSlider ? hitobject.StartTime : prevObject.EndTime, hitobject.EndTime, SpriteScale, SpriteScale * 0.6);

                    for(int i = 0; i < trailCount; i++){
                        OsbSprite trail = (OsbSprite) cursorTrail[i];
                        trail.Move(prevObject.EndTime + (i+1)*20, hitobject.StartTime + (i+1)*20, prevObject.EndPosition, hitobject.Position);
                        trail.Color(hitobject.StartTime + (i+1)*20, Color4.Black);
                        trail.Fade(hitobject.StartTime, 0.33);
                    }
                
                
                }
                cursor.Color(hitobject.StartTime, Color4.White);

                if (hitobject is OsuSlider){
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    var slider = (OsuSlider) hitobject;                    
                    
                    while (true){
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = hSprite.PositionAt(startTime);
                        var cursorPos = cursor.PositionAt(startTime);
                        hSprite.MoveY(startTime, endTime, startPosition.Y, hitobject.PositionAtTime(endTime).Y);
                        //outline.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));
                        cursor.Move(startTime, endTime, cursorPos, hitobject.PositionAtTime(endTime));

                        for(int i = 0; i < trailCount; i++){
                            OsbSprite trail = (OsbSprite) cursorTrail[i];
                            trail.Move(startTime + (i+1)*20, endTime + (i+1)*20, cursorPos, hitobject.PositionAtTime(endTime));
                        }

                        if (complete) break;
                        startTime += timestep;
                    }
                }
                prevObject = hitobject;
            }

            var heartSprite = layer.CreateSprite(HeartGrow, OsbOrigin.Centre, new Vector2(320,240));
            heartSprite.Scale((OsbEasing)6, 198228, 199111, 0.2, 20);
            heartSprite.ColorHsb((OsbEasing)6,198228, 199111, 0, 1, 1, 0, 0, 1);
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
