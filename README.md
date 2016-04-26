To run the RenkoFeed Demo, you'll need to obtain a demo or runtime 
SciChart license file from <http://www.scichart.com/> then copy the 
file into the bin/Debug folder.

----------

For the uninitiated, a Renko chart is made up of bars, also known as
bricks, which are always the same price range from the Open to Close.
The distance between the Open to the Close is specified in price ticks.
For example, if the Tick Size is .25, and the Renko brick size is
specified as 4, the Renko brick will have a price range of 1.0.  To 
learn more, visit <http://easyrenko.com/>.

There are Up Renko bars and Down Renko bars, and each bar either
continues or reverses the direction/trend of the previous bar. A bar
continues the direction of the previous bar when price moves the brick
size in the same direction from the previous bar closing price.

A bar reverses the direction of the previous bar when price moves twice
the brick size in the opposite direction from the previous bar closing
price. Until one of the bar closing conditions occurs, the bar continues
to accumulate, and can transition between looking like an Up and Down
bar until the bar actually closes. When one of the bar closing
conditions occurs, the bar closes and a new bar is started with the next
price tick.

When the direction/trend of the previous bar is continued with a new
bar, the open of the new bar is equal to the close of the previous bar.
When the direction/trend of the previous bar is reversed, the open of
the new bar is equal to the open of the prior bar.

