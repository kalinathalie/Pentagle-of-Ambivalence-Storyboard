using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.IO;

namespace StorybrewScripts{
    public class LyricsVerso1 : StoryboardObjectGenerator{
        [Configurable]
        public string SubtitlesPath = "lyrics.srt";

        [Configurable]
        public string FontName = "Verdana";

        [Configurable]
        public string SpritesPath = "sb/f";

        [Configurable]
        public int FontSize = 26;

        [Configurable]
        public float FontScale = 0.5f;

        [Configurable]
        public Color4 FontColor = Color4.White;

        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;

        [Configurable]
        public int ShadowThickness = 0;

        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 100);

        [Configurable]
        public Vector2 Padding = Vector2.Zero;

        [Configurable]
        public float SubtitleX = 400;

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
            },
            new FontShadow(){
                Thickness = ShadowThickness,
                Color = ShadowColor,
            });

            var subtitles = LoadSubtitles(SubtitlesPath);
            generateLyrics(font, subtitles, "", false);
        }

        public void generateLyrics(FontGenerator font, SubtitleSet subtitles, string layerName, bool additive){
            var layer = GetLayer(layerName);
            generatePerLine(font, subtitles, layer, additive);
        }

        public void generatePerLine(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer, bool additive){
            double last_first_time = 22993;
            double last_end_time = 23346;
            foreach (var line in subtitles.Lines){
                var texture = font.GetTexture(line.Text);
                var position = new Vector2(SubtitleX - texture.BaseWidth * FontScale * 0.5f, SubtitleY)
                    + texture.OffsetFor(Origin) * FontScale;
                var lastPosition = new Vector2(SubtitleX - texture.BaseWidth * FontScale * 0.5f, SubtitleY)
                    + texture.OffsetFor(Origin) * FontScale + new Vector2(-10,30);
                var firstPosition = new Vector2(SubtitleX - texture.BaseWidth * FontScale * 0.5f, SubtitleY)
                    + texture.OffsetFor(Origin) * FontScale + new Vector2(10,-30);

                var sprite = layer.CreateSprite(texture.Path, Origin, position);
                sprite.Rotate(line.StartTime - 200, MathHelper.DegreesToRadians(9));
                sprite.Scale(line.StartTime, FontScale);

                sprite.Color((OsbEasing)6, line.StartTime-200, line.EndTime, Color4.Red, Color4.White);
                
                sprite.Fade(last_first_time - 300, last_first_time, 0, 0.4);
                sprite.Fade(last_end_time-200, last_end_time, 0.4, 1);
                sprite.Move((OsbEasing)3, last_end_time-200, last_end_time, firstPosition, position);
                sprite.Fade(line.EndTime - 200, line.EndTime, 1, 0);
                sprite.Move((OsbEasing)3, line.EndTime-200, line.EndTime, position, lastPosition);
                if (additive) sprite.Additive(line.StartTime - 200, line.EndTime);
                last_first_time = line.StartTime;
                last_end_time = line.EndTime;
            }
        }
    }
}
