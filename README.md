# What is the Quantified Knife Project?
At [Seattle Ultrasonics](https://seattleultrasonics.com), we want to make a better knife: a knife that makes cutting easier. In order to do that, we first have to understand what “better” means. To find out, we strapped 21 of the most popular chef’s knives to a robotic arm and ran 525 food cutting tests. 

Why go to so much trouble? If you search for “best chef's knife” you'll plenty of editorial guides from reputable sources recommending their top picks. Expert chef recommendations certainly meaningful, and a reviewer’s opinion on ergonomics and design certainly matter.

But when choosing a chef's knife, shouldn’t you be able to consult cutting performance data? And shouldn’t that data be collected under conditions that are as objective and realistic as possible?
It’s common for knife enthusiasts to demonstrate sharpness by slicing a piece of paper, shaving their forearms, or, if they’re feeling acrobatic, cutting through a hanging rope and a row of plastic water bottles. These proxy tests are visually impressive. But we don’t buy chef’s knives to cut paper; we buy chef’s knives to cut food. 

**Since the world hadn’t published objective food cutting data for popular chef’s knives, we decided to create some ourselves.**

# Here’s how we did it
We bought 21 of the most popular chef's knives as recommended by sources like The New York Times Wirecutter, Serious Eats, America's Test Kitchen, Food & Wine, and the Chef’s Knife subreddit.
While these knives were still fresh out of factory packaging, we measured the sharpness of the cutting edge using the [Brubacher Edge Sharpness Scale (BESS)](https://www.sharpeningsupplies.com/collections/edge-on-up), a quantitative proxy test that’s familiar to knife enthusiasts.

We also took close-up photos of the knife edge, belly, tip, and choil. We were curious if there were visual indications that might predict cutting performance. 

Next, we mounted the knives to a six-axis robotic arm. Human cutting isn’t repeatable enough and introduces an opportunity for bias. So, we programmed the robot arm to move in slicing and push-cutting motions that it could repeat precisely for each trial. For every knife, we cut five samples each of tomatoes, potatoes, cheese, carrots, and bread.

To measure the cutting forces, we used a calibrated laboratory scale that streamed data to our robot control software so we could record the downward forces exerted at every point in the cutting stroke. When the tests were complete, we had gathered over 100,000 data points on cutting performance across the 21 knives in the test.

After the robotic cutting was complete, we sent the blades off for [CATRA Automatic Edge Testing](https://www.catra.org/testing-equipment/knife-edges/saet/) to measure initial sharpness and edge retention. The CATRA test is common in the industry and conforms with the International Cutting Test Standard BS EN ISO 8442-5: 2004. These tests were administered by [Dr. Larrin Thomas](https://knifesteelnerds.com/). 

Next, we analyzed the data. To account for natural variations (each tomato is not exactly the same as another, despite our rigorous selection criteria) we averaged the five trials for each food. We then extracted key metrics for each knife’s cutting performance, meant to represent the real-world experience of cutting each food: peak force for tomatoes, maximum force for carrots and bread, average force for potatoes, and total force for cheese. 

# The output of this analysis is the Quantified Knife Project. 
So, which chef's knife is the best? We're not here to make a recommendation, nor are we editorializing our findings. We don’t even have affiliate links to the blades in the study. Instead, we want to do two things:
1.	Provide objective data so people shopping for chef’s knives can make the most informed decisions, and
2.	Use our data to begin a conversation in the knife community about what factors contribute to a knife’s real-world performance.

Of course, the [Quantified Knife Project](https://seattleultrasonics.com/pages/knife-database) is only a first scratch at the surface. For all the breadth and depth of the data we collected, for all the rigor and precision of robotic food cutting, this is still a small set of experiments. There are many more questions we want to ask. What about other foods? What about other knives? What about resharpening? And so on.

That's why it was important that we release all our data in an open-source format so that others could use it as part of their own analysis. We hope that others will take up our challenge to replicate our findings, and that the insights we uncover as a community make the world’s knives “better.”

