using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace StorybrewScripts{
    public class LyricDrop : StoryboardObjectGenerator{
        [Configurable]
        public string SubtitlesPath = "lyrics.srt";

        [Configurable]
        public string FontName = "Verdana";

        [Configurable]
        public string SpritesPath = "sb/f";

        [Configurable]
        public int FontSize = 26;

        [Configurable]
        public Color4 FontColor = Color4.White;

        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;

        [Configurable]
        public Vector2 Padding = Vector2.Zero;

        [Configurable]
        public float SubtitleY = 400;

        [Configurable]
        public bool TrimTransparency = true;

        [Configurable]
        public bool EffectsOnly = false;

        [Configurable]
        public bool Debug = false;

        [Configurable]
        public OsbOrigin Origin = OsbOrigin.Centre;

        public override void Generate(){
            var font = LoadFont(SpritesPath, new FontDescription(){
                FontPath = FontName,
                FontSize = FontSize,
                Color = FontColor,
                Padding = Padding,
                FontStyle = FontStyle,
                TrimTransparency = TrimTransparency,
                EffectsOnly = EffectsOnly,
                Debug = Debug,
            });

            var subtitles = LoadSubtitles(SubtitlesPath);

            generateLyrics(font, subtitles, "", false);
        }

        public void generateLyrics(FontGenerator font, SubtitleSet subtitles, string layerName, bool additive){
            var layer = GetLayer(layerName);
            generatePerLine(font, subtitles, layer, additive);
        }

        public void generatePerLine(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer, bool additive){
            var positionList = new TupleList<int, int>{
                { 120, 100 },//I'll
                { 230, 100 },//find
                { 370, 100 },//some
                { 120, 138 },//peace
                { 120, 192 },//this
                { 120, 254 },//time
                { 306, 194 },//i'll 
                { 410, 194 },//let 
                { 306, 254 },//you
                { 120, 312 },//Know
                { 155, 160 },//we're
                { 325, 160 },//gonna
                { 155, 206 },//grow
                { 110, 100 },//i'm
                { 180, 100 },//gonna
                { 250, 146 },//show 
                { 316, 190 },//you
                { 110, 146 },//my
                { 110, 240 },//Heart
                { 340, 240 },//and
                { 110, 300 },//soul
                { 200, 150 },// this
                { 325, 150 },//time
                { 138, 190 },//this
                { 325, 190 },//time
                { 170, 140 },//my
                { 150, 180 },//love
                { 3, 1 },
                { 1, 1 }
            };
            var scaleList = new TupleList<double, double>{
                { 0.35, 0.35 },//I'll
                { 0.35, 0.35 },//find
                { 0.35, 0.35 },//some
                { 0.845, 0.50 },//peace
                { 0.55, 0.55},//this
                { 0.55, 0.55 },//time
                { 0.45, 0.45 },//i'll
                { 0.42, 0.45 },//let
                { 0.65, 0.53 },//you
                { 0.900, 0.60 },//Know
                { 0.35, 0.35 },//we're
                { 0.35, 0.35 },//gonna
                { 0.78, 1.3 },//grow
                { 0.35, 0.35 },//i'm
                { 0.35, 0.35 },//gonna
                { 0.35, 0.35 },//you 
                { 0.35, 0.35 },//show 
                { 0.65, 0.80 },//my
                { 0.45, 0.50 },//Heart
                { 0.45, 0.5 },//and
                { 1.1, 0.80 },//soul
                { 0.35, 0.35 },// this
                { 0.35, 0.35 },//time
                { 0.55, 0.55 },//this
                { 0.55, 0.55 },//time
                { 0.35, 0.35 },//my
                { 0.75, 0.75 },//love
            };
            var angleList = new List<int>{
                0,//I'll
                0,//find
                0,//some
                0,//peace
                0,//this
                0,//time
                0,//i'll
                0,//let 
                0,//you
                0,//Know
                0,//we're
                0,//gonna
                0,//grow
                0,//i'm 
                0,//gonna
                0,//show
                0,//you
                0,//my
                0,//Heart
                0,//and
                0,//soul
                0,// this
                0,//time
                0,//this
                0,//time
                0,//my
                0,//love
            };
            var colorList = new List<Color4>{
                Color4.White,//I'll
                Color4.White,//find
                Color4.White,//some
                Color4.MediumSeaGreen,//peace
                Color4.White,//this
                Color4.MediumSeaGreen,//time
                Color4.White,//i'll
                Color4.White,//let 
                Color4.White,//you
                Color4.CornflowerBlue,//Know
                Color4.White,//we're
                Color4.White,//gonna
                Color4.CornflowerBlue,//grow
                Color4.White,//i'm 
                Color4.White,//gonna
                Color4.CornflowerBlue,//show
                Color4.White,//you
                Color4.White,//my
                Color4.Red,//Heart
                Color4.White,//and
                Color4.Red,//soul
                Color4.White,// this
                Color4.MediumSeaGreen,//time
                Color4.White,//this
                Color4.MediumSeaGreen,//time
                Color4.White,//my
                Color4.Red,//love
            };
            var run = 0;
            foreach (var line in subtitles.Lines){
                var texture = font.GetTexture(line.Text);
                var position = new Vector2(positionList[run].Item1, positionList[run].Item2);
                var sprite = layer.CreateSprite(texture.Path, Origin, position);
                sprite.ScaleVec(line.StartTime, scaleList[run].Item1, scaleList[run].Item2);
                sprite.Fade(line.StartTime - 100, line.StartTime, 0, 1);
                sprite.Fade(line.EndTime - 100, line.EndTime, 1, 0);
                sprite.Rotate(line.EndTime-100, MathHelper.DegreesToRadians(angleList[run]));
                sprite.Color(line.StartTime, colorList[run]);
                run+=1;
            }
        }
        public class TupleList<T1, T2> : List<Tuple<T1, T2>>{
            public void Add( T1 item, T2 item2 ){
                Add( new Tuple<T1, T2>( item, item2 ) );
            }
        }
    }
}
