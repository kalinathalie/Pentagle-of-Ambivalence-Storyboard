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
    public class CalmPart : StoryboardObjectGenerator{

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 186405;

        [Configurable]
        public int MidTime = 165228;

        [Configurable]
        public string BG = "sb/beauty.png";

        [Configurable]
        public string GrayBG = "sb/beauty_gray.png";

        [Configurable]
        public string PinkBG = "sb/beauty_pink.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string Pixel = "sb/pixel.png";

        [Configurable]
        public string VigBG = "sb/vig.png";

        public override void Generate(){
            var layer = GetLayer("CalmPart");
            var BGbitmap = GetMapsetBitmap(GrayBG);

            var flash = layer.CreateSprite(Flash,OsbOrigin.Centre);
            flash.Scale(StartTime, 480.0f / BGbitmap.Height);
            flash.Fade(StartTime, MidTime, 1,0);
            
            var gray_background = layer.CreateSprite(GrayBG,OsbOrigin.Centre);
            gray_background.Scale(StartTime, 570.0f / BGbitmap.Height);
            gray_background.Fade(StartTime, MidTime, 0,0.7);
            gray_background.Fade(MidTime, MidTime+tick(0,(double)1/(double)2), 0,0.7);
            gray_background.Fade(MidTime+tick(0,(double)1/(double)2), EndTime, 0.7,0.4);
            gray_background.Fade(EndTime, EndTime+tick(0,(double)1/(double)2), 0.4,0.4);

            var background = layer.CreateSprite(BG,OsbOrigin.Centre);
            background.Scale(MidTime-tick(0,(double)1/(double)16), 570.0f / BGbitmap.Height);
            background.Fade(MidTime-tick(0,(double)1/(double)16), MidTime, 0,0.6);
            background.Fade(MidTime, MidTime+tick(0,(double)1/(double)2), 0.6,0);

            for(int x = -107; x<=780;x+=40){
                for(int y=0;y<=500;y+=40){
                    var pixel = layer.CreateSprite(Pixel,OsbOrigin.Centre, new Vector2(x, y));
                    pixel.Fade(MidTime, 0.5);
                    pixel.Scale((OsbEasing)7, MidTime, MidTime+tick(0,(double)1/(double)4), 0, 20);
                    pixel.Rotate((OsbEasing)7, MidTime, MidTime+tick(0,(double)1/(double)4), MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(90));
                    pixel.Fade( MidTime+tick(0,(double)1/(double)4),  MidTime+tick(0,(double)1/(double)8), 0.5, 0);
                }
            }

            var pixel1 = layer.CreateSprite(Pixel,OsbOrigin.CentreRight);
            var pixel2 = layer.CreateSprite(Pixel,OsbOrigin.CentreLeft);
            pixel1.Scale(EndTime, 854/2);
            pixel2.Scale(EndTime, 854/2);

            pixel1.Color(EndTime, Color4.Black);
            pixel2.Color(EndTime, Color4.Black);
            pixel1.MoveX((OsbEasing)10, EndTime, EndTime+tick(0,(double)1/(double)2), -107, 320);
            pixel2.MoveX((OsbEasing)10, EndTime, EndTime+tick(0,(double)1/(double)2), 746, 320);


            var vig = layer.CreateSprite(VigBG,OsbOrigin.Centre);
            var Vigbitmap = GetMapsetBitmap(GrayBG);
            vig.Fade(StartTime, StartTime+tick(0,(double)1/(double)8), 0,0.65);
            vig.Fade(StartTime+tick(0,(double)1/(double)8), EndTime, 0.65,0.65);
            vig.Scale(StartTime, 330.0f / Vigbitmap.Height);


        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
