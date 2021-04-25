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
    public class LastKiai : StoryboardObjectGenerator{
        
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string HeadCircle = "sb/head.png";

        [Configurable]
        public string CircleHead = "sb/c2.png";

        [Configurable]
        public string vigBG = "sb/vig.png";

        [Configurable]
        public string PixelBG = "sb/pixel.png";

        [Configurable]
        public string Heart = "sb/heart2.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string BGGray = "sb/bg_gray.jpg";

        [Configurable]
        public string Thank = "sb/thankyou.jpg";

        public override void Generate(){
		    var layer = GetLayer("LastKiai");

            var pixel = layer.CreateSprite(PixelBG,OsbOrigin.Centre);
            pixel.Scale(StartTime, 854/2);
            pixel.Fade(StartTime, StartTime+tick(0,1), 1, 0.6);
            pixel.Fade(StartTime+tick(0,1), EndTime, 1, 1);

            pixel.Color(StartTime+tick(0,1), 1f,0.8f,0.8f);

            int heartCount = 15;
            for(int line = 1; line<=6; line+=1){
                List<double> curveHeart = CalculateCurve(320, 240, 100+ 50.0*line);
                for(int count = 1 ; count <= heartCount; count++){
                    var heart = layer.CreateSprite(Heart,OsbOrigin.Centre);
                    //heart.Fade(tempo,0.6);
                    for(double x = StartTime+tick(0,1); x<=EndTime; x+=tick(0,(double)1/(double)2)){
                        heart.Fade(x, x+tick(0,1),1,0.5);
                    }
                    heart.Fade(EndTime, EndTime+tick(0,1), 0.5, 0);
                    
                    int angleDistance = (int)(360/heartCount)*count;
                    int run = (int)(360/heartCount)*(count-1);
                    int spin = 0;
                    for(double tempo = StartTime; tempo <= EndTime+tick(0,1); tempo+=tick(0,8)){
                        heart.Scale(tempo, 0.66);
                        if(line%2==0){
                            heart.Move(tempo, tempo+tick(0,8), curveHeart[run], curveHeart[run+1], curveHeart[run+2], curveHeart[run+3]);
                            heart.Rotate(tempo, MathHelper.DegreesToRadians(180+247+24*count+mod(spin*2,24)));
                        }
                        else{
                            heart.Move(tempo, tempo+tick(0,8), curveHeart[mod(-run, 360)], curveHeart[mod(-run-1, 360)], curveHeart[mod(-run-2, 360)], curveHeart[mod(-run-3, 360)]);
                            heart.Rotate(tempo, MathHelper.DegreesToRadians(-247-24*count-mod(spin*2,24)));
                        }
                        run+=2;
                        spin+=1;
                        if(run == angleDistance){
                            run = (int)(360/heartCount)*(count-1);
                        }
                    }
                }
            }

            var headSpin = layer.CreateSprite(HeadCircle,OsbOrigin.Centre);
            headSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 0, 1);
            headSpin.Fade(StartTime+tick(0,1), EndTime, 1, 1);
            headSpin.Scale(StartTime, 0.59);
            headSpin.Rotate(StartTime,EndTime, MathHelper.DegreesToRadians(-15),  MathHelper.DegreesToRadians(15));
            headSpin.Rotate(EndTime,EndTime+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(15),  MathHelper.DegreesToRadians(-30));
            

            var circleSpin = layer.CreateSprite(CircleHead,OsbOrigin.Centre);
            circleSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 0, 1);
            circleSpin.Fade(StartTime+tick(0,1), EndTime, 1, 1);
            circleSpin.Scale(StartTime, 0.45);

            headSpin.Fade(EndTime, EndTime+tick(0,(double)1/(double)2), 1, 0);
            circleSpin.Fade(EndTime, EndTime+tick(0,(double)1/(double)2), 1, 0);

            var background_gray = layer.CreateSprite(BGGray,OsbOrigin.Centre);
            var BGbitmap = GetMapsetBitmap(BGGray);
            background_gray.Scale(238640, 480.0f / BGbitmap.Height);
            background_gray.Fade(238640, 249934, 1, 0.2);

            var thankyou = layer.CreateSprite(Thank,OsbOrigin.Centre);
            thankyou.Fade(238640, 249934, 1, 0.2);
            thankyou.Scale(238640, 249934, 0.87, 0.55);
            thankyou.Rotate(238640, 249934, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));

            var vig = layer.CreateSprite(vigBG,OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap(vigBG);

            vig.Fade(StartTime, EndTime, 1,1);
            vig.Scale(StartTime, 540.0f / bitmap.Height);

            vig.Fade(238728, 249934, 1,0);


            int pixelinit = 237934;
            for(int x = 0; x<=7;x+=1){
                var pixel1 = layer.CreateSprite(PixelBG,OsbOrigin.Centre, new Vector2(320, 450-x*60));
                pixel1.ScaleVec(pixelinit, pixelinit+tick(0,4), 854/2, 0, 854/2, 30);
                pixel1.Fade(pixelinit, pixelinit+tick(0,4), 0, 1);
                pixel1.Fade(pixelinit+tick(0,4), 238640, 1, 1);
                pixel1.Fade(238640, 238640+tick(0,(double)1/(double)8), 1, 0);
                pixelinit+=(int)tick(0,4);
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
        int mod(int x, int m) {
            return (x%m + m)%m;
        }
    }
}
