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
    public class SecondVerso : StoryboardObjectGenerator{
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public bool LeftAngle = true;

        [Configurable]
        public string Heart = "sb/heart2.png";

        [Configurable]
        public string PixelBG = "sb/pixel.png";

        [Configurable]
        public string vigBG = "sb/vig.png";

        public override void Generate(){
            var layer = GetLayer("SecondVerso");
            var HitObjectlayer = GetLayer("HitObject");
		    
            var pixel = layer.CreateSprite(PixelBG,OsbOrigin.Centre);
            pixel.Scale(StartTime, 854/2);
            pixel.Fade(StartTime, StartTime+tick(0,1), 1, 0.1);
            pixel.Fade(StartTime+tick(0,1), EndTime, 1, 1);
            if(!LeftAngle){
                pixel.Color(StartTime+tick(0,1), 0.8f,1f,0.8f);
            }else{
                if(StartTime==46640){
                    pixel.Color(StartTime+tick(0,1), 0.8f,0.8f,1f);
                }else{
                    pixel.Color(StartTime+tick(0,1), 1f,0.8f,0.8f);
                }
            }

            if(EndTime==52287){
                var alt=0;
                var anda = -80;
                for(int x = 51934; x<=EndTime; x+=(int)tick(0,4)){
                    var pixelDrop = layer.CreateSprite(PixelBG,OsbOrigin.Centre);
                    pixelDrop.ScaleVec(x, 370, 210);
                    pixelDrop.Color(x, 0, 0, 0);
                    if(alt%2==0) pixelDrop.Rotate(x, MathHelper.DegreesToRadians(82)); else pixelDrop.Rotate(x, MathHelper.DegreesToRadians(-82));
                    if(alt%2==0) pixelDrop.Move(x, x+tick(0,2.5), anda, -300, anda+80, 250); else pixelDrop.Move(x, x+tick(0,2.5), anda+15, 700, anda+80, 250);
                    pixelDrop.Fade(x, EndTime-1, 1, 1);
                    pixelDrop.Fade(EndTime, 0);
                    alt+=1;
                    anda+=240;
                }
                
            }

            int count = 0;
            for(double tempo = StartTime; tempo <= EndTime; tempo+=tick(0,(double)1/(double)4)){
                count+=1;
                for(int y = 0; y <= 6; y+=1){
                    for(int x = 0; x<=9; x+=1){
                        var heart = layer.CreateSprite(Heart,OsbOrigin.Centre);
                        if(!LeftAngle){
                            heart.Color(tempo, 0.4f,1f,0.4f);
                        }else{
                            if(StartTime==46640){
                                heart.Color(tempo, 0.4f,0.4f,1f);
                            }else{
                                heart.Color(tempo, 1f,0.4f,0.4f);
                            }
                        }
                        heart.Scale(tempo, 0.8);
                        if(count == 4){
                            heart.Fade(EndTime-tick(0,(double)1/(double)4), EndTime-tick(0,1), 1, 0.2);
                            heart.Fade(EndTime-tick(0,1), EndTime, 0.2, 0);
                        }else{
                            heart.Scale(OsbEasing.OutCirc, tempo+tick(0,(double)1/(double)2), tempo+tick(0,(double)1/(double)2)+tick(0,2), 0.8, 0.92);
                            heart.Scale(tempo+tick(0,(double)1/(double)2)+tick(0,2), tempo+tick(0,(double)1/(double)2)+tick(0,2)+tick(0,4), 0.92, 0.8);
                            heart.Scale(OsbEasing.OutCirc, tempo+tick(0,(double)1/(double)2)+tick(0,2)+tick(0,4), tempo+tick(0,(double)1/(double)2)+tick(0,2)+tick(0,4)+tick(0,2), 0.8, 0.92);
                            heart.Scale(tempo+tick(0,(double)1/(double)2)+tick(0,2)+tick(0,4)+tick(0,2), tempo+tick(0,(double)1/(double)2)+tick(0,2)+tick(0,4)+tick(0,2)+tick(0,4), 0.92, 0.8);
                            heart.Scale(OsbEasing.OutCirc, tempo+tick(0,(double)1/(double)3)+tick(0,2), tempo+tick(0,(double)1/(double)4), 0.8, 0.92);
                            //comeco
                            heart.Scale(tempo, tempo+tick(0,4), 0.92, 0.8);

                        }
                        if(tempo==StartTime){
                            heart.Fade(StartTime+tick(0, (double)1/(double)1), StartTime+tick(0, (double)1/(double)2), 0, 1);
                        }
                        if(y%2==0){
                            if(LeftAngle){
                                heart.Move(tempo, tempo+tick(0,(double)1/(double)4), -150 +(x*100), -50 + y*80 + x*12, -150 +(x*100) +100, -50 + y*80 + x*12 +12);
                            }else{
                                heart.Move(tempo, tempo+tick(0,(double)1/(double)4), -150 +(x*100), +50 + y*80 + x*-12, -150 +(x*100) +100, +50 + y*80 + x*-12 -12);
                            }
                            heart.Rotate(tempo, tempo+tick(0,(double)1/(double)4), MathHelper.DegreesToRadians(x*45), MathHelper.DegreesToRadians(x*45 + 45));
                        }else{
                            if(LeftAngle){
                                heart.Move(tempo, tempo+tick(0,(double)1/(double)4), -150 +(x*100) +100, -50 + y*80 + x*12 + 12, -150 +(x*100), -50 + y*80 + x*12);
                            }else{
                                heart.Move(tempo, tempo+tick(0,(double)1/(double)4), -150 +(x*100) +100, +50 + y*80 + x*-12 - 12, -150 +(x*100), +50 + y*80 + x*-12);
                            }
                            
                            heart.Rotate(tempo, tempo+tick(0,(double)1/(double)4), MathHelper.DegreesToRadians(x*45 + 45), MathHelper.DegreesToRadians(x*45));
                        }
                    }
                }
            }

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if (hitobject.StartTime < EndTime-tick(0,(double)1/(double)2) || EndTime <= hitobject.StartTime)
                    continue;
                var heartCircle = HitObjectlayer.CreateSprite("sb/heart.png", OsbOrigin.Centre, hitobject.Position);
                heartCircle.Scale(hitobject.StartTime, EndTime+tick(0,1), 1, 1.4);
                heartCircle.Fade(hitobject.StartTime, EndTime+tick(0,1), 1, 0.8);
                heartCircle.Rotate(hitobject.StartTime, MathHelper.DegreesToRadians(Random(-25,25)));
                heartCircle.Color(hitobject.StartTime, hitobject.Color);
            }

            var vig = layer.CreateSprite(vigBG,OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap(vigBG);

            vig.Fade(StartTime, EndTime, 1,1);
            vig.Scale(StartTime, 540.0f / bitmap.Height);
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
