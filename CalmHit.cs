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
    public class CalmHit : StoryboardObjectGenerator{

        [Configurable]
        public string Line = "sb/pixel.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string Heart = "sb/heart2.png";

        [Configurable]
        public string BG = "sb/beauty.png";

        public override void Generate(){
            var HitObjectlayer = GetLayer("HitObject");
            var layer = GetLayer("CalmPart");

            var ClickBeat = new List<int>();
            var background = layer.CreateSprite(BG,OsbOrigin.Centre);
            var BGbitmap = GetMapsetBitmap(BG);
            background.Scale(StartTime, 570.0f / BGbitmap.Height);
            for(double x = StartTime; x<EndTime-5; x+=tick(0,(double)1/(double)4)){
                ClickBeat.Add((int)x);
                ClickBeat.Add((int)(x+tick(0,1)+tick(0,2)));
                ClickBeat.Add((int)(x+tick(0,(double)1/(double)3)));

            }
            foreach(int bgtime in ClickBeat){
                background.Fade(bgtime, bgtime+tick(0,1), 0.3,0);
            }

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if(ClickBeat.Contains((int)hitobject.StartTime) || ClickBeat.Contains((int)hitobject.StartTime+1) || ClickBeat.Contains((int)hitobject.StartTime-1)){
                    var hSprite2 = HitObjectlayer.CreateSprite(Line, OsbOrigin.Centre);
                    int rng = Random(0,2);
                    int rng2 = Random(0,2);
                    if(rng==1){
                        if(rng2==1){
                            hSprite2.ScaleVec(hitobject.StartTime, 854/2, 30);
                            hSprite2.MoveY(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), hitobject.Position.Y, hitobject.Position.Y-50);
                        }else{
                            hSprite2.ScaleVec(hitobject.StartTime, 30, 854/2);
                            hSprite2.MoveX(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), hitobject.Position.X-50, hitobject.Position.X);
                        }
                    }else{
                        if(rng2==1){
                            hSprite2.ScaleVec(hitobject.StartTime, 854/2, 30);
                            hSprite2.MoveY(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), hitobject.Position.Y, hitobject.Position.Y+50);
                        }else{
                            hSprite2.ScaleVec(hitobject.StartTime, 30, 854/2);
                            hSprite2.MoveX(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), hitobject.Position.X+50, hitobject.Position.X);
                        }
                    }
                    hSprite2.ColorHsb(hitobject.StartTime, Random(300,360), 1, 1);
                    hSprite2.Fade((OsbEasing)7, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 0.6, 0);
                }

            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
