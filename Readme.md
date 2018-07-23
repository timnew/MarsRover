Language and Tools
==========================
This solution is my answer for Mars Rovers Problem
All application is written in C# 4.0 with Visual Studio 2010 and Expression Blend 4.
So to build the solution requires the VS 2010 or .net 4 SDK.

Project Organization
===========================
There are 5 projects included in the solution.
1. MarsRovers
MarsRovers is the kernel class library, which contains the simulation engine and any
other key features related to the simulation.

2. MarsRover.Test
MarsRover.Test is the unit test project for MarsRovers. And for convenience reason,
Mars Rovers is internal visible to it.

3. MarsRovers.Console
MarsRovers.Console is a command-line based shell for MarsRovers. It enables user to
config the MarsRovers simulation engine, and review the result.

4. MarsRovers.Wpf
MarsRovers.Wpf is a super simple wpf application that enables user to watch the simulations
process in visual. For time reason, the app is not in product quality, which is just for fun.
Since I don't have enough time to design and implement full-functional UI for configuration,
I uses a tricky way to collect configuration from a command line window.
Just click the configure button, and type the configuration in command-line as usual.
Then click Start, then simulation will begin.

5. Windy.Foundation
It is actually a subset of classes of my own application framework. It provides some support
mechanism and features.

A not so brief description of the design
==================================================================
Classes in MarsRovers can be divided into following categories:
1. Models
Classes in this category, such as Rover, Plateau, is the foundation models for the simulation,
which are abstracted from the problem description.

2. ConfigurationProviders
Configuration provider is a adapter class that retrieves the simulation configuration from
different sources, such as Console or TextReader(actually can be a text file, by wrapping it
with StreamReader.) or just a hard-coded configuration. It ensures the capability to extend the
simulation engine.
After collection the configuration, then it initializes the models,and drives them into a state ready
for simulation.

3. Actions
IRoverAction is the abstraction of all possible actions that rover can perform. And there are 3 concrete
actions in project by default: move, turn left and turn right.
Actually I implemented a super lightweight dependency injection mechanism in project, which searches the
assemblies and instantiates all the actions it found in a global cache, which I called action pool.
Action can apply to rover to change the rover's state, such as position or orientation. There might be tons
of redundant actions being used in simulation, but actually there are only few instances cached in the pool.
For extensibility purpose, actually the action can be some more complicated action, such as turn back or even
move the boundary of plateau. As you can see, to add a new action into system in super easy.

4. Rover Controllers
In my solution, the instructions are not assigned to the rover itself, instead I assigned them to a controller,
then the controller take the responsibility to parse the instructions and push corresponding action to rover while
timer ticks.
Actually, rover controller shouldn't be push actions to rover according to the predefined command. It can works
in a more intelligent way, such as lead a rover follows another or drives the rover run around the plateau.
To introduce such an AI controller is also not too hard.

5. Timers
Timer is the driver of the simulation, it push time slice to the controllers in a order way, then collects the
feedback from controllers to determine where more time slices are needed.
It is possible to change the simulation pace by choosing different timers.
Timer is also extensible, actually, in WPF Shell, I introduce a new type of timer, which ticks depends on a
Dispatcher Timer, which ensure these is no cross thread violation in the simulation.

Timer-Controller-Rover data flow
Actually, I introduced simplified RX framework into the simulation
engine.
Timer push the time slice to the controller, and then controller push the corresponding instruction to rover.
Then rover changes it state, and then push the property changed notification to UI.
As you can see, the data flow is not pull-based but push-based, which provides a better control to simulation
timing, and the data flow is more smooth than the pull-based.
And there is an interesting design that I'd like to introduce a little bit:
Data in push model can only flows from the observee to observer, which means the data flow in one-way. But timer
need the feedback from the controllers to find out whether all the controllers had pushed all the instructions to
the rovers.
To solve this conflict, I introduce the time slice class, which is a reference type. The timer keep the reference
to it, and then push it to the controllers. Then the time slices passes through all the controllers, and controllers
reports their status to time slice. And finally timer check the data in time slice, then it knows what it need to know.

And another interesting design: the time slice controller
I wants to reuse the time slice rather than create a new one each time, which means the properties of time slice should
be mutable. At the same time, I don't want controller be able to modify the time slice's property
which means the properties should be read-only.
To solve the conflict, I introduces a controller, which is a delegate passed out from time slice internal methods.
the controller works a token, objects can modify the properties of time slice in specific way with the token.


Build Instruction
==========================
And there are 3rd party dependencies involved in the some projects.
For some reason, I didn't include the binaries into the repository, instead I use nuget
to fetch these dependencies while building the project.
To build the solution, you should install nuget first and make sure path of nuget has been
added into the path environment variable, then everything will be done automatically.

Or you can manually install these packages by nuget or just downloading them from CodePlex
or Google Code.

Here is the list of 3rd party dependencies:
MarsRover.Test requires
xunit version: 1.8.0.1545
Moq   version: 4.0.10827

MarsRovers.Wpf requires
Mvvm Light toolkit version: 3.0.2

Besides 3rd libraries, the solution also involved the Code Analysis and Code Contract.
To enable the full features, a Code Contract VS Plug-in might be necessary.

