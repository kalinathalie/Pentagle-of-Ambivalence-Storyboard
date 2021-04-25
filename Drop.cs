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
    public class Drop : StoryboardObjectGenerator{

        [Configurable]
        public string GrayBG = "";

        [Configurable]
        public string BlurGrayBG = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string BigHeart = "sb/heart2.png";

        [Configurable]
        public string Heart = "sb/heart.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string Line = "sb/pixel.png";

        public override void Generate(){

            var layer = GetLayer("Intro");
            var HitObjectlayer = GetLayer("HitObject");

            var background = layer.CreateSprite(GrayBG, OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap(GrayBG);
            background.Fade(StartTime, StartTime+tick(0,(double)1/(double)2), 0, 0.5);
            background.Fade(StartTime+tick(0,(double)1/(double)2), EndTime, 0.5, 0);
            background.Move(StartTime, EndTime, 600, 250, 250, 400);
            background.Scale(StartTime, 900.0f / bitmap.Height);

            background.Rotate(StartTime, MathHelper.DegreesToRadians(-7));

            var blur_background = layer.CreateSprite(BlurGrayBG, OsbOrigin.Centre);
            blur_background.Fade(StartTime, EndTime, 0, 1);
            blur_background.Move(StartTime, EndTime, 600, 250, 250, 400);
            blur_background.Scale(StartTime, 900.0f / bitmap.Height);

            blur_background.Rotate(StartTime, MathHelper.DegreesToRadians(-7));

            var flash = layer.CreateSprite(Flash, OsbOrigin.Centre);
            for(double flashPump = StartTime+tick(0,(double)1/(double)2); flashPump <= EndTime; flashPump+=tick(0,(double)1/(double)4)){
                flash.Fade(OsbEasing.OutQuad, flashPump, flashPump+tick(0,(double)1/(double)2), 0.6, 0);
            }

            double head_x = 0;
            double head_y = 0;
            double tail_x = 0;
            double tail_y = 0;
            int run_rhythm = 0;
            var to_white_color = new Color4(255, 255, 255, 255);
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

                if(hitobject.StartTime>=EndTime-tick(0,(double)1/(double)2)){
                    hSprite.Rotate(OsbEasing.InQuart, hitobject.StartTime, EndTime, 0, MathHelper.DegreesToRadians(180));
                    hSprite.ScaleVec(OsbEasing.InQuart, hitobject.StartTime, EndTime, 30, 1000, 1000, 1000);
                }
                
                
                var big_heart = HitObjectlayer.CreateSprite(BigHeart, OsbOrigin.Centre);
                var random_angle = (run_rhythm%2==0) ? Random(330, 360) : Random(180, 210);

                double heart_start = hitobject.StartTime-tick(0, 1.5);
                double heart_end = hitobject.StartTime+tick(0, 1.5);
                head_x = hitobject.Position.X + 952*Math.Cos(MathHelper.DegreesToRadians(random_angle));
                head_y = hitobject.Position.Y + 952*Math.Sin(MathHelper.DegreesToRadians(random_angle));
                tail_x = 2*hitobject.Position.X - head_x;
                tail_y = 2*hitobject.Position.Y - head_y;

                big_heart.Move(heart_start, heart_end, head_x, head_y, tail_x, tail_y);
                big_heart.Fade(heart_start, 1);
                big_heart.Color(heart_start, new Color4(255, 100, 100, 255));
                big_heart.Rotate(heart_start, heart_end, MathHelper.DegreesToRadians(random_angle), MathHelper.DegreesToRadians(random_angle+200));
                
                for(int x = 0; x<=8;x+=1){
                    var heart = HitObjectlayer.CreateSprite(Heart, OsbOrigin.Centre);
                    heart.Scale(hitobject.StartTime, 0.3);
                    heart.Color(OsbEasing.InQuad, hitobject.StartTime, EndTime, new Color4(255, 100, 100, 255), to_white_color);
                    heart.Fade(OsbEasing.InQuad, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)3), 1,0);
                    heart.Move(OsbEasing.OutQuart, hitobject.StartTime, hitobject.StartTime+tick(0,(double)1/(double)3), hitobject.Position.X, hitobject.Position.Y, hitobject.Position.X+(double)Random(-50,50), hitobject.Position.Y+(double)Random(-50,50));
                    heart.Rotate(hitobject.StartTime, MathHelper.DegreesToRadians(Random(-30,30)));


                }
                

                run_rhythm+=1;
            } 
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}