using OpenTK;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    /// <summary>
    /// An example of a spectrum effect.
    /// </summary>
    public class CustomRadialSpec : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 10000;

        [Configurable]
        public Vector2 Position = new Vector2(-107, 240);

        [Configurable]
        public int radius = 50;

        [Configurable]
        public float Width = 844;

        [Configurable]
        public int BeatDivisor = 16;

        [Configurable]
        public int BarCount = 96;

        [Configurable]
        public string SpritePath = "sb/bar.png";

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

        public override void Generate()
        {
            var endTime = Math.Min(EndTime, (int)AudioDuration);
            var startTime = Math.Min(StartTime, endTime);
            var bitmap = GetMapsetBitmap(SpritePath);

            var heightKeyframes = new KeyframedValue<float>[BarCount];
            for (var i = 0; i < BarCount; i++)
            {
                heightKeyframes[i] = new KeyframedValue<float>(null);
                heightKeyframes[i].Add(StartTime, MinimalHeight);
            }
            
            var fftTimeStep = Beatmap.GetTimingPointAt(startTime).BeatDuration / BeatDivisor;
            var fftOffset = fftTimeStep * 0.2;
            for (var time = (double)startTime + fftTimeStep; time < endTime+tick(0,1); time += fftTimeStep)
            {
                var fft = GetFft(time + fftOffset, BarCount, null, FftEasing);
                for (var i = 0; i < BarCount; i++)
                {
                    var height = (float)Math.Log10(1 + fft[i] * LogScale) * Scale.Y / bitmap.Height;
                    if (height < MinimalHeight) height = MinimalHeight;

                    heightKeyframes[i].Add(time, height);
                }
            }

            var layer = GetLayer("Spectrum");
            var barWidth = Width / BarCount;
            for (var i = 0; i < BarCount; i++)
            {
                var keyframes = heightKeyframes[i];
                keyframes.Simplify1dKeyframes(Tolerance, h => h);

                var bar = layer.CreateSprite(SpritePath, OsbOrigin.BottomCentre, new Vector2((float)(Position.X + radius * Math.Cos(i*2*Math.PI/BarCount)), (float)(Position.Y + radius * Math.Sin(i*2*Math.PI/BarCount))));
                
                //bar.Additive(startTime, endTime);
                bar.Rotate(startTime,i*2*Math.PI/BarCount + Math.PI/2);

                var scaleX = Scale.X * barWidth / bitmap.Width;
                scaleX = (float)Math.Floor(scaleX * 10) / 10.0f;

                var hasScale = false;
                keyframes.ForEachPair(
                    (start, end) =>
                    {
                        hasScale = true;
                        bar.ScaleVec(start.Time, end.Time,
                            scaleX, start.Value,
                            scaleX, end.Value);
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
                if (!hasScale) bar.ScaleVec(startTime, scaleX, MinimalHeight);
                bar.Fade(EndTime, EndTime+tick(0,1), 1, 0);
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
