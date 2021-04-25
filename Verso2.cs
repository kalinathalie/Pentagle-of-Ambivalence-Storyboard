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
    public class Verso2 : StoryboardObjectGenerator{
        [Configurable]
        public string HeartBlur = "";

        [Configurable]
        public string blurBG = "";

        [Configurable]
        public string vigBG = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        public override void Generate(){
            var layer = GetLayer("Verso2");
            var bitmap = GetMapsetBitmap(blurBG);
            var bgSprite = layer.CreateSprite(blurBG,OsbOrigin.Centre);
            bgSprite.Scale(StartTime, 480.0f / bitmap.Height);
            bgSprite.Fade(StartTime, EndTime, 0.8, 0.8);

            var vig = layer.CreateSprite(vigBG,OsbOrigin.Centre);
            vig.Scale(StartTime, 500.0f / bitmap.Height);
            vig.Fade(StartTime, EndTime, 1,1);
		    

            var heart = layer.CreateSprite(HeartBlur,OsbOrigin.Centre);
            heart.Fade(StartTime, 1);
            
            var first = StartTime;
            var second = first+tick(0, (double)1/(double)4);
            var third = second+tick(0, (double)1/(double)4);
            var fourth = third+tick(0, (double)1/(double)4);
            
            heart.Move(first, 170, 300);
            double grow = 1.12;
            heart.Rotate(first, MathHelper.DegreesToRadians(7));
            for(double x = first; x <= second-10; x+=tick(0, 2)){
                heart.Scale(x, x+tick(0, 4), grow, grow*2);
                heart.Scale(x+tick(0, 4), x+tick(0, 2), grow*2, grow);
                grow *=1.2;
            }

            heart.Move(second, 400, 120);
            grow = 1.12;
            heart.Rotate(second, MathHelper.DegreesToRadians(-7));
            for(double x = second; x <= third-10; x+=tick(0, 2)){
                heart.Scale(x, x+tick(0, 4), grow, grow*2);
                heart.Scale(x+tick(0, 4), x+tick(0, 2), grow*2, grow);
                grow *=1.2;
            }

            heart.Move(third, 480, 400);
            grow = 1.12;
            heart.Rotate(third, MathHelper.DegreesToRadians(7));
            for(double x = third; x <= fourth-10; x+=tick(0, 2)){
                heart.Scale(x, x+tick(0, 4), grow, grow*2);
                heart.Scale(x+tick(0, 4), x+tick(0, 2), grow*2, grow);
                grow *=1.2;
            }

            heart.Move(fourth, 270, 200);
            grow = 1.5;
            heart.Rotate(fourth, MathHelper.DegreesToRadians(-7));
            for(double x = fourth; x <= 34460; x+=tick(0, 2)){
                heart.Scale(x, x+tick(0, 4), grow, grow*2);
                heart.Scale(x+tick(0, 4), x+tick(0, 2), grow*2, grow);
                grow *=1.4;
            }
            for(double x = 34464; x <= 35169; x+=tick(0, 1)){
                heart.Scale(x, x+tick(0, 2), grow, grow*2);
                heart.Scale(x+tick(0, 2), x+tick(0, 1), grow*2, grow);
                grow *=1.5;
            }
            heart.Scale(35170, 35346, grow, grow*4);
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
