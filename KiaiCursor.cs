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
    public class KiaiCursor : StoryboardObjectGenerator{
        
        [Configurable]
        public string Line = "sb/pixel.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string Heart = "";

        public override void Generate(){

            var HitObjectlayer = GetLayer("HitObject");

            var heartBeat = new List<int>();
            heartBeat.Add(65699);
            heartBeat.Add(67111);
            heartBeat.Add(69934);
            heartBeat.Add(71346);
            heartBeat.Add(72758);
            heartBeat.Add(75581);
            heartBeat.Add(76993);
            heartBeat.Add(78405);
            heartBeat.Add(79816);
            heartBeat.Add(81228);
            heartBeat.Add(84052);
            heartBeat.Add(126405);
            heartBeat.Add(127816);
            heartBeat.Add(129228);
            heartBeat.Add(130640);
            heartBeat.Add(132052);
            heartBeat.Add(133464);
            heartBeat.Add(134875);
            heartBeat.Add(136287);
            heartBeat.Add(137699);
            heartBeat.Add(139111);
            heartBeat.Add(140522);
            heartBeat.Add(141934);
            heartBeat.Add(143346);
            heartBeat.Add(144758);
            heartBeat.Add(146169);
            heartBeat.Add(147581);
            heartBeat.Add(205464);
            heartBeat.Add(206875);
            heartBeat.Add(208287);
            heartBeat.Add(209699);
            heartBeat.Add(211111);
            heartBeat.Add(212522);
            heartBeat.Add(213934);
            heartBeat.Add(215346);
            heartBeat.Add(216758);
            heartBeat.Add(218169);
            heartBeat.Add(219581);
            heartBeat.Add(220993);
            heartBeat.Add(222405);
            heartBeat.Add(223816);
            heartBeat.Add(225228);
            heartBeat.Add(226640);

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ( (hitobject.StartTime >= StartTime+tick(0,(double)1/(double)11)-5 && hitobject.StartTime < StartTime+tick(0,(double)1/(double)16)) || (hitobject.StartTime > EndTime-tick(0,(double)1/(double)5) && hitobject.StartTime<=EndTime ) ){
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

                if((hitobject.StartTime >= StartTime+tick(0,(double)1/(double)6) && hitobject.StartTime <= StartTime+tick(0,(double)1/(double)8)+2 ) ||
                    (hitobject.StartTime >= StartTime+tick(0,(double)1/(double)28) && hitobject.StartTime <= StartTime+tick(0,(double)1/(double)32)+2 ) ||
                    (hitobject.StartTime >= StartTime+tick(0,(double)1/(double)40) && hitobject.StartTime <= StartTime+tick(0,(double)1/(double)48)+2 ) ||
                    (hitobject.StartTime >= StartTime+tick(0,(double)1/(double)52) && hitobject.StartTime <= StartTime+tick(0,(double)1/(double)53)+2 ) ||
                    (hitobject.StartTime >= StartTime+tick(0,(double)1/(double)22) && hitobject.StartTime <= StartTime+tick(0,(double)1/(double)24)+2)){
                    var heartCircle = HitObjectlayer.CreateSprite(Heart, OsbOrigin.Centre, hitobject.Position);
                    heartCircle.Scale(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 1.4);
                    heartCircle.Fade(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 0);
                    heartCircle.Rotate(hitobject.StartTime, MathHelper.DegreesToRadians(Random(-25,25)));
                    heartCircle.Color(hitobject.StartTime, hitobject.Color);
                }

                if(heartBeat.Contains((int)hitobject.StartTime) || heartBeat.Contains((int)hitobject.StartTime+1) || heartBeat.Contains((int)hitobject.StartTime-1)){
                    var hSprite2 = HitObjectlayer.CreateSprite(Line, OsbOrigin.Centre);
                    hSprite2.MoveY(hitobject.StartTime, hitobject.Position.Y);
                    hSprite2.ScaleVec((OsbEasing)7, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 854/2, 30, 854/2, 0);
                    hSprite2.Fade((OsbEasing)7, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 0);

                    for(int x = 0; x<=15;x+=1){
                        var heart = HitObjectlayer.CreateSprite(Heart, OsbOrigin.Centre);
                        heart.Scale(hitobject.StartTime, 0.4);
                        heart.Color(hitobject.StartTime, new Color4(255, 100, 100, 255));
                        heart.Fade(OsbEasing.InQuad, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1,0);
                        heart.Move(OsbEasing.OutQuart, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), hitobject.Position.X, hitobject.Position.Y, hitobject.Position.X+(double)Random(-175,175), hitobject.Position.Y+(double)Random(-30,30));
                        heart.Rotate(hitobject.StartTime, MathHelper.DegreesToRadians(Random(-30,30)));
                    }
                }
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
