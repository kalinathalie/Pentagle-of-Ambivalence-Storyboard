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
    public class GayPart : StoryboardObjectGenerator{

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 186405;

        [Configurable]
        public string BG = "sb/beauty.png";

        [Configurable]
        public string PinkBG = "sb/beauty_pink.png";

        [Configurable]
        public string Pixel = "sb/pixel.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string HeartParticle = "sb/heart.png";

        [Configurable]
        public Color4 Color = Color4.White;

        public override void Generate(){
            var layer = GetLayer("GayPart");
            var BGbitmap = GetMapsetBitmap(PinkBG);

            var backgroundPink = layer.CreateSprite(PinkBG,OsbOrigin.Centre);
            backgroundPink.Scale(StartTime, 570.0f / BGbitmap.Height);
            backgroundPink.Fade(EndTime, EndTime+tick(0,(double)1/(double)2), 1,0.5);

            var background = layer.CreateSprite(BG,OsbOrigin.Centre);
            background.Scale(StartTime, 570.0f / BGbitmap.Height);
            double lastrng = 0.0;
            for(int x = StartTime; x<=EndTime; x+=(int)tick(0,1.2)){
                double rng = Random(0,0.35);
                background.Fade(x, x+tick(0,1), lastrng, rng);
                lastrng = rng;
            }

            var flash = layer.CreateSprite(Flash,OsbOrigin.Centre);
            flash.Scale(StartTime, 480.0f / BGbitmap.Height);
            flash.Fade(StartTime, StartTime+tick(0,(double)1/(double)8), 0.5,0);

            flash.Fade(EndTime, EndTime+tick(0,(double)1/(double)2), 0,0.5);

            for(int x = -107; x<=780;x+=40){
                for(int y=0;y<=500;y+=40){
                    var pixel = layer.CreateSprite(Pixel,OsbOrigin.Centre, new Vector2(x, y));
                    pixel.Fade(StartTime, 0.5);
                    pixel.Scale((OsbEasing)8, StartTime, StartTime+tick(0,(double)1/(double)8), 20, 0);
                    pixel.Rotate((OsbEasing)8, StartTime, StartTime+tick(0,(double)1/(double)8), MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(90));
                    pixel.Fade(StartTime, StartTime+tick(0,(double)1/(double)8), 1, 0.4);
                }
            }

            var color = Color;
            var ColorVariance = MathHelper.Clamp(0.6, 0, 1);
            var hsba = Color4.ToHsl(color);
            var sMin = Math.Max(0, hsba.Y - ColorVariance * 0.5f);
            var sMax = Math.Min(sMin + ColorVariance, 1);
            var vMin = Math.Max(0, hsba.Z - ColorVariance * 0.5f);
            var vMax = Math.Min(vMin + ColorVariance, 1);

            for(double x = StartTime; x <EndTime; x+=tick(0,2)){

                Vector2 Pos1 = new Vector2(Random(-100, 720), 490);
                Vector2 Pos2 = new Vector2(Pos1.X+Random(-100, 100), -50);

                var heart1 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);

                color = Color4.FromHsl(new Vector4(
                    hsba.X,
                    (float)Random(sMin, sMax),
                    (float)Random(vMin, vMax),
                    hsba.W));

                heart1.Color(x, color);
                heart1.Fade((OsbEasing)9, x+tick(0,1), EndTime, 1, 0);
                heart1.Scale(x, Random(0.7, 2));
                heart1.Rotate(x, x+tick(0,(double)1/(double)8), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));      
                heart1.Move(x, x+tick(0,(double)1/(double)8), Pos1, Pos2);

            }
            var droptime = EndTime;
            var LineEndTime = droptime+tick(0,(double)1/(double)2);
            for(int x = 0;x<=3;x++){
                var pixel = layer.CreateSprite(Pixel,OsbOrigin.Centre, new Vector2(220*x, 240));
                pixel.ScaleVec(droptime, droptime+tick(0,2), 0, 854/2, 110, 854/2);
                pixel.Fade(droptime, droptime+tick(0,2), 1, 0.5);
                pixel.Fade(droptime+tick(0,2), LineEndTime, 0.5, 1);
                droptime+=(int)tick(0,2);
            }

        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
