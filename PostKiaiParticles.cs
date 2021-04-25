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
    public class PostKiaiParticles : StoryboardObjectGenerator{

        [Configurable]
        public string HeartParticle = "sb/heart.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public Color4 Color = Color4.White;

        public override void Generate(){

            var layer = GetLayer("PostKiaiParticles");

            var color = Color;
            var ColorVariance = MathHelper.Clamp(0.6, 0, 1);
            var hsba = Color4.ToHsl(color);
            var sMin = Math.Max(0, hsba.Y - ColorVariance * 0.5f);
            var sMax = Math.Min(sMin + ColorVariance, 1);
            var vMin = Math.Max(0, hsba.Z - ColorVariance * 0.5f);
            var vMax = Math.Min(vMin + ColorVariance, 1);

            for(double x = StartTime; x <StartTime+tick(0,(double)1/(double)16); x+=tick(0,1)){

                Vector2 initPos1 = new Vector2(Random(-70, 130), -10);
                Vector2 initPos2 = new Vector2(Random(490, 690), -10);

                var heart1 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);
                var heart2 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);

                color = Color4.FromHsl(new Vector4(
                    hsba.X,
                    (float)Random(sMin, sMax),
                    (float)Random(vMin, vMax),
                    hsba.W));

                heart1.Color(x, color);
                heart2.Color(x, color);

                heart1.Fade((OsbEasing)9, x+tick(0,1), x+tick(0,(double)1/(double)8), 1, 0);
                heart2.Fade((OsbEasing)9, x+tick(0,1), x+tick(0,(double)1/(double)8), 1, 0);

                heart1.Scale(x, Random(0.7, 0.9));
                heart2.Scale(x, Random(0.7, 0.9));

                heart1.Rotate(x, x+tick(0,(double)1/(double)8), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                heart2.Rotate(x, x+tick(0,(double)1/(double)8), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                
                heart1.Move(x, x+tick(0,(double)1/(double)8), initPos1, initPos1+new Vector2(Random(-100, 100), Random(400,500)));
                heart2.Move(x, x+tick(0,(double)1/(double)8), initPos2, initPos2+new Vector2(Random(-100, 100), Random(400,500)));

            }

            for(double x = StartTime+tick(0,(double)1/(double)16); x <StartTime+tick(0,(double)1/(double)32); x+=tick(0,4)){

                Vector2 initPos1 = new Vector2(Random(-70, 130), -10);
                Vector2 initPos2 = new Vector2(Random(490, 690), -10);

                var heart1 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);
                var heart2 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);

                color = Color4.FromHsl(new Vector4(
                    hsba.X,
                    (float)Random(sMin, sMax),
                    (float)Random(vMin, vMax),
                    hsba.W));

                heart1.Color(x, color);
                heart2.Color(x, color);

                heart1.Fade((OsbEasing)9, x+tick(0,1), x+tick(0,(double)1/(double)4), 1, 0);
                heart2.Fade((OsbEasing)9, x+tick(0,1), x+tick(0,(double)1/(double)4), 1, 0);

                heart1.Scale(x, Random(0.7, 0.9));
                heart2.Scale(x, Random(0.7, 0.9));

                heart1.Rotate(x, x+tick(0,(double)1/(double)4), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                heart2.Rotate(x, x+tick(0,(double)1/(double)4), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                
                heart1.Move(x, x+tick(0,(double)1/(double)4), initPos1, initPos1+new Vector2(Random(-100, 100), Random(400,500)));
                heart2.Move(x, x+tick(0,(double)1/(double)4), initPos2, initPos2+new Vector2(Random(-100, 100), Random(400,500)));
                
            }

            for(double x = StartTime+tick(0,(double)1/(double)32); x <StartTime+tick(0,(double)1/(double)48); x+=tick(0,8)){

                Vector2 initPos1 = new Vector2(Random(-70, 130), -10);
                Vector2 initPos2 = new Vector2(Random(490, 690), -10);

                var heart1 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);
                var heart2 = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);

                color = Color4.FromHsl(new Vector4(
                    hsba.X,
                    (float)Random(sMin, sMax),
                    (float)Random(vMin, vMax),
                    hsba.W));

                heart1.Color(x, color);
                heart2.Color(x, color);

                heart1.Fade((OsbEasing)9, x+tick(0,1), x+tick(0,(double)1/(double)2), 1, 0);
                heart2.Fade((OsbEasing)9, x+tick(0,1), x+tick(0,(double)1/(double)2), 1, 0);

                heart1.Scale(x, Random(0.7, 0.9));
                heart2.Scale(x, Random(0.7, 0.9));

                heart1.Rotate(x, x+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                heart2.Rotate(x, x+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                
                heart1.Move(x, x+tick(0,(double)1/(double)2), initPos1, initPos1+new Vector2(Random(-100, 100), Random(400,500)));
                heart2.Move(x, x+tick(0,(double)1/(double)2), initPos2, initPos2+new Vector2(Random(-100, 100), Random(400,500)));
                
            }
            int run = 0;
            List<double> curveCircle = CalculateCurve(315, 240, 520.0);
            Vector2 initPos = new Vector2(315, 240);
            for(double x = StartTime+tick(0,(double)1/(double)48); x <StartTime+tick(0,(double)1/(double)51); x+=tick(0,4)){
                for(int y = 1; y<=4 ; y+=1){
                    color = Color4.FromHsl(new Vector4(
                        hsba.X,
                        (float)Random(sMin, sMax),
                        (float)Random(vMin, vMax),
                        hsba.W));

                    var heart = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);
                    heart.Color(x, color);
                    Vector2 endPos = new Vector2((float)curveCircle[(run+90*y)%360], (float)curveCircle[(run+1+90*y)%360]);
                    heart.Move(x, x+tick(0,0.75), initPos, endPos);
                    run+=2;
                }
            }
            run = 0;
            for(double x = StartTime+tick(0,(double)1/(double)48); x <StartTime+tick(0,(double)1/(double)51); x+=tick(0,4)){
                for(int y = 1; y<=4 ; y+=1){
                    color = Color4.FromHsl(new Vector4(
                        (float)Random(sMin, sMax),
                        (float)Random(vMin, vMax),
                        hsba.W,
                        hsba.X));

                    var heart = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);
                    heart.Color(x, color);
                    Vector2 endPos = new Vector2((float)curveCircle[(run+90*y)%360], (float)curveCircle[(run+1+90*y)%360]);
                    heart.Move(StartTime+tick(0,(double)1/(double)51)+run*2, StartTime+tick(0,(double)1/(double)52), endPos, initPos);
                    run+=2;
                }
            }
            run = 360;
            for(double x = StartTime+tick(0,(double)1/(double)52); x <StartTime+tick(0,(double)1/(double)55); x+=tick(0,4)){
                for(int y = 1; y<=4 ; y+=1){
                    color = Color4.FromHsl(new Vector4(
                        hsba.X,
                        (float)Random(sMin, sMax),
                        (float)Random(vMin, vMax),
                        hsba.W));

                    var heart = layer.CreateSprite(HeartParticle, OsbOrigin.Centre);
                    heart.Color(x, color);
                    Vector2 endPos = new Vector2((float)curveCircle[(run+90*y)%360], (float)curveCircle[(run+1+90*y)%360]);
                    heart.Move((OsbEasing)4, x, x+tick(0,0.75), initPos, endPos);
                    run-=2;
                }
            }
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
        List<double> CalculateCurve(double x, double y, double radius){
            List<double> curve = new List<double>();
            for(double a = 0; a<=360; a++){
                curve.Add(x+radius*Math.Cos((a)*(Math.PI/90)));
                curve.Add(y+radius*Math.Sin((a)*(Math.PI/90)));
            }
            return curve;
        }
    }
}
