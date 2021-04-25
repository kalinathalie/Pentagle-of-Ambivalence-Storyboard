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

    public class Intro : StoryboardObjectGenerator{
        [Configurable]
        public string GrayBG = "";

        [Configurable]
        public string NormalBG = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        public override void Generate(){
            var layer = GetLayer("Intro");
            var bitmap = GetMapsetBitmap(GrayBG);

            var bgNormal = layer.CreateSprite(NormalBG, OsbOrigin.Centre);
            bgNormal.Scale(EndTime, 480.0f / bitmap.Height);
            bgNormal.Fade(EndTime, 24052, 0, 1);

            var bgGray = layer.CreateSprite(GrayBG, OsbOrigin.Centre);
            bgGray.Scale(StartTime, 480.0f / bitmap.Height);
            bgGray.Fade(StartTime - 200, StartTime, 0, 0.1);
            bgGray.Fade(StartTime, EndTime, 0.1, 0.8);
            bgGray.Fade(EndTime, 24052, 0.8, 0);
		    


            // Setting the font
            var font = LoadFont("sb/intro-chars", new FontDescription(){
                FontPath = "fonts/QanelasSoftDEMO-ExtraBold.otf",
                FontSize = 72,
                Color = Color4.White,
                Padding = Vector2.Zero,
                //FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontOutline(){
                Thickness = 0,
                //Color = Color.Transparent,
            },
            new FontShadow(){
                Thickness = 0,
               // Color = Color.Transparent,
            });

            var artist = font.GetTexture("Artist");
            var artistSprite = layer.CreateSprite(artist.Path,OsbOrigin.TopCentre);
            artistSprite.Move(2875-150, 4287+150, 310, 250, 340, 250);
            artistSprite.Scale(2875,0.35);
            artistSprite.Fade(OsbEasing.InOutCirc, 2875-150,2875,0,1);
            artistSprite.Fade(4287,4287+150,1,0);

            var kuba = font.GetTexture("Kuba Oms");
            var kubaSprite = layer.CreateSprite(kuba.Path,OsbOrigin.TopCentre);
            kubaSprite.Move(2875-150, 4287+150, 340, 200, 310, 200);
            kubaSprite.Scale(2875,0.35);
            kubaSprite.Fade(OsbEasing.InOutCirc, 2875-150,2875,0,1);
            kubaSprite.Fade(4287,4287+150,1,0);
            kubaSprite.ColorHsb(StartTime, 0, 0.7, 1);

            var song = font.GetTexture("Song");
            var songSprite = layer.CreateSprite(song.Path,OsbOrigin.TopCentre);
            songSprite.Move(5699-150, 7111+150, 340, 250, 310, 250);
            songSprite.Scale(5699,0.35);
            songSprite.Fade(OsbEasing.InOutCirc, 5699-150,5699,0,1);
            songSprite.Fade(7111,7111+150,1,0);

            var mylove = font.GetTexture("My Love");
            var myloveSprite = layer.CreateSprite(mylove.Path,OsbOrigin.TopCentre);
            myloveSprite.Move(5699-150, 7111+150, 310, 200, 340, 200);
            myloveSprite.Scale(5699,0.35);
            myloveSprite.Fade(OsbEasing.InOutCirc, 5699-150,5699,0,1);
            myloveSprite.Fade(7111,7111+150,1,0);
            myloveSprite.ColorHsb(StartTime, 0, 0.7, 1);

            var mappers = font.GetTexture("Mappers");
            var mappersSprite = layer.CreateSprite(mappers.Path,OsbOrigin.TopCentre);
            mappersSprite.Move(5648+2875-150, 5648+7111+150, 600, 100, 650, 100);
            mappersSprite.Scale(5648+2875,0.35);
            mappersSprite.Fade(OsbEasing.InOutCirc, 5648+2875-150,5648+2875,0,1);
            mappersSprite.Fade(5648+7111,5648+7111+150,1,0);

            var line = layer.CreateSprite("sb/pixel.png");
            line.ScaleVec(OsbEasing.InOutCirc,5648+4287-150,5648+4287+500,1,1,170,1);
            line.Move(5648+4287, 5648+7111+150, 650, 90, 600, 90);
            line.Fade(OsbEasing.InOutCirc, 5648+4287-150,5648+4287,0,1);
            line.Fade(5648+7111,5648+7111+150,1,0);

            var names = font.GetTexture("Kalindraz and Sakura Airi");
            var namesSprite = layer.CreateSprite(names.Path,OsbOrigin.TopCentre);
            namesSprite.Move(5648+5699-150, 5648+7111+150, 600, 58, 550, 58);
            namesSprite.Scale(5648+5699,0.28);
            namesSprite.Fade(OsbEasing.InOutCirc, 5648+5699-150,5648+5699,0,1);
            namesSprite.Fade(5648+7111,5648+7111+150,1,0);
            namesSprite.ColorHsb(StartTime, 200, 0.7, 1);
            



            var remixer = font.GetTexture("Remixer");
            var remixerSprite = layer.CreateSprite(remixer.Path,OsbOrigin.TopCentre);
            remixerSprite.Move(5648+8522-150, 5648+12758+150, 20, 250, -30, 250);
            remixerSprite.Scale(5648+8522,0.35);
            remixerSprite.Fade(OsbEasing.InOutCirc, 5648+8522-150,5648+8522,0,1);
            remixerSprite.Fade(5648+12758,5648+12758+150,1,0);

            var line2 = layer.CreateSprite("sb/pixel.png");
            line2.ScaleVec(OsbEasing.InOutCirc,5648+9934-150,5648+9934+500,1,1,170,1);
            line2.Move(5648+9934, 5648+12758+150, -50, 240, 0, 240);
            line2.Fade(OsbEasing.InOutCirc, 5648+9934-150,5648+9934,0,1);
            line2.Fade(5648+12758,5648+12758+150,1,0);

            var maemi = font.GetTexture("Maemi no Yume");
            var maemiSprite = layer.CreateSprite(maemi.Path,OsbOrigin.TopCentre);
            maemiSprite.Move(5648+11346-150, 5648+12758+150, 0, 208, 50, 208);
            maemiSprite.Scale(5648+11346,0.28);
            maemiSprite.Fade(OsbEasing.InOutCirc, 5648+11346-150,5648+11346,0,1);
            maemiSprite.Fade(5648+12758,5648+12758+150,1,0);
            maemiSprite.ColorHsb(StartTime, 100, 0.7, 1);



            var sb = font.GetTexture("Storyboarder");
            var sbSprite = layer.CreateSprite(sb.Path,OsbOrigin.TopCentre);
            sbSprite.Move(5648+14169-150, 24052, 580, 400, 630, 400);
            sbSprite.Scale(5648+14169,0.35);
            sbSprite.Fade(OsbEasing.InOutCirc, 5648+14169-150,5648+14169,0,1);
            sbSprite.Fade(5648+18405,24052,1,0);

            var line3 = layer.CreateSprite("sb/pixel.png");
            line3.ScaleVec(OsbEasing.InOutCirc,5648+15581-150,5648+15581+500,1,1,170,1);
            line3.Move(5648+15581, 24052, 650, 390, 600, 390);
            line3.Fade(OsbEasing.InOutCirc, 5648+15581-150,5648+15581,0,1);
            line3.Fade(5648+18405,24052,1,0);

            var kali = font.GetTexture("K4L1");
            var kaliSprite = layer.CreateSprite(kali.Path,OsbOrigin.TopCentre);
            kaliSprite.Move(5648+16993-150, 24052, 620, 345, 570, 345);
            kaliSprite.Scale(5648+16993,0.53);
            kaliSprite.Fade(OsbEasing.InOutCirc, 5648+16993-150,5648+16993,0,1);
            kaliSprite.Fade(5648+18405,24052,1,0);
            kaliSprite.ColorHsb(StartTime, 320, 0.7, 1);
            
        }
    }
}

