using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;

namespace StorybrewScripts{
    public class LyricVerso2 : StoryboardObjectGenerator{

        [Configurable]
        public string Line = "sb/pixel.png";

        [Configurable]
        public string Heart = "sb/heart2.png";

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
        public float SubtitleY = 400;

        [Configurable]
        public bool TrimTransparency = true;

        [Configurable]
        public bool EffectsOnly = false;

        [Configurable]
        public bool Debug = false;

        [Configurable]
        public bool First = false;

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public OsbOrigin Origin = OsbOrigin.Centre;

        public override void Generate(){
            var layerDesign = GetLayer("LyricVerso2");

            var hSprite1 = layerDesign.CreateSprite(Line, OsbOrigin.Centre, new Vector2(165, 58));
            var hSprite2 = layerDesign.CreateSprite(Line, OsbOrigin.Centre, new Vector2(476, 58));

            var heart1 = layerDesign.CreateSprite(Heart, OsbOrigin.Centre, new Vector2(280, 58));
            var heart2 = layerDesign.CreateSprite(Heart, OsbOrigin.Centre, new Vector2(320, 58));
            var heart3 = layerDesign.CreateSprite(Heart, OsbOrigin.Centre, new Vector2(360, 58));

            if(StartTime==0){
                hSprite1.StartLoopGroup(0, 3);
                hSprite1.ScaleVec(OsbEasing.OutCirc, 35699, 36052, 0, 0, 200/2, 1);
                hSprite1.Fade(35699, 40640, 1, 1);
                hSprite1.ScaleVec(OsbEasing.InCirc, 40287, 40640, 200/2, 1, 0, 0);
                hSprite1.Fade(40287, 41346, 1, 0);
                hSprite1.EndGroup();

                hSprite2.StartLoopGroup(0, 3);
                hSprite2.ScaleVec(OsbEasing.OutCirc, 35699, 36052, 0, 0, 200/2, 1);
                hSprite2.Fade(35699, 40640, 1, 1);
                hSprite2.ScaleVec(OsbEasing.InCirc, 40287, 40640, 200/2, 1, 0, 0);
                hSprite2.Fade(40287, 41346, 1, 0);
                hSprite2.EndGroup();

                heart1.StartLoopGroup(0, 3);
                heart1.Scale(OsbEasing.OutCirc, 35699, 36052, 0, 0.2);
                heart1.Fade(35699, 40640, 1, 1);
                heart1.Scale(OsbEasing.InCirc, 40287, 40640, 0.2, 0);
                heart1.Fade(40287, 41346, 1, 0);
                heart1.EndGroup();

                heart2.StartLoopGroup(0, 3);
                heart2.Color(35699, Color4.White);
                heart2.Scale(OsbEasing.OutCirc, 35699, 36052, 0, 0.4);
                heart2.Fade(35699, 40640, 1, 1);
                heart2.Scale(OsbEasing.InCirc, 40287, 40640, 0.4, 0);
                heart2.Fade(40287, 41346, 1, 0);
                heart2.Color(OsbEasing.InCirc, 36052, 36758, 1, 0, 0, 1, 1, 1);
                heart2.Color(OsbEasing.InCirc, 37464, 38169, 1, 0, 0, 1, 1, 1);
                heart2.Color(OsbEasing.InCirc, 38875, 39581, 1, 0, 0, 1, 1, 1);
                heart2.Color(OsbEasing.InCirc, 40287, 40640, 1, 0, 0, 1, 1, 1);
                heart2.EndGroup();

                heart3.StartLoopGroup(0, 3);
                heart3.Scale(OsbEasing.OutCirc, 35699, 36052, 0, 0.2);
                heart3.Fade(35699, 40640, 1, 1);
                heart3.Scale(OsbEasing.InCirc, 40287, 40640, 0.2, 0);
                heart3.Fade(40287, 41346, 1, 0);
                heart3.EndGroup();
            }
            if(StartTime==227346){
                hSprite1.ScaleVec(OsbEasing.OutCirc, 227346, 228052, 0, 0, 200/2, 1);
                hSprite1.Fade(227346, 238640, 1, 1);
                hSprite1.ScaleVec(OsbEasing.InCirc, 237934, 238640, 200/2, 1, 0, 0);
                hSprite1.Fade(237934, 238640, 1, 0);

                hSprite2.ScaleVec(OsbEasing.OutCirc, 227346, 228052, 0, 0, 200/2, 1);
                hSprite2.Fade(227346, 238640, 1, 1);
                hSprite2.ScaleVec(OsbEasing.InCirc, 237934, 238640, 200/2, 1, 0, 0);
                hSprite2.Fade(237934, 238640, 1, 0);

                heart1.Scale(OsbEasing.OutCirc, 227346, 228052, 0, 0.2);
                heart1.Fade(227346, 238640, 1, 1);
                heart1.Scale(OsbEasing.InCirc, 237934, 238640, 0.2, 0);
                heart1.Fade(237934, 238640, 1, 0);

                heart2.Color(227346, Color4.White);
                heart2.Scale(OsbEasing.OutCirc, 227346, 228052, 0, 0.4);
                heart2.Fade(227346, 238640, 1, 1);
                heart2.Scale(OsbEasing.InCirc, 237934, 238640, 0.4, 0);
                heart2.Fade(237934, 238640, 1, 0);

                for(double x = StartTime+tick(0,1); x<=EndTime; x+=tick(0,(double)1/(double)2)){
                    heart2.Color(x, x+tick(0,1), 1, 0, 0, 1, 1, 1);
                }

                heart3.Scale(OsbEasing.OutCirc, 227346, 228052, 0, 0.2);
                heart3.Fade(227346, 238640, 1, 1);
                heart3.Scale(OsbEasing.InCirc, 237934, 238640, 0.2, 0);
                heart3.Fade(237934, 238640, 1, 0);
            }
            if(StartTime==86169){
                hSprite1.ScaleVec(OsbEasing.OutCirc, StartTime, 86875, 0, 0, 200/2, 1);
                hSprite1.Fade(StartTime, 105934, 1, 1);
                hSprite1.ScaleVec(OsbEasing.InCirc, 105934, 106640, 200/2, 1, 0, 0);
                hSprite1.Fade(105934, 106640, 1, 0);

                hSprite2.ScaleVec(OsbEasing.OutCirc, StartTime, 86875, 0, 0, 200/2, 1);
                hSprite2.Fade(StartTime, 105934, 1, 1);
                hSprite2.ScaleVec(OsbEasing.InCirc, 105934, 106640, 200/2, 1, 0, 0);
                hSprite2.Fade(105934, 106640, 1, 0);

                heart1.Scale(OsbEasing.OutCirc, StartTime, 86875, 0, 0.2);
                heart1.Fade(StartTime, 105934, 1, 1);
                heart1.Scale(OsbEasing.InCirc, 105934, 106640, 0.2, 0);
                heart1.Fade(105934, 106640, 1, 0);

                heart2.Color(StartTime, Color4.White);
                heart2.Scale(OsbEasing.OutCirc, StartTime, 86875, 0, 0.4);
                heart2.Fade(StartTime, 105934, 1, 1);
                heart2.Scale(OsbEasing.InCirc, 105934, 106640, 0.4, 0);
                heart2.Fade(105934, 106640, 1, 0);

                for(double x = StartTime+tick(0,(double)1/(double)2); x<=91816; x+=tick(0,(double)1/(double)4)){
                    heart2.Color(x, x+tick(0,1), 1, 0, 0, 1, 1, 1);
                }

                for(double x = 92169; x<=EndTime; x+=tick(0,(double)1/(double)2)){
                    heart2.Color(x, x+tick(0,1), 1, 0, 0, 1, 1, 1);
                }

                heart3.Scale(OsbEasing.OutCirc, StartTime, 86875, 0, 0.2);
                heart3.Fade(StartTime, 105934, 1, 1);
                heart3.Scale(OsbEasing.InCirc, 105934, 106640, 0.2, 0);
                heart3.Fade(105934, 106640, 1, 0);

            }
            
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
            generatePerCharacter(font, subtitles, layer, additive);
        }

