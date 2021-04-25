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
    public class Ultimo : StoryboardObjectGenerator{

        [Configurable]
        public string GrayBG = "sb/bg_sad.png";

        [Configurable]
        public string Pixel = "sb/pixel.png";

        [Configurable]
        public string VigBG = "sb/vig.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 120052;

        public override void Generate(){
            var layer = GetLayer("Ultimo");
            var HitObjectlayer = GetLayer("HitObject");
            var BGbitmap = GetMapsetBitmap(GrayBG);
            var background = layer.CreateSprite(GrayBG,OsbOrigin.Centre);
            background.Scale(StartTime, 480.0f / BGbitmap.Height);
            background.Fade(StartTime, 0.7);
            background.Fade(EndTime, EndTime+tick(0,(double)1/(double)2), 0.7, 0.3);

            background.Fade(120052, 120052+tick(0,(double)1/(double)2), 0.7, 0.3);
            background.Fade(120758, 120758+tick(0,(double)1/(double)2), 0.7, 0.3);
            background.Fade(121463, 121463+tick(0,(double)1/(double)1), 0.7, 0.3);
            background.Fade(121816, 121816+tick(0,(double)1/(double)1), 0.7, 0.3);
            background.Fade(122169, 122169+tick(0,(double)1/(double)1), 0.7, 0.3);
            background.Fade(122522, 122522+tick(0,(double)1/(double)1), 0.7, 0.3);
            for(double x = 122875; x<124285;x+=tick(0,2)){
                background.Fade(x, x+tick(0,2), 0.5, 0.2);
            }
            background.Fade(124287, 125699, 1, 0);
            


            var vig = layer.CreateSprite(VigBG,OsbOrigin.Centre);
            var Vigbitmap = GetMapsetBitmap(VigBG);
            vig.Fade(StartTime, 1);
            vig.Fade(124287, 125611, 1,0);
            vig.Scale(StartTime, 540.0f / Vigbitmap.Height);

            for(int x = 0;x<=3;x++){
                var pixel = layer.CreateSprite(Pixel,OsbOrigin.Centre, new Vector2(220*x, 240));
                pixel.Color(StartTime, Color4.Black);
                pixel.ScaleVec((OsbEasing)6, StartTime, StartTime+tick(0,1), 110, 854/2, 0, 854/2);
                pixel.Fade((OsbEasing)6, StartTime, StartTime+tick(0,1), 1, 0.5);
            }

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
                    var dropLine = HitObjectlayer.CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(hitobject.Position.X, 240));
                    dropLine.ScaleVec(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 858/2, 0, 858/2);
                    dropLine.ColorHsb(hitobject.StartTime, Random(300,360), 1, 1);
                    dropLine.Fade(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), 1, 0);
                    dropLine.Rotate(hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(Random(-5,5)), MathHelper.DegreesToRadians(Random(-5,5)));
            }

            int pixelinit = 124287;
            for(int x = 0; x<=4;x+=1){
                for(int y = 0; y<=8;y+=1){
                    var pixel1 = layer.CreateSprite(Pixel,OsbOrigin.Centre, new Vector2(-48+y*122, 60+x*122));
                    pixel1.ScaleVec(pixelinit, pixelinit+tick(0,2), 0, 0, 61, 61);
                    pixel1.Rotate(pixelinit, pixelinit+tick(0,2), MathHelper.DegreesToRadians(90), 0);
                    pixel1.Fade(124287, 125699, 0.3, 0.9);
                    pixel1.ColorHsb(pixelinit, 30*y, 1, 1);
                    pixelinit+=(int)tick(0,10);
                }
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
