using EpsilonEngine;
[assembly: RegisterVirtualInput("Jump")]
[assembly: RegisterVirtualInput("Right")]
[assembly: RegisterVirtualInput("Left")]
[assembly: RegisterVirtualInput("Up")]
[assembly: RegisterVirtualInput("Down")]

[assembly: DefaultInputBinding("Space", "Jump")]
[assembly: DefaultInputBinding("D", "Right")]
[assembly: DefaultInputBinding("A", "Left")]
[assembly: DefaultInputBinding("W", "Up")]
[assembly: DefaultInputBinding("S", "Down")]