        public void generatePerCharacter(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer, bool additive){
            foreach (var subtitleLine in subtitles.Lines){
                var letterY = SubtitleY;
                foreach (var line in subtitleLine.Text.Split('\n')){
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line){
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * FontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
                    }

                    var letterX = 320 - lineWidth * 0.5f;
                    var run = 0;
                    foreach (var letter in line){
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty){
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(Origin) * FontScale;
                            var lastPosition = new Vector2(letterX, letterY)
                                + texture.OffsetFor(Origin) * FontScale+new Vector2(0,-20);
                            var firstPosition = new Vector2(letterX, letterY)
                                + texture.OffsetFor(Origin) * FontScale+new Vector2(0,+15);

                            var sprite = layer.CreateSprite(texture.Path, Origin, position);
                            sprite.Scale(subtitleLine.StartTime, FontScale);
                            sprite.Move(subtitleLine.StartTime - 200, subtitleLine.StartTime - 200+run, firstPosition, position);
                            sprite.Fade(subtitleLine.StartTime - 200, subtitleLine.StartTime - 200+run, 0, 1);
                            sprite.Fade(subtitleLine.EndTime - 200, subtitleLine.EndTime, 1, 0);
                            sprite.Move(subtitleLine.EndTime - 200, subtitleLine.EndTime, position, lastPosition);
                            sprite.ScaleVec((OsbEasing)6, subtitleLine.EndTime - 200, subtitleLine.EndTime, FontScale, FontScale, FontScale, 0);
                            if (additive) sprite.Additive(subtitleLine.StartTime - 200, subtitleLine.EndTime);
                        }
                        letterX += texture.BaseWidth * FontScale;
                        run+=14;
                    }
                    letterY += lineHeight;
                }
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
