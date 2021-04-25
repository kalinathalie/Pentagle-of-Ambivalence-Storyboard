using OpenTK;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Collections.Generic;

namespace StorybrewScripts{
    public class Kiai : StoryboardObjectGenerator{
        
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 10000;

        [Configurable]
        public Vector2 Position = new Vector2(-107, 240);

        [Configurable]
        public float Width = 844;

        [Configurable]
        public int BeatDivisor = 16;

        [Configurable]
        public int BarCount = 96;

        [Configurable]
        public string SpritePath = "sb/bar.png";

        [Configurable]
        public string BlurBG = "sb/blur_bg.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string LightBG = "sb/bg_light.png";

        [Configurable]
        public string CrazyBG = "sb/doidera2.png";

        [Configurable]
        public OsbOrigin SpriteOrigin = OsbOrigin.CentreLeft;

        [Configurable]
        public Vector2 Scale = new Vector2(1, 200);

        [Configurable]
        public int LogScale = 600;

        [Configurable]
        public double Tolerance = 0.2;

        [Configurable]
        public int CommandDecimals = 1;

        [Configurable]
        public float MinimalHeight = 0.05f;

        [Configurable]
        public OsbEasing FftEasing = OsbEasing.InExpo;

        public override void Generate(){
            var layer = GetLayer("Kiai");
            var BGbitmap = GetMapsetBitmap(BlurBG);

            var crazyBG = layer.CreateSprite(CrazyBG,OsbOrigin.Centre);
            crazyBG.Scale(StartTime, 480.0f / BGbitmap.Height);

            var bgSprite = layer.CreateSprite(BlurBG,OsbOrigin.Centre);
            bgSprite.Scale(StartTime, 480.0f / BGbitmap.Height);

            var flash = layer.CreateSprite(Flash, OsbOrigin.Centre);
            var flashBG = layer.CreateSprite(LightBG, OsbOrigin.Centre);

            var endTime = Math.Min(EndTime, (int)AudioDuration);
            var startTime = Math.Min(StartTime, endTime);
            var bitmap = GetMapsetBitmap(SpritePath);

            var heightKeyframes = new KeyframedValue<float>[BarCount];
            for (var i = 0; i < BarCount; i++)
                heightKeyframes[i] = new KeyframedValue<float>(null);

            var fftTimeStep = Beatmap.GetTimingPointAt(startTime).BeatDuration / BeatDivisor;
            var fftOffset = fftTimeStep * 0.2;
            for (var time = (double)startTime; time < endTime; time += fftTimeStep)
            {
                var fft = GetFft(time + fftOffset, BarCount, null, FftEasing);
                for (var i = 0; i < BarCount; i++)
                {
                    var height = (float)Math.Log10(1 + fft[i] * LogScale) * Scale.Y / bitmap.Height;
                    if (height < MinimalHeight) height = MinimalHeight;

                    heightKeyframes[i].Add(time, height);
                }
            }

            var spectrumDropTwoBeats = new List<int>();
            var spectrumDropOneBeat = new List<int>();
            var spectrumDropOneMidBeat = new List<int>();
            var spectrumDropMidBeat = new List<int>();
            var pumpBG = new List<int>();
            var flashSnare = new List<int>();
            if(StartTime==63581){
                spectrumDropTwoBeats.Add(63581);
                spectrumDropTwoBeats.Add(64993);
                spectrumDropTwoBeats.Add(66405);
                spectrumDropTwoBeats.Add(69228);
                spectrumDropTwoBeats.Add(70640);
                spectrumDropTwoBeats.Add(72052);
                spectrumDropTwoBeats.Add(74875);
                spectrumDropTwoBeats.Add(76287);
                spectrumDropTwoBeats.Add(80522);
                spectrumDropOneBeat.Add(82287);
                spectrumDropTwoBeats.Add(83346);
                pumpBG.Add(67463);
                pumpBG.Add(67816);
                pumpBG.Add(68081);
                pumpBG.Add(68346);
                pumpBG.Add(68522);
                pumpBG.Add(68787);
                pumpBG.Add(69052);
                pumpBG.Add(73111);
                pumpBG.Add(73463);
                pumpBG.Add(73993);
                pumpBG.Add(74169);
                pumpBG.Add(74699);
                pumpBG.Add(77699);
                pumpBG.Add(78052);
                pumpBG.Add(78405);
                pumpBG.Add(78758);
                pumpBG.Add(79111);
                pumpBG.Add(79346);
                pumpBG.Add(79816);
                pumpBG.Add(80169);
                pumpBG.Add(81581);
                pumpBG.Add(82287);
                pumpBG.Add(82640);
                pumpBG.Add(82905);
                pumpBG.Add(83169);
                flashSnare.Add(65699);
                flashSnare.Add(67111);
                flashSnare.Add(69934);
                flashSnare.Add(71346);
                flashSnare.Add(72758);
                flashSnare.Add(75581);
                flashSnare.Add(76993);
                flashSnare.Add(78405);
                flashSnare.Add(79816);
                flashSnare.Add(81228);
                flashSnare.Add(84052);
            }
            if(StartTime==125699){
                spectrumDropTwoBeats.Add(125699);
                spectrumDropOneBeat.Add(127463);
                spectrumDropTwoBeats.Add(128522);
                spectrumDropOneBeat.Add(129581);
                spectrumDropOneBeat.Add(130287);
                spectrumDropOneMidBeat.Add(130816);
                spectrumDropTwoBeats.Add(131346);
                spectrumDropOneBeat.Add(133111);
                spectrumDropTwoBeats.Add(134169);
                spectrumDropOneBeat.Add(135228);
                spectrumDropOneBeat.Add(135934);
                for(double x = 136287;x<=138287;x+=tick(0,3)){
                    spectrumDropMidBeat.Add((int)x);
                }
                spectrumDropOneBeat.Add(138758);
                spectrumDropOneMidBeat.Add(139816);
                spectrumDropOneBeat.Add(140875);
                spectrumDropOneBeat.Add(141581);
                spectrumDropOneMidBeat.Add(142111);
                spectrumDropTwoBeats.Add(142640);
                spectrumDropOneBeat.Add(144405);
                spectrumDropTwoBeats.Add(145464);
                spectrumDropOneBeat.Add(146522);
                for(double x = 143346;x<=144052;x+=tick(0,3)){
                    spectrumDropMidBeat.Add((int)x);
                }
                for(double x = 146875;x<=148170;x+=tick(0,3)){
                    spectrumDropMidBeat.Add((int)x);
                }
                pumpBG.Add(126758);
                pumpBG.Add(127463);
                pumpBG.Add(127993);
                pumpBG.Add(128346);
                pumpBG.Add(129581);
                pumpBG.Add(130287);
                pumpBG.Add(130816);
                pumpBG.Add(131169);
                pumpBG.Add(132405);
                pumpBG.Add(133111);
                pumpBG.Add(133640);
                pumpBG.Add(133993);
                pumpBG.Add(135228);
                pumpBG.Add(135934);
                pumpBG.Add(135934);
                for(double x = 136287;x<=138052;x+=tick(0,3)){
                    pumpBG.Add((int)x);
                }
                pumpBG.Add(138758);
                pumpBG.Add(139287);
                pumpBG.Add(139640);
                pumpBG.Add(139816);
                pumpBG.Add(140875);
                pumpBG.Add(141228);
                pumpBG.Add(141581);
                pumpBG.Add(141758);
                pumpBG.Add(142111);
                pumpBG.Add(142464);
                pumpBG.Add(143699);
                pumpBG.Add(144405);
                pumpBG.Add(144934);
                pumpBG.Add(145287);
                pumpBG.Add(146522);
                pumpBG.Add(147228);
                flashSnare.Add(126405);
                flashSnare.Add(127816);
                flashSnare.Add(129228);
                flashSnare.Add(130640);
                flashSnare.Add(132052);
                flashSnare.Add(133464);
                flashSnare.Add(134875);
                flashSnare.Add(136287);
                flashSnare.Add(137699);
                flashSnare.Add(139111);
                flashSnare.Add(140522);
                flashSnare.Add(141934);
                flashSnare.Add(143346);
                flashSnare.Add(144758);
                flashSnare.Add(146169);
                flashSnare.Add(147581);
            }else{
                spectrumDropTwoBeats.Add(204758);
                spectrumDropOneBeat.Add(206522);
                spectrumDropTwoBeats.Add(207581);
                spectrumDropOneBeat.Add(208640);
                spectrumDropOneBeat.Add(209346);
                spectrumDropOneMidBeat.Add(209875);
                spectrumDropTwoBeats.Add(210405);
                spectrumDropOneBeat.Add(212169);
                spectrumDropTwoBeats.Add(213228);
                spectrumDropOneBeat.Add(214287);
                spectrumDropOneBeat.Add(214993);
                for(double x = 215346;x<=217287;x+=tick(0,3)){
                    spectrumDropMidBeat.Add((int)x);
                }
                spectrumDropOneBeat.Add(217816);
                spectrumDropOneMidBeat.Add(218875);
                spectrumDropOneBeat.Add(219934);
                spectrumDropOneBeat.Add(220640);
                spectrumDropOneMidBeat.Add(221169);
                spectrumDropTwoBeats.Add(221699);
                for(double x = 222405;x<=223111;x+=tick(0,3)){
                    spectrumDropMidBeat.Add((int)x);
                }
                spectrumDropOneBeat.Add(223464);
                spectrumDropTwoBeats.Add(224522);
                spectrumDropOneBeat.Add(225581);
                for(double x = 225934;x<=227346;x+=tick(0,3)){
                    spectrumDropMidBeat.Add((int)x);
                }
                pumpBG.Add(205816);
                pumpBG.Add(206522);
                pumpBG.Add(207052);
                pumpBG.Add(207405);
                pumpBG.Add(208640);
                pumpBG.Add(209346);
                pumpBG.Add(209875);
                pumpBG.Add(210228);
                pumpBG.Add(211464);
                pumpBG.Add(212169);
                pumpBG.Add(212699);
                pumpBG.Add(213052);
                pumpBG.Add(214287);
                pumpBG.Add(214993);
                for(double x = 215346;x<=217111;x+=tick(0,3)){
                    pumpBG.Add((int)x);
                }
                pumpBG.Add(217816);
                pumpBG.Add(218346);
                pumpBG.Add(218699);
                pumpBG.Add(218875);
                pumpBG.Add(219934);
                pumpBG.Add(220287);
                pumpBG.Add(220640);
                pumpBG.Add(220816);
                pumpBG.Add(221169);
                pumpBG.Add(221522);
                pumpBG.Add(222758);
                pumpBG.Add(223464);
                pumpBG.Add(223993);
                pumpBG.Add(224346);
                pumpBG.Add(225581);
                pumpBG.Add(226287);
                flashSnare.Add(205464);
                flashSnare.Add(206875);
                flashSnare.Add(208287);
                flashSnare.Add(209699);
                flashSnare.Add(211111);
                flashSnare.Add(212522);
                flashSnare.Add(213934);
                flashSnare.Add(215346);
                flashSnare.Add(216758);
                flashSnare.Add(218169);
                flashSnare.Add(219581);
                flashSnare.Add(220993);
                flashSnare.Add(222405);
                flashSnare.Add(223816);
                flashSnare.Add(225228);
                flashSnare.Add(226640);
            }

            foreach(int flashtime in spectrumDropTwoBeats){
                flash.Fade(flashtime, flashtime+tick(0,(double)1/(double)2), 0.6, 0);
                flashBG.Fade(flashtime, flashtime+tick(0,(double)1/(double)2), 0.6, 0);
                flashBG.Scale(flashtime, flashtime+tick(0,(double)1/(double)2), 480.0f / BGbitmap.Height, 520.0f / BGbitmap.Height);
                bgSprite.Fade(flashtime, 0.4);
                crazyBG.Fade(flashtime, 0);
                bgSprite.Fade((OsbEasing)4, flashtime+tick(0,(double)1/(double)2), flashtime+tick(0,(double)1/(double)4), 0.5, 0.25);
            }
            bgSprite.Fade(EndTime-tick(0,(double)1/(double)4)+1, EndTime, 0.5, 0);

            foreach(int pump in pumpBG){
                crazyBG.Fade((OsbEasing)4, pump, pump+tick(0,2), 0.35, 0);
            }

            foreach(int flashtick in flashSnare){
                flash.Fade((OsbEasing)4, flashtick, flashtick+tick(0,2), 0.35, 0);
            }
            if(StartTime==204758){
                flash.Fade((OsbEasing)3, EndTime, EndTime+tick(0,(double)1/(double)2), 1, 0);
            }
            if(StartTime!=63581){
                flash.Fade((OsbEasing)3, EndTime-tick(0,(double)1/(double)4), EndTime, 0, 1);
            }

            var barWidth = Width / BarCount;
            for (var i = 0; i < BarCount; i++){
                var keyframes = heightKeyframes[i];
                keyframes.Simplify1dKeyframes(Tolerance, h => h);
                var actual_x = Position.X + i * barWidth;
                //Log($"{actual_x}, {(float)Math.Sin(actual_x)}");
                //Log($"{barWidth}");
                List<double> curveCircle = CalculateCurve(0, 0, 30.0);
                int run = 0;
                var bar = layer.CreateSprite(SpritePath, SpriteOrigin);
                var startBar = StartTime-(float)tick(0,(double)1/(double)2)+ ((float)tick(0,(double)1/(double)2)/BarCount)*i;
                bar.Fade(OsbEasing.InQuad, startBar, StartTime, 0, 1);
                bar.Rotate(startBar, 0);
                for (float x = StartTime-(float)tick(0,(double)1/(double)2); x <= EndTime; x+=30.0f){
                    var actual_y = (float)Math.Sin(MathHelper.DegreesToRadians(actual_x/2)+0.0025*x);
                    var after_y = (float)Math.Sin(MathHelper.DegreesToRadians(actual_x/2)+0.0025*x +0.0025);
                    bar.Move(x, x+30, new Vector2(actual_x, Position.Y + actual_y*35)+new Vector2((float)curveCircle[run], (float)curveCircle[run+1]), new Vector2(actual_x, Position.Y + after_y*35)+new Vector2((float)curveCircle[run+2], (float)curveCircle[run+3]));
                    run+=2;
                    if(run==356) run = 0;
                }

                var scaleX = Scale.X * barWidth / bitmap.Width;
                scaleX = (float)Math.Floor(scaleX * 10) / 10.0f;

                if(StartTime==125699){
                    bar.ScaleVec(139111-(int)tick(0,4), 139111, scaleX, scaleX*1.5, scaleX, scaleX);
                    bar.Rotate(139111, 0);
                    bar.ScaleVec(144758-(int)tick(0,4), 144758, scaleX, scaleX*1.5, scaleX, scaleX);
                    bar.Rotate(144758, 0);
                }
                if(StartTime==204758){
                    bar.ScaleVec(218169-(int)tick(0,4), 218169, scaleX, scaleX*1.5, scaleX, scaleX);
                    bar.Rotate(218169, 0);
                    bar.ScaleVec(223816-(int)tick(0,4), 223816, scaleX, scaleX*1.5, scaleX, scaleX);
                    bar.Rotate(223816, 0);
                }

                var hasScale = false;
                var lastMid = scaleX;
                keyframes.ForEachPair(
                    (start, end) =>
                    {
                        var random_angle = Random(-10,10);
                        if(spectrumDropTwoBeats.Contains((int)start.Time) || spectrumDropTwoBeats.Contains((int)start.Time+1) || spectrumDropTwoBeats.Contains((int)start.Time-1)){
                            bar.ScaleVec((OsbEasing)19, start.Time, start.Time+tick(0,1),
                            scaleX, scaleX,
                            scaleX, start.Value);
                            bar.ScaleVec((OsbEasing)19, start.Time+tick(0,(double)6/(double)8), start.Time+tick(0,(double)1/(double)2),
                            scaleX, start.Value,
                            scaleX, scaleX);
                            bar.Rotate(start.Time, MathHelper.DegreesToRadians(random_angle));
                            bar.Rotate(start.Time+tick(0,(double)6/(double)8), start.Time+tick(0,(double)1/(double)2), MathHelper.DegreesToRadians(random_angle), 0);
                        }
                        if(spectrumDropOneBeat.Contains((int)start.Time) || spectrumDropOneBeat.Contains((int)start.Time+1) || spectrumDropOneBeat.Contains((int)start.Time-1)){
                            bar.ScaleVec((OsbEasing)19, start.Time, start.Time+tick(0,2),
                            scaleX, scaleX,
                            scaleX, start.Value);
                            bar.ScaleVec((OsbEasing)19, start.Time+tick(0,(double)4/(double)3), start.Time+tick(0,1),
                            scaleX, start.Value,
                            scaleX, scaleX);
                            bar.Rotate(start.Time, MathHelper.DegreesToRadians(random_angle));
                            bar.Rotate(start.Time+tick(0,(double)4/(double)3), start.Time+tick(0,1), MathHelper.DegreesToRadians(random_angle), 0);
                        }
                        if(spectrumDropOneMidBeat.Contains((int)start.Time) || spectrumDropOneMidBeat.Contains((int)start.Time+1) || spectrumDropOneMidBeat.Contains((int)start.Time-1)){
                            bar.ScaleVec((OsbEasing)19, start.Time, start.Time+tick(0,2),
                            scaleX, scaleX,
                            scaleX, start.Value);
                            bar.ScaleVec((OsbEasing)19, start.Time+tick(0,(double)5/(double)6), start.Time+tick(0,1)+tick(0,2),
                            scaleX, start.Value,
                            scaleX, scaleX);
                            bar.Rotate(start.Time, MathHelper.DegreesToRadians(random_angle));
                            bar.Rotate(start.Time+tick(0,(double)5/(double)6), start.Time+tick(0,1)+tick(0,2), MathHelper.DegreesToRadians(random_angle), 0);
                        }
                        if(spectrumDropMidBeat.Contains((int)start.Time) || spectrumDropMidBeat.Contains((int)start.Time+1) || spectrumDropMidBeat.Contains((int)start.Time-1)){
                            if(lastMid==scaleX) lastMid = start.Value;
                            bar.ScaleVec((OsbEasing)19, start.Time, start.Time+tick(0,3), scaleX, lastMid, scaleX, start.Value);
                            bar.Rotate(start.Time, MathHelper.DegreesToRadians(random_angle));
                        }
                    },
                    MinimalHeight,
                    s => (float)Math.Round(s, CommandDecimals)
                );

                var colors = "0,330,0.8,1,0.8,1";
                var colorOne = int.Parse(colors.Split(',')[0], System.Globalization.CultureInfo.InvariantCulture);
                var colorTwo = int.Parse(colors.Split(',')[1], System.Globalization.CultureInfo.InvariantCulture);
                var colorOneSa = double.Parse(colors.Split(',')[2], System.Globalization.CultureInfo.InvariantCulture);
                var colorOneBra = double.Parse(colors.Split(',')[3], System.Globalization.CultureInfo.InvariantCulture);
                var colorTwoSa = double.Parse(colors.Split(',')[4], System.Globalization.CultureInfo.InvariantCulture);
                var colorTwoBra = double.Parse(colors.Split(',')[5], System.Globalization.CultureInfo.InvariantCulture);
                if(i%2==0)
                    bar.ColorHsb(startTime, colorOne, colorOneSa, colorOneBra);
                else
                    bar.ColorHsb(startTime, colorTwo, colorTwoSa, colorTwoBra);
                bar.Additive(startTime, endTime);

                bar.Fade((OsbEasing)6, endTime-tick(0, (double)1/(double)4), endTime, 1, 0);
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
