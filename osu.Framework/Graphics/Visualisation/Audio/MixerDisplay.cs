// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using ManagedBass.Mix;
using osu.Framework.Audio.Mixing;
using osu.Framework.Audio.Mixing.Bass;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace osu.Framework.Graphics.Visualisation.Audio
{
    public class MixerDisplay : CompositeDrawable
    {
        public readonly AudioMixer Mixer;

        private readonly FillFlowContainer<AudioChannelDisplay> channelsContainer;

        public MixerDisplay(AudioMixer mixer)
        {
            Mixer = mixer;

            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;

            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black.Opacity(0.2f)
                    },
                    new SpriteText
                    {
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                        Text = mixer.Identifier,
                        Font = FrameworkFont.Condensed.With(size: 14),
                        Colour = FrameworkColour.Yellow,
                        Padding = new MarginPadding(2),
                    },
                    channelsContainer = new FillFlowContainer<AudioChannelDisplay>
                    {
                        RelativeSizeAxes = Axes.Y,
                        AutoSizeAxes = Axes.X,
                        Direction = FillDirection.Horizontal,
                        Spacing = new Vector2(10),
                        Padding = new MarginPadding
                        {
                            Horizontal = 10,
                            Bottom = 20
                        }
                    }
                }
            };
        }

        protected override void Update()
        {
            base.Update();

            if (!(Mixer is BassAudioMixer bassMixer))
                return;

            int[] channels = BassMix.MixerGetChannels(bassMixer.Handle);

            if (channels == null)
                return;

            foreach (int channel in channels)
            {
                if (channelsContainer.All(ch => ch.ChannelHandle != channel))
                    channelsContainer.Add(new AudioChannelDisplay(channel));
            }

            channelsContainer.RemoveAll(ch => !channels.Contains(ch.ChannelHandle));
        }
    }
}
