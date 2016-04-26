#region Copyright and Author Details
// Project: RenkoFeedDemo, Namespace: RenkoFeedDemo
// Copyright (C) 2016 SquidEyes, LLC.
// Written by Louis S. Berman <louis@squideyes.com>, 4/26/2016

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace RenkoFeedDemo
{
    public class RenkoFeed : IEnumerable<Brick>
    {
        private List<Brick> bricks = new List<Brick>();
        private DateTime? openOn = null;
        private double lastRate = 0.0;

        private Decimals decimals;
        private double brickTicks;

        public event EventHandler<GenericArgs<Brick>> OnBrick;

        public RenkoFeed(double brickPips, Decimals decimals)
        {
            if ((brickPips < 0.1) || (brickPips > 99.9) ||
                (brickPips != Math.Round(brickPips, 1)))
            {
                throw new ArgumentOutOfRangeException(nameof(brickPips));
            }

            brickTicks = brickPips.PipsToRate(decimals);

            this.decimals = decimals;
        }

        public int Count => bricks.Count;

        public Brick this[int index] => bricks[index];

        public void HandleTick(Tick tick)
        {
            if (lastRate == 0.0)
            {
                openOn = tick.TickOn;

                lastRate = Math.Round(tick.BidRate -
                    (tick.BidRate % brickTicks) + brickTicks / 2, 5);
            }

            while (tick.BidRate >= lastRate + (brickTicks * 1.5))
            {
                lastRate = Math.Round(lastRate + brickTicks, 5);

                var brick = new Brick(openOn, tick.TickOn,
                    Math.Round(lastRate - brickTicks / 2, 5),
                    Math.Round(lastRate + brickTicks / 2, 5));

                AddAndRaiseBrick(brick);

                openOn = tick.TickOn;
            }

            while (tick.BidRate <= lastRate - (brickTicks * 1.5))
            {
                lastRate = Math.Round(lastRate - brickTicks, 5);

                var brick = new Brick(openOn, tick.TickOn,
                    Math.Round(lastRate + brickTicks / 2, 5),
                    Math.Round(lastRate - brickTicks / 2, 5));

                AddAndRaiseBrick(brick);

                openOn = tick.TickOn;
            }
        }

        private void AddAndRaiseBrick(Brick brick)
        {
            bricks.Add(brick);

            OnBrick?.Invoke(this, new GenericArgs<Brick>(brick));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Brick> GetEnumerator() => bricks.GetEnumerator();
    }
}
