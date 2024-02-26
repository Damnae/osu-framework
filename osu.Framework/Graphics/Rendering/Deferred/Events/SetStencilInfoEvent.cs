// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Graphics.Rendering.Deferred.Events
{
    internal readonly record struct SetStencilInfoEvent(RenderEventType Type, StencilInfo Info) : IRenderEvent
    {
        public static SetStencilInfoEvent Create(StencilInfo info)
            => new SetStencilInfoEvent(RenderEventType.SetStencilInfo, info);
    }
}
