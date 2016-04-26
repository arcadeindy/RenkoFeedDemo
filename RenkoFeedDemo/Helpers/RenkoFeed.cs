// Copyright (C) 2016 SquidEyes, LLC. - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and Confidential
// Written by Louis S. Berman <louis@squideyes.com>, 4/25/2016
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

        public RenkoFeed(double brickTicks, Decimals decimals)
        {
            // validate!!!!!!!

            this.brickTicks = brickTicks;
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

                var brick = new Brick(decimals, openOn, tick.TickOn);

                brick.OpenRate = brick.LowRate =
                    Math.Round(lastRate - brickTicks / 2, 5);

                brick.HighRate = brick.CloseRate =
                    Math.Round(lastRate + brickTicks / 2, 5);

                AddAndRaiseBrick(brick);

                openOn = tick.TickOn;
            }

            while (tick.BidRate <= lastRate - (brickTicks * 1.5))
            {
                lastRate = Math.Round(lastRate - brickTicks, 5);

                var brick = new Brick(decimals, openOn, tick.TickOn);

                brick.OpenRate = brick.HighRate =
                    Math.Round(lastRate + brickTicks / 2, 5);

                brick.LowRate = brick.CloseRate =
                    Math.Round(lastRate - brickTicks / 2, 5);

                AddAndRaiseBrick(brick);

                openOn = tick.TickOn;
            }
        }

        private void AddAndRaiseBrick(Brick brick)
        {
            bricks.Add(brick);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Brick> GetEnumerator() => bricks.GetEnumerator();
    }
}
